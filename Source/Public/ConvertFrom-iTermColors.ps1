function ConvertFrom-iTermColors {
    <#
        .SYNOPSIS
            Convert a .itermcolors XML file into a partial theme
        .DESCRIPTION
            Generate a PANSIES PowerShell theme from iTermColors.

            For a collection of iTermColors files you can use, visit https:#ithub.com/mbadolato/iTerm2-Color-Schemes

        .EXAMPLE
            ConvertFrom-iTermColors Argonaut

            Will find the Argonaut.itermcolors file in the furrent directory (or in the module storage path)
     #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        # The name of (or full path to) an XML PList itermcolors scheme
        # If you provide just a name, will search for an .itermcolors file in the current folder and the theme
        [Alias("PSPath", "Name")]
        [Parameter(ValueFromPipelineByPropertyName)]
        [string]$Theme,

        [switch]$Force,

        [switch]$Update,

        [switch]$Passthru,

        [ValidateSet("User", "Machine")]
        [string]$Scope = "User"
    )

    begin {
        # In Windows Color Table order
        $PListColorNames = @(
            "Ansi 0 Color"   # DARK_BLACK
            "Ansi 4 Color"   # DARK_BLUE
            "Ansi 2 Color"   # DARK_GREEN
            "Ansi 6 Color"   # DARK_CYAN
            "Ansi 1 Color"   # DARK_RED
            "Ansi 5 Color"   # DARK_MAGENTA
            "Ansi 3 Color"   # DARK_YELLOW
            "Ansi 7 Color"   # DARK_WHITE
            "Ansi 8 Color"   # BRIGHT_BLACK
            "Ansi 12 Color"  # BRIGHT_BLUE
            "Ansi 10 Color"  # BRIGHT_GREEN
            "Ansi 14 Color"  # BRIGHT_CYAN
            "Ansi 9 Color"   # BRIGHT_RED
            "Ansi 13 Color"  # BRIGHT_MAGENTA
            "Ansi 11 Color"  # BRIGHT_YELLOW
            "Ansi 15 Color"  # BRIGHT_WHITE
        )
    }

    process {
        if (!(Test-Path $Theme)) {
            throw [System.IO.FileNotFoundException]::new("Palette file not found", $Theme)
        }

        if (!($pList = Import-PList $Theme)) {
            return
        }

        $ThemeOutput = @{
            Name = [IO.Path]::GetFileNameWithoutExtension($Theme)
            ConsoleColors = @(
                foreach($color in $PListColorNames) {
                    if(!$pList[$color]) {
                        Wait-Debugger
                        Write-Warning "Missing color $color"
                        $null
                    } else {
                        ConvertToCssColor $pList[$color]
                    }
                }
            )
        }
        if ($pList.ContainsKey("Foreground Color") -and $pList.ContainsKey("Background Color")) {
            $ThemeOutput.ConsoleForeground = ConvertToCssColor $pList["Foreground Color"]
            $ThemeOutput.ConsoleBackground = ConvertToCssColor $pList["Background Color"]
        }
        if ($pList.ContainsKey("Bold Color")) {
            if (!$ThemeOutput['PSReadLine']) {
                $ThemeOutput['PSReadLine'] = @{}
            }
            if (!$ThemeOutput['PSReadLine']['Colors']) {
                $ThemeOutput['PSReadLine']['Colors'] = @{}
            }
            $ThemeOutput['PSReadLine']['Colors']['Emphasis'] = ConvertToCssColor $pList["Bold Color"]
        }
        if ($pList.ContainsKey("Selection Color")) {
            if (!$ThemeOutput['PSReadLine']) {
                $ThemeOutput['PSReadLine'] = @{}
            }
            if (!$ThemeOutput['PSReadLine']['Colors']) {
                $ThemeOutput['PSReadLine']['Colors'] = @{}
            }
            $ThemeOutput['PSReadLine']['Colors']['Selection'] = ConvertToCssColor $pList["Selection Color"]
        }
        if(!($Name = $pList["name"])) {
            $Name = ([IO.Path]::GetFileNameWithoutExtension($Theme))
        }

        $ThemeOutput | ExportTheme -Name $Name -Passthru:$Passthru -Scope:$Scope -Force:$Force -Update:$Update
    }
}