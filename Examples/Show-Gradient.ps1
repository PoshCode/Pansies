using namespace PoshCode.Pansies
[CmdletBinding()]
param(
    [RgbColor]
    $StartColor = "DarkBlue",

    [RgbColor]
    $EndColor = "Red",

    [ValidateSet("LAB", "LUV", "HunterLAB", "HSL", "HSLReverse", "RGB", "XYZ")]
    $ColorSpace = "LAB",

    [int]$Width = $Host.UI.RawUI.WindowSize.Width,

    [int]$Height = $Host.UI.RawUI.WindowSize.Height
)

Get-Gradient -StartColor $StartColor -EndColor $EndColor -ColorSpace $ColorSpace -Width $Width -Height $Height
| ForEach-Object { $_
    | ForEach-Object { $_
        | ForEach-Object {
            Write-Host " " -Background $_ -NoNewline
        }
        Write-Host
    }
}
