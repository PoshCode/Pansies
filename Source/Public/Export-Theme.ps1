function Export-Theme {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    param(
        # The name of the theme to export the current settings to
        [Parameter(Position = 0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$Name,

        [string]$Update,

        [switch]$Force,

        [switch]$Passthru,

        [ValidateSet("User", "Machine")]
        [string]$Scope = "User"
    )
    end {

        $ConsoleColors = (Get-ConsolePalette -AddScreenAndPopup)
        $Theme = [Ordered]@{
            Name = "$Name"
            # Force the colors to be saved as simple '#RRGGBB' instead of native (RgbColor '#RRGGBB')
            ConsoleForeground = $ConsoleColors[16].ToString()
            ConsoleBackground = $ConsoleColors[17].ToString()
            PopupForeground   = $ConsoleColors[18].ToString()
            PopupBackground   = $ConsoleColors[19].ToString()
            ConsoleColors     = $ConsoleColors[0..15].ForEach({$_.ToString()})
        }

        if($Host.Name -eq "ConsoleHost") {
            $Theme['Host'] = @{
                PrivateData = ConvertFrom-Metadata ($Host.PrivateData | Select-Object * | ConvertTo-Metadata -AsHashtable)
            }
        }

        if ((Get-Module PSReadLine).Version -ge "2.0.0") {
            $Theme['PSReadLine'] = @{
                # TODO: convert escape sequences to RGBColor values (e.g. 30's and 90's to colors, including 38;2;R;G;B;)
                Colors = ConvertFrom-Metadata -Ordered @(
                    Get-PSReadLineOption | Select-Object -Property @(
                        @{Name = "ContinuationPrompt"; Expression = { $_.ContinuationPromptColor }}
                        @{Name = "Parameter"; Expression = { $_.ParameterColor }}
                        @{Name = "Member"; Expression = { $_.MemberColor }}
                        @{Name = "Command"; Expression = { $_.CommandColor }}
                        @{Name = "Operator"; Expression = { $_.OperatorColor }}
                        @{Name = "Emphasis"; Expression = { $_.EmphasisColor }}
                        @{Name = "Selection"; Expression = { $_.SelectionColor }}
                        @{Name = "Variable"; Expression = { $_.VariableColor }}
                        @{Name = "Type"; Expression = { $_.TypeColor }}
                        @{Name = "Keyword"; Expression = { $_.KeywordColor }}
                        @{Name = "String"; Expression = { $_.StringColor }}
                        @{Name = "Error"; Expression = { $_.ErrorColor }}
                        @{Name = "Number"; Expression = { $_.NumberColor }}
                        @{Name = "Default"; Expression = { $_.DefaultTokenColor }}
                        @{Name = "Comment"; Expression = { $_.CommentColor }}
                    ) | ConvertTo-Metadata -AsHashtable )
            }
        }

        if($Update) {
            $Theme = ImportTheme $Name -ErrorAction SilentlyContinue | Update-Object $Theme
        }

        $Theme | ExportTheme -Name $Name -Passthru:$Passthru -Scope:$Scope -Force:$Force
    }
}