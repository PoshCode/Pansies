function ConvertFrom-iTermColors {
    <#
        .SYNOPSIS
            Convert a .itermcolors XML file into a partial theme
        .DESCRIPTION
            Generate a PANSIES PowerShell theme from iTermColors.

            For a collection of iTermColors files you can use, visit https://github.com/mbadolato/iTerm2-Color-Schemes

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

        [switch]$Passthru,

        [ValidateSet("User", "Machine")]
        [string]$Scope = "User"
    )

    process {
        if (Test-Path $Theme) {
            $ThemeFile = Get-Item $Theme
        } else {
            # Make sure that it ends in .itermcolors
            $ThemeName = [IO.Path]::GetFileNameWithoutExtension($Theme) + ".itermcolors"

            if(!($ThemeFile = Get-ChildItem -Filter $ThemeName -ErrorAction SilentlyContinue)) {
                $ThemeFile = Get-ChildItem $(Get-ConfigurationPath -Scope $Scope) -Filter $ThemeName -Recurse -ErrorAction SilentlyContinue
            }
        }

        if(!(Test-Path $ThemeFile.FullName)) {
            Write-Error "Cannot file '$($ThemeFile.FullName)'"
        } else {
            try {
                $Palette = [PoshCode.Pansies.Parsers.PListParser]::Parse($ThemeFile.FullName);
            }
            catch [System.IO.FileNotFoundException]
            {
                $PSCmdlet.ThrowTerminatingError( [System.Management.Automation.ErrorRecord]::new($_, "PaletteFileNotFound", "ObjectNotFound", $ThemeFile.FullName));
            }
            catch
            {
                $Ex = if($_ -is [Exception]) { $_ } else { $_.Exception }
                $PSCmdlet.ThrowTerminatingError( [System.Management.Automation.ErrorRecord]::new($Ex, "PaletteParseException", "ParserError", $ThemeFile.FullName));
            }

            if (!$Palette)
            {
                $PSCmdlet.ThrowTerminatingError( [System.Management.Automation.ErrorRecord]::new(
                    [System.IO.InvalidDataException]::new("Unknown Palette file format: '$($ThemeFile.FullName)'"),
                     "PaletteParseFailure", "NotImplemented", $ThemeFile.FullName));
            }
            $ThemeOutput = @{
                ConsoleColors = @($Palette[0..15].ForEach({$_.ToString()}))
            }
        }

        if ($Palette.Count -ge 18) {
            $ThemeOutput['ConsoleBackground'] = $Palette[16].ToString()
            $ThemeOutput['ConsoleForeground'] = $Palette[17].ToString()
        }

        $ThemeOutput | ExportTheme -Name $ThemeFile.BaseName -Passthru:$Passthru -Scope:$Scope -Force:$Force
    }
}