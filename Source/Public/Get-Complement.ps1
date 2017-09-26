function Get-Complement {
    <#
    .SYNOPSIS
        Get the Hue complement color

    .DESCRIPTION
        Rotate 180 degrees around the Hue of HSL
    #>
    [CmdletBinding()]
    param(
        # The source color to calculate the complement of
        [Parameter(ValueFromPipeline, Mandatory)]
        [PoshCode.Pansies.RgbColor]$Color,

        # Force the luminance to have "enough" contrast
        [switch]$ForceContrast,

        # If set, output the input $Color before the complement
        [switch]$Passthru
    )
    process {
        if($Passthru) { $Color }
        $Hsl = $Color.ToHsl()
        $Hsl.H = ($Hsl.H + 180) % 360

        if($ForceContrast) {
            $Lab = $Hsl.ToHunterLab()
            $Source = $Color.ToHunterLab()
            $Lab.L = ($Source.L + 50) % 100
            [PoshCode.Pansies.RgbColor]$Lab
        } else {
            [PoshCode.Pansies.RgbColor]$Hsl
        }
    }
}