function Get-Complement {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    [OutputType([PoshCode.Pansies.RgbColor])]
    param(
        # The source color to calculate the complement of
        [Parameter(ValueFromPipeline, Mandatory, Position = 0)]
        [PoshCode.Pansies.RgbColor]$Color,

        # Force the luminance to have "enough" contrast
        [switch]$ForceContrast,

        # Assume there are only 16 colors
        [switch]$ConsoleColor,

        # If set, output the input $Color before the complement
        [switch]$Passthru
    )
    process {
        if($ConsoleColor) {
            $Color = $Color.ConsoleColor
        }

        if($Passthru) { $Color }

        if($ConsoleColor) {
            if($Color.ToHunterLab().L -lt 50) {
                [PoshCode.Pansies.RgbColor][ConsoleColor]::White
            } else {
                [PoshCode.Pansies.RgbColor][ConsoleColor]::Black
            }
        } else {
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
}