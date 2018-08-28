function Export-Theme {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    param(
        # The path to export the current settings to
        [Parameter(ValueFromPipeline, Mandatory)]
        [string]$Path,


        [string]$Update
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