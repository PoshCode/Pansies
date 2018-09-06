function Convert-ConsolePalette {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding(SupportsShouldProcess, ConfirmImpact="Medium", DefaultParameterSetName = "Dark")]
    param(
        # How much to shift the dark colors. Positive values make the colors brighter, negative values make them darker
        [Parameter(Mandatory, ParameterSetName = "Dark", Position = 0)]
        [int]$DarkShift,

        # How much to shift the bright colors. Positive values make the colors brighter, negative values make them darker
        [Parameter(Mandatory, ParameterSetName = "Bright")]
        [int]$BrightShift,

        # By default, the colors are modified in-place. If copy is set:
        # - the dark colors start with the value of the bright colors
        # - the light colors start at the value of the dark colors
        [switch]$Copy
    )
    $Palette = Get-ConsolePalette
    for($i=0;$i -lt 8; $i++){
        $Dark  = $Palette[$i].ToHunterLab()
        $Light = $Palette[$i+8].ToHunterLab()

        if ($BrightShift) {
            if ($Copy) {
                $Light = $Dark.ToHunterLab()
            }
            $Light.L  += $BrightShift
        }

        if ($DarkShift) {
            if ($Copy) {
                $Dark  = $Light.ToHunterLab()
            }
            $Dark.L   += $DarkShift
        }

        $Palette[$i]   = [PoshCode.Pansies.RgbColor]$Dark.ToRgb()
        $Palette[$i+8] = [PoshCode.Pansies.RgbColor]$Light.ToRgb()
    }

    ShowPreview $Palette -Tiny

    if($PSCmdlet.ShouldProcess("Save Theme")) {
        Set-ConsolePalette $Palette
    }
}