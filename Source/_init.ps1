using namespace ColorMine.ColorSpaces
using namespace PoshCode.Pansies
using namespace ColorMine.Palettes
using namespace PoshCode.Pansies.Palettes
using namespace System.Collections.Generic

# On first import, if HostPreference doesn't exist, set it and strongly type it
if(!(Test-Path Variable:HostPreference) -or $HostPreference -eq $null) {
    [System.Management.Automation.ActionPreference]$global:HostPreference = "Continue"
}

Set-Variable HostPreference -Description "Dictates the action taken when a host message is delivered" -Visibility Public -Scope Global

if(-not $IsLinux -and -not $IsMacOS) {
    [PoshCode.Pansies.Console.WindowsHelper]::EnableVirtualTerminalProcessing()
}

if(Get-Command Add-MetadataConverter -ErrorAction SilentlyContinue) {
    Add-MetadataConverter @{
        RgbColor = { [PoshCode.Pansies.RgbColor]$args[0] }
        [PoshCode.Pansies.RgbColor] = { "RgbColor '$_'" }
    }
}