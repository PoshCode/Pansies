function Get-Gradient {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    [OutputType([PoshCode.Pansies.RgbColor[][]],[PoshCode.Pansies.RgbColor[]])]
    param(
        [Parameter(Mandatory, Position=0)]
        [PoshCode.Pansies.RgbColor]$StartColor,

        [Parameter(Mandatory, Position=1)]
        [PoshCode.Pansies.RgbColor]$EndColor,

        [Parameter(Position=2)]
        [Alias("Length","Count","Steps")]
        [int]$Width = $Host.UI.RawUI.WindowSize.Width,

        [Parameter(Position=3)]
        [int]$Height = 1,
        [ValidateSet("CMY","CMYK","LAB","LCH","LUV","HunterLAB","HSL","HSV","HSB","RGB","XYZ","YXY")]
        $ColorSpace = "HunterLab",

        [switch]$Reverse,

        [switch]$Flatten
    )

    $Height = [Math]::Max(1, $Height)
    $Width = [Math]::Max(1, $Width)
    $Colors = new-object PoshCode.Pansies.RgbColor[][] $Height, $Width

    # Simple pythagorean distance
    $Size = [Math]::Sqrt(($Height - 1) * ($Height - 1) + ($Width - 1) * ($Width - 1))

    $Left = $StartColor."To$ColorSpace"()
    $Right = $EndColor."To$ColorSpace"()
    $StepSize = New-Object "${ColorSpace}Color" -Property @{
        Ordinals = $(
            foreach ($i in 0..($Left.Ordinals.Count-1)) {
                ($Right.Ordinals[$i] - $Left.Ordinals[$i]) / $Size
            }
        )
    }
    Write-Verbose "Size: $('{0:N2}' -f $Size) ($Width x $Height) ($($Colors.Length) x $($Colors[0].Length))"
    Write-Verbose "Diff: {$StepSize}"
    Write-Verbose "From: {$Left} $($StartColor.Ordinals)"
    Write-Verbose "To:   {$Right} $($EndColor.Ordinals)"
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
            $D = [Math]::Sqrt(($Line - 1) * ($Line - 1) + ($Column - 1) * ($Column - 1))

            $StepColor = New-Object "${ColorSpace}Color" -Property @{
                Ordinals = $(
                    foreach ($i in 0..$Left.Ordinals.Count) {
                        $Left.Ordinals[$i] + $StepSize.Ordinals[$i] * $D
                    }
                )
            }

            # For colors based on hue rotation, the math is slightly more complex:
            if($RotatingColor) {
                if($StepColor.H -lt 0) {
                    $StepColor.H += 360
                }
                $StepColor.H %= $Ceiling
            }
            $Ordinals1 = $StepColor.Ordinals

            $Colors[$Line - 1][$Column - 1] = $StepColor.ToRgb()
            $Ordinals2 = $Colors[$Line - 1][$Column - 1].Ordinals
            Write-Debug ("Step ${Line},${Column}: {0:N2}, {1:N2}, {2:N2}  =>  {3:N2}, {4:N2}, {5:N2}" -f $Ordinals1[0], $Ordinals1[1], $Ordinals1[2], $Ordinals2[0], $Ordinals2[1], $Ordinals2[2] )
        }
    }

    if ($Flatten) {
        $Colors.GetEnumerator().GetEnumerator()
    } else {
        ,$Colors
    }

}
