using namespace PoshCode.Pansies

param(
    [Object]$Object,

    [RgbColor]
    $LeftColor = "Cyan",

    [RgbColor]
    $RightColor = "Red",

    [ValidateSet("CMYK","LAB","LUV","HunterLAB","HSL","HSLReverse","HSV","RGB","XYZ")]
    $ColorSpace = "LAB"
)
process {
    $Text = [Text]::ConvertToString($Object)

    $Color = Get-Gradient -Color $LeftColor,$RightColor -Height 1 -Width $Text.Length -ColorSpace $ColorSpace -Flatten

    for($c = 0; $c -lt $Color.Length; $c++) {
        Write-HostAnsi $Text[$c] -ForegroundColor $Color[$c] -NoNewline
    }

    Write-HostAnsi
}