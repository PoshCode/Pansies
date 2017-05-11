using namespace PoshCode.Pansies

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
        $fg = ([RgbColor]$Color[$c]).ToHunterLab().ForEach{
            $_.L *= .4
            $_
        }.ToRgb()

        Write-Host $Text[$c] -BackgroundColor $Color[$c] -ForegroundColor $fg -NoNewline
    }

    Write-Host
}