function ConvertFrom-VSCodeTheme {
    <#
        .SYNOPSIS
            Convert a VSCode Theme file into a partial theme
        .DESCRIPTION
            Attempts to generate a PANSIES Powershell theme from a VSCode Theme.

            This feature is still experimental, and so far, includes theming:

            - The ConsoleColor palette (the base 16 ConsoleColors) from the `terminal.ansi*` colors in the VSCode theme
            - The PSReadLine colors (requires PSReadline 2.0.0 beta 2) from various named scopes
            - The PrivateData ConsoleColors (used for foreground of the output streams: Verbose, Error, Warning, Debug, Progress) from various named colors.

            NOTE: for now, even if everything works, there may be some colors for PSReadLine that aren't set, or that are set incorrectly (depending on the theme). If so, please let me know of themes you want to use or of colors that are wrong in the issues at https://GitHub.com/PoshCode/PANSIES/issues
        .EXAMPLE
            ConvertFrom-VSCodeTheme Dark+
            Import-Theme Dark+

            # This example shows how to convert the VSCode Dark+ default theme and then use it in your console.
    #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        # The name of (or full path to) a vscode json theme
        # E.g. 'Dark+' or 'Monokai'
        [Alias("PSPath", "Name")]
        [Parameter(ValueFromPipelineByPropertyName)]
        [string]$Theme,

        # Overwrite any existing theme
        [switch]$Force,

        [switch]$Update,

        # Output the theme after importing it
        [switch]$Passthru,

        [ValidateSet("User", "Machine")]
        [string]$Scope = "User"
    )
    $ThemeSource = FindVsCodeTheme $Theme -ErrorAction Stop

    Write-Verbose "Importing $($ThemeSource.Path)"
    if($PSCmdlet.ShouldProcess($ThemeSource.Path, "Convert to $($ThemeSource.Name).theme.psd1")) {
        # Load the theme file and split the output into colors and tokencolors
        if($ThemeSource.Path.endswith(".json")) {
            $colors, $tokens = (ImportJsonIncludeLast $ThemeSource.Path).Where( {!$_.scope}, 'Split', 2)
        } else {
            $colors, $tokens = (Import-PList $ThemeSource.Path).settings.Where( {!$_.scope}, 'Split', 2)
            $colors = $colors.settings
        }

        # these should come from the colors, rather than the token scopes
        $DefaultTokenColor = GetColorProperty $colors 'editor.foreground', 'foreground', 'terminal.foreground'
        $SelectionColor = GetColorProperty $colors 'editor.selectionBackground', 'editor.selectionHighlightBackground', 'selection'
        $ErrorColor = @(@(GetColorProperty $colors 'errorForeground', 'editorError.foreground') + @(GetColorScopeForeground $tokens 'invalid'))[0]

        # I'm going to need some help figuring out what the best mappings are
        $CommandColor = GetColorScopeForeground $tokens 'support.function'
        $CommentColor = GetColorScopeForeground $tokens 'comment'
        $ContinuationPromptColor = GetColorScopeForeground $tokens 'constant.character'
        $EmphasisColor = GetColorScopeForeground $tokens 'markup.bold','markup.italic','emphasis','strong','constant.other.color', 'markup.heading'
        $KeywordColor = GetColorScopeForeground $tokens '^keyword.control$', '^keyword$', 'keyword.control', 'keyword'
        $MemberColor = GetColorScopeForeground $tokens 'variable.other.object.property', 'member', 'type.property', 'support.function.any-method', 'entity.name.function'
        $NumberColor = GetColorScopeForeground $tokens 'constant.numeric'
        $OperatorColor = GetColorScopeForeground $tokens 'keyword.operator$', 'keyword'
        $ParameterColor = GetColorScopeForeground $tokens 'parameter'
        $StringColor = GetColorScopeForeground $tokens '^string$'
        $TypeColor = GetColorScopeForeground $tokens '^storage.type$','^support.class$', '^entity.name.type.class$', '^entity.name.type$'
        $VariableColor = GetColorScopeForeground $tokens '^variable$', '^entity.name.variable$', '^variable.other$'


        $ThemeOutput = [Ordered]@{
            Name       = $ThemeSource.Name
            PSReadLine = @{
                Colors = @{
                    Command =            $CommandColor
                    Comment =            $CommentColor
                    ContinuationPrompt = $ContinuationPromptColor
                    DefaultToken =       $DefaultTokenColor
                    Emphasis =           $EmphasisColor
                    Error =              $ErrorColor
                    Keyword =            $KeywordColor
                    Member =             $MemberColor
                    Number =             $NumberColor
                    Operator =           $OperatorColor
                    Parameter =          $ParameterColor
                    Selection =          $SelectionColor
                    String =             $StringColor
                    Type =               $TypeColor
                    Variable =           $VariableColor
                }
            }
        }

        # If the VSCode Theme has terminal colors, export those
        if ($colors.'terminal.ansiBrightYellow') {
            Write-Verbose "Exporting ConsoleColors"
            $ThemeOutput['ConsoleColors'] = @(
                    GetColorProperty $colors "terminal.ansiBlack"
                    GetColorProperty $colors "terminal.ansiRed"
                    GetColorProperty $colors "terminal.ansiGreen"
                    GetColorProperty $colors "terminal.ansiYellow"
                    GetColorProperty $colors "terminal.ansiBlue"
                    GetColorProperty $colors "terminal.ansiMagenta"
                    GetColorProperty $colors "terminal.ansiCyan"
                    GetColorProperty $colors "terminal.ansiWhite"
                    GetColorProperty $colors "terminal.ansiBrightBlack"
                    GetColorProperty $colors "terminal.ansiBrightRed"
                    GetColorProperty $colors "terminal.ansiBrightGreen"
                    GetColorProperty $colors "terminal.ansiBrightYellow"
                    GetColorProperty $colors "terminal.ansiBrightBlue"
                    GetColorProperty $colors "terminal.ansiBrightMagenta"
                    GetColorProperty $colors "terminal.ansiBrightCyan"
                    GetColorProperty $colors "terminal.ansiBrightWhite"
                )
            if ($colors."terminal.background") {
                $ThemeOutput['ConsoleBackground'] = GetColorProperty $colors "terminal.background"
            }
            if ($colors."terminal.foreground") {
                $ThemeOutput['ConsoleForeground'] = GetColorProperty $colors "terminal.foreground"
            }
        }

        if (GetColorProperty $colors 'editorWarning.foreground') {
            $ThemeOutput['Host'] = @{
                'PrivateData' = @{
                    WarningForegroundColor  = GetColorProperty $colors 'editorWarning.foreground'
                    ErrorForegroundColor = GetColorProperty $Colors 'editorError.foreground'
                    VerboseForegroundColor = GetColorProperty $Colors 'editorInfo.foreground'
                    ProgressForegroundColor = GetColorProperty $Colors 'notifications.foreground'
                    ProgressBackgroundColor = GetColorProperty $Colors 'notifications.background'
                }
            }
        }

        if ($DebugPreference -in "Continue", "Inquire") {
            $global:colors = $colors
            $global:tokens = $tokens
            $global:Theme = $ThemeOutput
            ${function:global:Get-VSColorScope} = ${function:GetColorScopeForeground}
            ${function:global:Get-VSColor} = ${function:GetColorProperty}
            Write-Debug "For debugging, `$Theme, `$Colors, `$Tokens were copied to global variables, and Get-VSColor and Get-VSColorScope exported."
        }

        if ($ThemeOutput.PSReadLine.Colors.Values -contains $null) {
            Write-Warning "Some PSReadLine color values not set in '$($ThemeSource.Path)'"
        }

        $ThemeOutput | ExportTheme -Name $ThemeSource.Name -Passthru:$Passthru -Scope:$Scope -Force:$Force -Update:$Update
    }
}