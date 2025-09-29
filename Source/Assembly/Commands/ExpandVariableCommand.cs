using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Text;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Expand", "Variable", DefaultParameterSetName = ParameterSetContent)]
    [OutputType(typeof(string))]
    public sealed class ExpandVariableCommand : PSCmdlet
    {
        private const string ParameterSetPath = "Path";
        private const string ParameterSetContent = "Content";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ParameterSetName = ParameterSetPath)]
        [Alias("PSPath")]
        public string Path { get; set; } = string.Empty;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSetContent)]
        public string Content { get; set; } = string.Empty;

        [Parameter]
        public SwitchParameter Unescaped { get; set; }

        /// <summary>
        /// Specifies which drives to use for variable expansion. Each drive represents a different source or type of variable:
        /// <list type="bullet">
        /// <item><term>bg</term><description>Background color variables.</description></item>
        /// <item><term>emoji</term><description>Emoji variables.</description></item>
        /// <item><term>esc</term><description>Escape sequence variables.</description></item>
        /// <item><term>extra</term><description>Extra or custom variables.</description></item>
        /// <item><term>fg</term><description>Foreground color variables.</description></item>
        /// <item><term>nf</term><description>Nerd Font icon variables.</description></item>
        /// <item><term>variable</term><description>Standard PowerShell variables.</description></item>
        /// </list>
        /// By specifying one or more drives, you control which sources are used during variable expansion.
        /// </summary>
        [Parameter]
        public string[] Drive { get; set; } = new[] { "bg", "emoji", "esc", "extra", "fg", "nf", "variable" };

        [Parameter(ParameterSetName = ParameterSetPath)]
        public SwitchParameter InPlace { get; set; }

        [Parameter(ParameterSetName = ParameterSetPath)]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == ParameterSetContent)
            {
                var result = ExpandVariable(Content ?? string.Empty, "Content");

                if (result != null)
                {
                    WriteObject(result);
                }

                return;
            }

            var resolvedPaths = GetResolvedProviderPathFromPSPath(Path, out var providerInfo);

            foreach (var resolvedPath in resolvedPaths)
            {
                var fullName = BuildProviderQualifiedPath(providerInfo, resolvedPath);
                var value = GetVariableValue(fullName);
                var replacement = ExpandVariable(value?.ToString() ?? string.Empty, fullName);

                if (replacement is null)
                {
                    continue;
                }

                var isVariableProvider = string.Equals(providerInfo.Name, "Variable", StringComparison.OrdinalIgnoreCase);
                var passthruPath = fullName;

                if (isVariableProvider && fullName.IndexOf(':') < 0)
                {
                    passthruPath = providerInfo.Name + ":" + fullName;
                }

                if (InPlace)
                {
                    if (isVariableProvider)
                    {
                        var variableName = passthruPath;

                        var providerSeparator = variableName.IndexOf(':');

                        if (providerSeparator >= 0)
                        {
                            variableName = variableName.Substring(providerSeparator + 1);
                        }

                        var variable = SessionState.PSVariable.Get(variableName);

                        if (variable != null)
                        {
                            variable.Value = replacement;
                            SessionState.PSVariable.Set(variable);
                        }
                        else
                        {
                            SessionState.PSVariable.Set(variableName, replacement);
                        }
                    }
                    else
                    {
                        SessionState.InvokeProvider.Item.Set(passthruPath, replacement);
                    }

                    if (Passthru)
                    {
                        if (isVariableProvider)
                        {
                            WriteObject(replacement);
                        }
                        else
                        {
                            WriteObject(SessionState.InvokeProvider.Item.Get(passthruPath), true);
                        }
                    }
                }
                else
                {
                    WriteObject(replacement);
                }
            }
        }

        private static string BuildProviderQualifiedPath(ProviderInfo providerInfo, string path)
        {
            if (providerInfo.Name == "Variable" || providerInfo.Name == "FileSystem" || path.Contains(':'))
            {
                return path;
            }

            return $"{providerInfo.Name}:{path}";
        }

        private string ExpandVariable(string code, string source)
        {
            var replacements = new List<TextReplacement>();
            var ast = Parser.ParseInput(code, source, out _, out var errors);

            if (errors.Length > 0)
            {
                WriteError(new ErrorRecord(
                    new ParseException($"{errors.Length} Parse Errors in {source}, cannot expand."),
                    "ParseErrors",
                    ErrorCategory.InvalidOperation,
                    source));

                return null;
            }

            var drives = new HashSet<string>(Drive, StringComparer.OrdinalIgnoreCase);

            var variables = ast
                .FindAll(node => node is VariableExpressionAst, searchNestedScriptBlocks: true)
                .OfType<VariableExpressionAst>()
                .Where(variable => ShouldExpand(variable, drives));

            foreach (var variable in variables)
            {
                try
                {
                    var replacement = GetVariableValue(variable.VariablePath.UserPath)?.ToString() ?? string.Empty;

                    if (!Unescaped)
                    {
                        replacement = replacement.ToPsEscapedString();
                    }

                    if (variable.Parent is ExpandableStringExpressionAst)
                    {
                        replacements.Add(new TextReplacement(replacement, variable.Extent));
                    }
                    else
                    {
                        replacements.Add(new TextReplacement("\"" + replacement + "\"", variable.Extent));
                    }
                }
                catch
                {
                    WriteWarning($"VariableNotFound: '{variable.VariablePath.UserPath}' at {source}:{variable.Extent.StartLineNumber}:{variable.Extent.StartColumnNumber}");
                }
            }

            var builder = new StringBuilder(code);

            foreach (var replacement in replacements.OrderByDescending(r => r.StartOffset))
            {
                builder.Remove(replacement.StartOffset, replacement.Length)
                       .Insert(replacement.StartOffset, replacement.Text);
            }

            return builder.ToString();
        }

        private bool ShouldExpand(VariableExpressionAst variable, HashSet<string> drives)
        {
            if (!variable.VariablePath.IsDriveQualified)
            {
                return drives.Contains("variable");
            }

            return drives.Contains(variable.VariablePath.DriveName ?? "variable");
        }

        private sealed class TextReplacement
        {
            public TextReplacement(string text, IScriptExtent extent)
            {
                Text = text;
                StartOffset = extent.StartOffset;
                EndOffset = extent.EndOffset;
            }

            public string Text { get; }

            public int StartOffset { get; }

            public int EndOffset { get; }

            public int Length => EndOffset - StartOffset;
        }

        private sealed class ParseException : Exception
        {
            public ParseException(string message)
                : base(message)
            {
            }
        }
    }
}
