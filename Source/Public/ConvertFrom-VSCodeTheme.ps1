function ConvertFrom-VSCodeTheme {
    <#
        .SYNOPSIS
            Convert a VSCode Theme file into a partial theme
        .EXAMPLE
            $theme = ConvertFrom-VSCodeTheme '~\AppData\Local\Programs\*Code*\resources\app\extensions\theme-defaults\themes\dark_plus.json'

            foreach ($color in $theme.Keys) {
                $R,$G,$B = $theme.$color -replace "#" -split "(?<=^.{2}|^.{4})" | % { [Convert]::ToByte($_,16) };
                "$([char]27)[38;2;$R;$G;${B}m $color $($theme.$color) "
            }

            # This example shows a demo of the Dark Plus theme from VS Code (if you've installed it to the new user location)
    #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        # The name of (or full path to) a vscode json theme
        # E.g. 'Dark+' or 'Monokai'
        [string]$Theme,

        [switch]$Force,

        [switch]$Passthru
    )

    if(!(Test-Path $Theme)) {
        $Theme = FindVsCodeTheme ([IO.Path]::GetFileName($Theme) -replace ".json$") -ErrorAction Stop
    }

    Write-Verbose "Importing $Theme"
    # Load the theme file and split the output into colors and tokencolors
    $colors, $tokens = (ImportJsonIncludeLast $Theme).Where( {!$_.scope}, 'Split', 2)

    # these should come from the colors, rather than the token scopes
    $DefaultTokenColor = GetColor $colors 'editor.foreground', 'foreground'
    $SelectionColor = GetColor $colors 'editor.selectionBackground', 'editor.selectionHighlightBackground'
    $ErrorColor = @(@(GetColor $colors 'errorForeground', 'editorError.foreground') + @(GetColorScope $tokens 'invalid'))[0]

    # I'm going to need some help figuring out what the best mappings are
    $CommandColor = GetColorScope $tokens 'support.function'
    $CommentColor = GetColorScope $tokens 'comment'
    $ContinuationPromptColor = GetColorScope $tokens 'constant.character'
    $EmphasisColor = GetColorScope $tokens 'markup.bold','emphasis','strong'
    $KeywordColor = GetColorScope $tokens '^keyword.control$'
    $MemberColor = GetColorScope $tokens 'variable.other.object.property','member', 'type.property'
    $NumberColor = GetColorScope $tokens 'constant.numeric'
    $OperatorColor = GetColorScope $tokens 'keyword.operator$', 'keyword'
    $ParameterColor = GetColorScope $tokens 'parameter'
    $StringColor = GetColorScope $tokens '^string$'
    $TypeColor = GetColorScope $tokens '^storage.type$'
    $VariableColor = GetColorScope $tokens '^variable$', '^entity.name.variable$'


    $ThemeOutput = [Ordered]@{
        Name       = [IO.Path]::GetFileNameWithoutExtension($Theme)
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
                GetColor $colors "terminal.ansiBlack"
                GetColor $colors "terminal.ansiRed"
                GetColor $colors "terminal.ansiGreen"
                GetColor $colors "terminal.ansiYellow"
                GetColor $colors "terminal.ansiBlue"
                GetColor $colors "terminal.ansiMagenta"
                GetColor $colors "terminal.ansiCyan"
                GetColor $colors "terminal.ansiWhite"
                GetColor $colors "terminal.ansiBrightBlack"
                GetColor $colors "terminal.ansiBrightRed"
                GetColor $colors "terminal.ansiBrightGreen"
                GetColor $colors "terminal.ansiBrightYellow"
                GetColor $colors "terminal.ansiBrightBlue"
                GetColor $colors "terminal.ansiBrightMagenta"
                GetColor $colors "terminal.ansiBrightCyan"
                GetColor $colors "terminal.ansiBrightWhite"
            )
        if ($colors."terminal.background") {
            $ThemeOutput['ConsoleBackground'] = GetColor $colors "terminal.background"
        }
        if ($colors."terminal.foreground") {
            $ThemeOutput['ConsoleForeground'] = GetColor $colors "terminal.foreground"
        }
    }

    if ($colors.'editorWarning.foreground') {
        $ThemeOutput['HostColors'] = @{
            WarningForegroundColor  = GetColor $colors 'editorWarning.foreground'
            ErrorForegroundColor = GetColor $Colors 'editorError.foreground'
            VerboseForegroundColor = GetColor $Colors 'editorInfo.foreground'
            ProgressForegroundColor = GetColor $Colors 'notifications.foreground'
            ProgressBackgroundColor = GetColor $Colors 'notifications.background'
        }
    }

    if ($DebugPreference -in "Continue", "Inquire") {
        $global:colors = $colors
        $global:tokens = $tokens
        $global:Theme = $ThemeOutput
        ${function:global:Get-VSColorScope} = ${function:GetColorScope}
        ${function:global:Get-VSColor} = ${function:GetColor}
        Write-Debug "For debugging, `$Theme, `$Colors, `$Tokens were copied to global variables, and Get-VSColor and Get-VSColorScope exported."
    }

    if ($ThemeOutput.PSReadLine.Colors.Values -contains $null) {
        Write-Warning "Some PSReadLine color values not set in '$Theme'"
    }

    $NativeThemePath = Join-Path $PSScriptRoot "Themes\$([IO.Path]::GetFileNameWithoutExtension($Theme)).psd1"
    if(Test-Path $NativeThemePath) {
        if($Force -or $PSCmdlet.ShouldContinue("Overwrite $NativeThemePath?", "Theme exists")) {
            Write-Verbose "Exporting to $NativeThemePath"
            $ThemeOutput | Export-Metadata $NativeThemePath
        }
    } else {
        Write-Verbose "Exporting to $NativeThemePath"
        $ThemeOutput | Export-Metadata $NativeThemePath
    }
    if($PassThru) {
        $Colors
    }
}