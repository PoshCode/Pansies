using namespace PoshCode.Pansies
using namespace ColorMine.ColorSpaces

# On first import, if HostPreference doesn't exist, set it and strongly type it
if (!(Test-Path Variable:HostPreference) -or $null -eq $HostPreference) {
    [System.Management.Automation.ActionPreference]$global:HostPreference = "Continue"
}

Set-Variable HostPreference -Description "Dictates the action taken when a host message is delivered" -Visibility Public -Scope Global

if(-not $IsLinux -and -not $IsMacOS) {
    [PoshCode.Pansies.NativeMethods]::EnableVirtualTerminalProcessing()
}

if(Get-Command Add-MetadataConverter -ErrorAction Ignore) {
    Add-MetadataConverter @{
        RgbColor = { [PoshCode.Pansies.RgbColor]$args[0] }
        [PoshCode.Pansies.RgbColor] = { "RgbColor '$_'" }
    }
}
