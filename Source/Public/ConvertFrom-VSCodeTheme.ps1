using namespace PoshCode.Pansies
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
    [CmdletBinding()]
    param(
        # The path to a vscode json theme
        # E.g. '~\AppData\Local\Programs\Microsoft VS Code Insiders\resources\app\extensions\theme-defaults\themes\dark_plus.json'
        [string[]]$Path
    )

    function ImportJsonIncludeLast {
        # Import VSCode json themes, including any included themes
        param([string[]]$Path)

        # take the first
        $themeFile, $Path = $Path
        $theme = Get-Content $themeFile | ConvertFrom-Json

        # Output all the colors or token colors
        if($theme.colors) { $theme.colors }
        if($theme.tokenColors) { $theme.tokenColors }

        # Recurse includes
        if ($theme.include) {
            $Path += $themeFile | Split-Path | Join-Path -Child $theme.include | convert-path
        }
        if ($Path) {
            ImportJsonIncludeLast $Path
        }
    }

    # Load the theme file and split the output into colors and tokencolors
    $colors, $tokens = (ImportJsonIncludeLast $Path).Where({!$_.scope},'Split', 2)

    function GetColorScope {
        # Search the tokens for a scope name with a foreground color
        param(
            [Array]$tokens,
            [string[]]$name
        )
        # Since we loaded the themes in order of prescedence, we take the first match that has a foreground color
        foreach ($pattern in $name) {
            foreach ($token in $tokens) {
                if (($token.scope -match $pattern) -and $token.settings.foreground) {
                    $token.settings.foreground
                    return
                }
            }
        }
    }

    # these should come from the colors, rather than the token scopes
    $DefaultTokenColor = [RgbColor]$colors.'editor.foreground'
    $SelectionColor = [RgbColor]($colors.'editor.selectionHighlightBackground' -replace '.*(.{6})$', '#$1')

    # I'm going to need some help figuring out what the best mappings are
    $CommandColor = GetColorScope $tokens 'support.function'
    $CommentColor = GetColorScope $tokens 'comment'
    $ContinuationPromptColor = GetColorScope $tokens 'constant.character'
    $EmphasisColor = GetColorScope $tokens 'markup.bold','emphasis','strong'
    $ErrorColor = GetColorScope $tokens 'invalid'
    $KeywordColor = GetColorScope $tokens '^keyword.control$'
    $MemberColor = GetColorScope $tokens 'variable.other.object.property','member', 'type.property'
    $NumberColor = GetColorScope $tokens 'constant.numeric'
    $OperatorColor = GetColorScope $tokens 'keyword.operator$'
    $ParameterColor = GetColorScope $tokens 'parameter'
    $StringColor = GetColorScope $tokens '^string$'
    $TypeColor = GetColorScope $tokens '^storage.type$'
    $VariableColor = GetColorScope $tokens '^variable$', '^entity.name.variable$'

    @{
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