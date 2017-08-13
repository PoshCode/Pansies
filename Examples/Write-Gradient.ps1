using namespace PoshCode.Pansies
[CmdletBinding()]
param(
    [Object]$Object = "Hello World",

    [RgbColor]
    $StartColor = "xt33",

    [RgbColor]
    $EndColor = "xt199",

    [ValidateSet("CMYK","LAB","LUV","HunterLAB","HSL","HSLReverse","HSV","RGB","XYZ")]
    $ColorSpace = "LAB"
)
process {
    $Text = [Text]::ConvertToString($Object)

    $Color = Get-Gradient -Start $StartColor -End $EndColor -Height 1 -Width $Text.Length -ColorSpace $ColorSpace -Flatten

    for($c = 0; $c -lt $Color.Length; $c++) {
        # Fun twist: use HSL or LAB to pick a darker version of the color:
        $LAB = ([RgbColor]$Color[$c]).ToHunterLab()
        # Invert the color
        $LAB.L = 100 - $LAB.L
        # And then push it to make it darker or lighter
        if($LAB.L -gt 50) {
            $LAB.L += (100 - $Lab.L) * .4
        } else {
            $LAB.L -= $LAB.L * .4
        }
        $fg = $LAB.ToRgb()

        $LAB | Out-String | Write-Verbose
        # # Or just rotate the Hue:
        # $HSL = ([RgbColor]$Color[$c]).ToHsl()
        # $HSL.H = ($HSL.H + 180) % 360
        # $HSL.S = 100
        # if($HSL.L -gt .4) {
        #     $HSL.L *= .75
        # } else {
        #     $HSL.L *= 1.5
        # }
        # $fg = $HSL.ToRgb()

        Write-Host $Text[$c] -BackgroundColor $Color[$c] -ForegroundColor $fg -NoNewline
    }

    Write-Host
}