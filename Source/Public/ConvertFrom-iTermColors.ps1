function ConvertFrom-iTermColors {
    <#
        .SYNOPSIS
            Convert a .itermcolors XML file into a partial theme
        .DESCRIPTION
            Generate a PANSIES PowerShell theme from iTermColors.

            For a collection of iTermColors files you can use, visit https://github.com/mbadolato/iTerm2-Color-Schemes

        .EXAMPLE
            ConvertFrom-iTermColors Argonaut
     #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        # The name of (or full path to) an XML PList itermcolors scheme
        # If you provide just a name, will search for an .itermcolors file in the current folder and the theme
        [Alias("PSPath", "Name")]
        [Parameter(ValueFromPipelineByPropertyName)]
        [string]$Theme,

        [switch]$Force,

        [switch]$Passthru
    )

    process {
        if (Test-Path $Theme) {
            $ThemeFile = Get-Item $Theme
        } else {
            # Make sure that it ends in .itermcolors
            $ThemeName = [IO.Path]::GetFileNameWithoutExtension($Theme) + ".itermcolors"

            if(!($ThemeFile = Get-ChildItem -Filter $ThemeName -ErrorAction SilentlyContinue)) {
                $ThemeFile = Get-ChildItem $PSScriptRoot -Filter $ThemeName -Recurse -ErrorAction SilentlyContinue
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
                $PSCmdlet.ThrowTerminatingError( [System.Management.Automation.ErrorRecord]::new($_, "PaletteParseException", "ParseError", $ThemeFile.FullName));
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

        if($Palette.Count -ge 18) {
            $ThemeOutput['ConsoleBackground'] = $Palette[16].ToString()
            $ThemeOutput['ConsoleForeground'] = $Palette[17].ToString()
        }
        if($PassThru) {
            $ThemeOutput | Add-Member NoteProperty Name $ThemeFile.BaseName -Passthru
        }
    }
}