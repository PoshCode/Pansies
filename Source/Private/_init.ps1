using namespace PoshCode.Pansies
using namespace ColorMine.ColorSpaces

if(-not $IsCoreCLR) {
    Add-Type -Path $PSScriptRoot\lib\net452\Pansies.dll
} else {
    Add-Type -Path $PSScriptRoot\lib\netstandard1.6\Pansies.dll
}
