using namespace PoshCode.Pansies
using namespace ColorMine.ColorSpaces

if(-not $IsLinux -and -not $IsMacOS) {
    [PoshCode.Pansies.Console.WindowsHelper]::EnableVirtualTerminalProcessing()
}

if(Get-Command Add-MetadataConverter -ErrorAction SilentlyContinue) {
    Add-MetadataConverter @{
        RgbColor = { [PoshCode.Pansies.RgbColor]$args[0] }
        [PoshCode.Pansies.RgbColor] = { "RgbColor '$_'" }
    }
}