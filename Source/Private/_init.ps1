using namespace PoshCode.Pansies
using namespace PoshCode.Pansies.Palettes
using namespace System.Collections.Generic
using namespace System.Collections
using namespace ColorMine.ColorSpaces
using namespace System.Management.Automation
using namespace System.Management.Automation.Language

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

$Accelerators = @{
    "RGBColor" = [PoshCode.Pansies.RgbColor]
    "Entities" = [PoshCode.Pansies.Entities]
}

# IArgumentCompleterFactory only available on PS7+
if ("System.Management.Automation.IArgumentCompleterFactory" -as [type]) {
    Add-Type @"
using System.Management.Automation;
using PoshCode.Pansies.Palettes;
namespace PoshCode.Pansies {
    public class ColorCompleterAttribute : ArgumentCompleterAttribute, IArgumentCompleterFactory {
        public ColorCompleterAttribute(){}
        public IArgumentCompleter Create() {
            return new X11Palette();
        }
    }
}
"@ -ReferencedAssemblies ([psobject].Assembly), ([PoshCode.Pansies.RgbColor].Assembly), "netstandard" -CompilerOptions "-NoWarn:1701"

    $Accelerators["ColorCompleterAttribute"] = [PoshCode.Pansies.ColorCompleterAttribute]

}

$xlr8r = [psobject].assembly.gettype("System.Management.Automation.TypeAccelerators")
$Accelerators.GetEnumerator().ForEach({
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

$script:X11Palette = [X11Palette]::new()
$RgbColorCompleter = {
    param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameters)
    $script:X11Palette.CompleteArgument($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameters)
}

$global:PansiesColorCompleterRegistration = Register-EngineEvent -SourceIdentifier PowerShell.OnIdle {
    foreach ($command in Get-Command -ParameterType RgbColor) {
        foreach ($parameter in $command.Parameters.Values.Where{ $_.ParameterType -eq [RgbColor] }) {
            Register-ArgumentCompleter -CommandName $command.Name -ParameterName $parameter.Name -ScriptBlock $RgbColorCompleter
        }
    }
    Stop-Job $global:PansiesColorCompleterRegistration # This removes the event
    Remove-Variable PansiesColorCompleterRegistration -Scope global
}

Export-ModuleMember -Variable RgbColorCompleter -Function *-* -Cmdlet * -Alias *
