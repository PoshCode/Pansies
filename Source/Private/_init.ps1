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

$xlr8r = [psobject].assembly.gettype("System.Management.Automation.TypeAccelerators")
@{
    "RGBColor" = [PoshCode.Pansies.RgbColor]
    "Entities" = [PoshCode.Pansies.Entities]
}.GetEnumerator().ForEach({
    $Name = $_.Key
    $Type = $_.Value
    if ($xlr8r::AddReplace) {
        $xlr8r::AddReplace( $Name, $Type)
    } else {
        $null = $xlr8r::Remove( $Name )
        $xlr8r::Add( $Name, $Type)
    }
    trap [System.Management.Automation.MethodInvocationException] {
        if ($xlr8r::get.keys -contains $Name) {
            if ($xlr8r::get[$Name] -ne $Type) {
                Write-Error "Cannot add accelerator [$Name] for [$($Type.FullName)]n                  [$Name] is already defined as [$($xlr8r::get[$Name].FullName)]"
            }
            Continue;
        }
        throw
    }
})