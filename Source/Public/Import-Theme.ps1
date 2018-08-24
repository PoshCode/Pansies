function Import-Theme {
    [CmdletBinding()]
    param(
        # A theme psd1 to import
        [string]$Path
    )
    $Theme = Import-Metadata $Path

    if($PSReadLine = $Theme.PSReadLine) {
        foreach($key in @($PSReadLine.Colors.Keys)) {
            $PSReadLine.Colors[$key] = ([PoshCode.Pansies.RgbColor]$PSReadLine.Colors[$key]).ToVtEscapeSequence()
        }
        Set-PSReadLineOption @PSReadLine
    }

    if($ConsolePalette = $Theme.ConsolePalette) {
        for($i = 0; $i -lt $ConsolePalette.Count; $i++) {
            $ConsolePalette[$i] = [PoshCode.Pansies.RgbColor]$ConsolePalette[$i]
        }
        Set-ConsolePalette -Colors $ConsolePalette
    }

}

