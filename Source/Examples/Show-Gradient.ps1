using namespace PoshCode.Pansies
[CmdletBinding()]
param(
    [RgbColor]
    $LeftColor = "DarkBlue",

    [RgbColor]
    $RightColor = "Red",

    [ValidateSet("CMYK","LAB","LUV","HunterLAB","HSL","HSLReverse","RGB","XYZ")]
    $ColorSpace = "LAB"
)

Get-Gradient -Color $LeftColor,$RightColor -ColorSpace $ColorSpace -Flatten |
    ForEach-Object { Write-HostAnsi " " -BackgroundColor $_ -NoNewline}