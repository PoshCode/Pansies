
function ConvertFrom-VSCodeJsonTheme {
    [CmdletBinding()]
    param(
        # The path to a vscode json theme
        # E.g. "~\AppData\Local\Programs\Microsoft VS Code Insiders\resources\app\extensions\theme-defaults\themes\dark_plus.json"
        [string[]]$Path
    )

    function ImportNestedFirst {
        param([string[]]$Path)
        if(!$Path){return}

        $themeFile, $Path = $Path
        $theme = Get-Content $themeFile | ConvertFrom-Json

        # Recurse first
        if($theme.include) {
            $Path += $themeFile | Split-Path | Join-Path -Child $theme.include | convert-path
        }
        ImportNestedFirst $Path

        # Then output
        if($theme.colors) { $theme.colors }
        if($theme.tokenColors) { $theme.tokenColors }
    }

    function GetColor {
        param(
            $tokens = $tokens, 
            $name
        )
        [PoshCode.Pansies.RgbColor]@($tokens.where{$_.scope -match $name}.settings.foreground)[-1]
    }

    $colors, $tokens = (ImportNestedFirst $Path).Where({!$_.scope},"Split", 2)

    # I'm going to need some help figuring out what this mapping should be
    $CommandColor = GetColor $tokens "support.function"
    $CommentColor = GetColor $tokens "comment"
    $ContinuationPromptColor = GetColor $tokens "constant.character" 
    $DefaultTokenColor = [PoshCode.Pansies.RgbColor]$colors."editor.foreground"
    $EmphasisColor = GetColor $tokens "markup.bold|emphasis|strong" 
    $ErrorColor = GetColor $tokens "invalid" 
    $KeywordColor = GetColor $tokens "^keyword.control$" 
    $MemberColor = GetColor $tokens "member|type.property|variable.other.object.property" 
    $NumberColor = GetColor $tokens "constant.numeric" 
    $OperatorColor = GetColor $tokens "keyword.operator$" 
    $ParameterColor = GetColor $tokens "parameter" 
    $SelectionColor = [PoshCode.Pansies.RgbColor]($colors."editor.selectionHighlightBackground" -replace '.*(.{6})$', '#$1')
    $StringColor = GetColor $tokens "^string$"
    $TypeColor = GetColor $tokens "^storage.type$"
    $VariableColor = GetColor $tokens "^variable$|^entity.name.variable$" 

    @{
        PSReadLine = @{
            Colors = @{
                Command = [string]$CommandColor
                Comment = [string]$CommentColor
                ContinuationPrompt = [string]$ContinuationPromptColor
                DefaultToken = [string]$DefaultTokenColor
                Emphasis = [string]$EmphasisColor
                Error = [string]$ErrorColor
                Keyword = [string]$KeywordColor
                Member = [string]$MemberColor
                Number = [string]$NumberColor
                Operator = [string]$OperatorColor
                Parameter = [string]$ParameterColor
                Selection = [string]$SelectionColor
                String = [string]$StringColor
                Type = [string]$TypeColor
                Variable = [string]$VariableColor
            }
        }
    }
}