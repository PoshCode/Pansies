using namespace PoshCode.Pansies
using namespace ColorMine.ColorSpaces

if(-not $IsCoreCLR) {
    Import-Module $PSScriptRoot\lib\net451\Pansies.dll
} else {
    Import-Module $PSScriptRoot\lib\netstandard1.6\Pansies.dll
}

if(-not $IsLinux) {
    [PoshCode.Pansies.Console.WindowsHelper]::EnableVirtualTerminalProcessing()
}
