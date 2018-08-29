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
        if($Update) {
            $Theme = ImportTheme $Name -ErrorAction SilentlyContinue
        }
        if(!$Theme) {
            $Theme = @{}
        }
        $Theme = $Theme | Update-Object @{
            ConsoleColors = (Get-ConsolePalette).ForEach({$_.ToString()})
        }

        $Theme = $Theme | Update-Object @{
            PSReadLine = @{
                # TODO: convert escape sequences to RGBColor values (e.g. 30's and 90's to colors, including 38;2;R;G;B;)
                Colors = ConvertFrom-Metadata (Get-PSReadLineOption | Select-Object *Color | ConvertTo-Metadata -AsHashtable)
            }
        }

        if($Host.Name -eq "ConsoleHost") {

            $Theme = $Theme | Update-Object @{
                Host = @{
                    PrivateData = ConvertFrom-Metadata ($Host.PrivateData | Select-Object * | ConvertTo-Metadata -AsHashtable)
                }
            }
        }

        $Theme | ExportTheme -Name $Name -Passthru:$Passthru -Scope:$Scope -Force:$Force
    }
}