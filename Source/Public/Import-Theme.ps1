function Import-Theme {
    <#
        .SYNOPSIS
            Import a theme file to style your console window and PowerShell session
    #>
    [CmdletBinding()]
    param(
        # A theme to import (can be the name of an installed PANSIES theme, or the full path to a psd1 file)
        [string]$Name
    )
    if (!$Name.EndsWith(".psd1")) { $Name += ".psd1" }
    $Path = if (!(Test-Path $Name)) {
        Join-Path $PSScriptRoot "Themes" | Join-Path -ChildPath $Name
    } else {
        $Name
    }

    $Theme = Import-Metadata $Path
    Write-Verbose "Imported theme from $Path"
    if ($ConsolePalette = $Theme.ConsolePalette) {
        for ($i = 0; $i -lt $ConsolePalette.Count; $i++) {
            $ConsolePalette[$i] = [PoshCode.Pansies.RgbColor]$ConsolePalette[$i]
        }
        Write-Verbose "Setting the console palette"
        Set-ConsolePalette -Colors $ConsolePalette
    }

    if ($PSReadLine = $Theme.PSReadLine) {
        foreach($key in @($PSReadLine.Colors.Keys)) {
            $PSReadLine.Colors[$key] = ([PoshCode.Pansies.RgbColor]$PSReadLine.Colors[$key]).ToVtEscapeSequence()
        }
        Write-Verbose "Applying PSReadLineOptions"
        Set-PSReadLineOption @PSReadLine
    }


}

