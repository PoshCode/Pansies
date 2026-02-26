using PoshCode.Pansies.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Language;
using System.Text;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Expand", "Variable", DefaultParameterSetName = "Content")]
    public class ExpandVariableCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ParameterSetName = "Path")]
        [Alias("PSPath")]
        public string Path { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Content")]
        public string Content { get; set; }

        [Parameter()]
        public SwitchParameter Unescaped { get; set; }

        [Parameter()]
        public string[] Drive { get; set; } = ["bg", "emoji", "esc", "extra", "fg", "nf"];

        [Parameter(ParameterSetName = "Path")]
        public SwitchParameter InPlace { get; set; }

        [Parameter(ParameterSetName = "Path")]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Content")
            {
                var result = ExpandVariable(Content, "Content");
                WriteObject(result);
                return;
            }

            var resolvedProviderPath = GetResolvedProviderPathFromPSPath(Path, out ProviderInfo provider);
            WriteDebug($"Resolved '{Path}' to {resolvedProviderPath.Length} path(s) in provider '{provider.Name}'");
            foreach (var file in resolvedProviderPath)
            {
                string fullName = file;
                if (provider.Name != "Variable" && provider.Name != "FileSystem" && !fullName.Contains(":"))
                {
                    fullName = $"{provider.Name}:{file}";
                }
                WriteVerbose($"Expanding variables in '{fullName}'");
                string code = GetVariableValue(fullName).ToString();
                WriteDebug($"# {fullName}\n{code}");
                var result = ExpandVariable(code, fullName);

                if (result != null)
                {
                    if (InPlace)
                    {
                        SessionState.PSVariable.Set(fullName, result);
                        if (Passthru)
                        {
                            WriteObject(SessionState.InvokeProvider.Item.Get(fullName), true);
                        }
                    }
                    else
                    {
                        WriteObject(result);
                    }
                }
            }
        }

        private string ExpandVariable(string code, string fullName = null)
        {
            var builder = new StringBuilder(code);
            var replacements = new List<TextReplacement>();
            Ast ast = null;
            ast = Parser.ParseInput(code, fullName, out var tokens, out var parseErrors);
            if (parseErrors.Length > 0)
            {
                WriteError(new ErrorRecord(new Exception($"{parseErrors.Length} Parse Errors in {fullName}, cannot expand."), "ParseErrors", ErrorCategory.InvalidOperation, fullName));
                return null;
            }

            var variables = ast.FindAll(a => a is VariableExpressionAst, true).
                                Cast<VariableExpressionAst>().
                                Where(v => (!v.VariablePath.IsUnqualified ||    // variable:foo is IsUnqualified = False and IsVariable = True
                                            v.VariablePath.IsDriveQualified) && // fg:red is IsDriveQualified = True and IsVariable = False
                                            Drive.Contains(v.VariablePath.DriveName ?? "variable", StringComparer.OrdinalIgnoreCase));

            foreach (var variable in variables)
            {
                try
                {
                    var replacement = GetVariableValue(variable.VariablePath.UserPath).ToString();
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
                        replacements.Add(new TextReplacement('"' + replacement + '"', variable.Extent));
                    }
                }
                catch
                {
                    WriteWarning($"VariableNotFound: '{variable.VariablePath.UserPath}' at {fullName}:{variable.Extent.StartLineNumber}:{variable.Extent.StartColumnNumber}");
                    continue;
                }
            }

            foreach (var replacement in replacements.OrderByDescending(r => r.StartOffset))
            {
                builder.Remove(replacement.StartOffset, replacement.Length).Insert(replacement.StartOffset, replacement.Text);
            }
            return builder.ToString();
        }

        private class TextReplacement(string text, IScriptExtent extent)
        {
            public int StartOffset = extent.StartOffset;
            public int EndOffset = extent.EndOffset;
            public string Text = text;
            public int Length => EndOffset - StartOffset;
        }

    }
}
