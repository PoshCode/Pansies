using namespace PoshCode.Pansies
using namespace ColorMine.ColorSpaces
function Get-Gradient {
    <#
    .Synopsis
        Get a range of colors from one or more colors
    #>
    [CmdletBinding()]
    param(
        # One or more colors to generate a gradient from
        [Parameter(Mandatory)]
        [RgbColor[]]$Color,

        [int]$Height = $Host.UI.RawUI.WindowSize.Height,

        [int]$Width = $Host.UI.RawUI.WindowSize.Width,

        [ValidateSet("CMY","CMYK","LAB","LCH","LUV","HunterLAB","HSL","HSV","HSB","RGB","XYZ","YXY")]
        $ColorSpace = "HunterLab",

        # For color spaces with Hue (HSL, HSV), setting this generates the gradient the long way
        [switch]$Reverse,

        [switch]$Flatten
    )
    $Height = [Math]::Max(1, $Height)
    $Width = [Math]::Max(1, $Width)
    $Colors = new-object Rgb[][] $Height, $Width
    $C = [PSCustomObject]@{R = 0; G = 0; B = 0}
    # If we're not doing a color scale, we can return immediately:
    if ($Color.Count -eq 1) {
        for ($r = 0; $r -lt $Height; $r++) {
            for ($c = 0; $c -lt $Width; $c++) {
                $Colors[$r][$c] = $Color[0]
            }
        }
    }
    else {
        # Simple pythagorean distance
        $Size = [Math]::Sqrt($Height * $Height + $Width * $Width)

        $Left = $Color[0]."To$ColorSpace"()
        $Right = $Color[-1]."To$ColorSpace"()
        $StepSize = New-Object "${ColorSpace}Color" -Property @{
            Ordinals = $(
                foreach ($i in 0..($Left.Ordinals.Count-1)) {
                    ($Left.Ordinals[$i] - $Right.Ordinals[$i]) / $Size
                }
            )
        }
        Write-Verbose "Size: $('{0:N2}' -f $Size)"
        Write-Verbose "Diff: {$StepSize}"
        Write-Verbose "From: {$Left}"
        Write-Verbose "To:   {$Right}"
        # For colors based on hue rotation, the math is slightly more complex:
        [bool]$RotatingColor = $StepSize | Get-Member H

        if($RotatingColor) {
            [Bool]$Change = [Math]::Abs($StepSize.H) -ge 180 / $Size
            if ($Reverse) { $Change = !$Change }
            if ($Change) {
                $StepSize.H = if ($StepSize.H -gt 0) {
                    $StepSize.H - 360 / $Size
                } else {
                    $StepSize.H + 360 / $Size
                }
            }
            $Ceiling = 360
        }

        for ($Line = 1; $Line -le $Height; $Line++) {
            for ($Column = 1; $Column -le $Width; $Column++) {
                $D = [Math]::Sqrt($Line * $Line + $Column * $Column)

                $StepColor = New-Object "${ColorSpace}Color" -Property @{
                    Ordinals = $(
                        foreach ($i in 0..$Left.Ordinals.Count) {
                            $Left.Ordinals[$i] - $StepSize.Ordinals[$i] * $D
                        }
                    )
                }

                # For colors based on hue rotation, the math is slightly more complex:
                if($RotatingColor) { $StepColor.H %= $Ceiling }
                Write-Debug "Step ${Line},${Column}: $StepColor"

                $Colors[$Line - 1][$Column - 1] = $StepColor.ToRgb()
                Write-Debug "Step: $($Colors[$Line - 1][$Column - 1])"
            }
        }
    }

    if ($Flatten) {
        $Colors.GetEnumerator().GetEnumerator()
    } else {
        ,$Colors
    }

}
