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

Get-Gradient $LeftColor $RightColor -ColorSpace $ColorSpace -Flatten |
    ForEach-Object { Write-Host " " -BackgroundColor $_ -NoNewline}