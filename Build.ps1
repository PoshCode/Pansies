#requires -Module Configuration, @{ ModuleName = "ModuleBuilder"; ModuleVersion = "1.6.0" }

[CmdletBinding()]
param(
    [ValidateSet("Release","Debug")]
    $Configuration = "Release",

    # The version of the output module
    [Alias("ModuleVersion","Version")]
    [string]$SemVer
)
Push-Location $PSScriptRoot -StackName BuildTestStack

if (!$SemVer -and (Get-Command gitversion -ErrorAction Ignore)) {
    $PSBoundParameters['SemVer'] = $SemVer = gitversion -showvariable nugetversion
}

try {
    $BuildTimer = New-Object System.Diagnostics.Stopwatch
    $BuildTimer.Start()
    $ErrorActionPreference = "Stop"

    $Module = Build-Module -Passthru -SemVer $SemVer
    $Folder  = Split-Path $Module.Path

    if (!$SkipBinaryBuild) {
        Write-Host "##  Compiling Pansies binary module" -ForegroundColor Cyan
        # dotnet restore
        dotnet build -c $Configuration -o "$($folder)\lib"
        # The only framework specific assembly we have is for Windows-only functionality, so ...
        dotnet publish -c $Configuration -o "$($Folder)\lib" -r win10 -f "netstandard2.0"

        # Make sure we never ship SMA
        Get-ChildItem "$($Folder)\lib" -Filter "System.Management.Automation*" |
            Remove-Item
    }

    Write-Host
    Write-Host "Module build finished." -ForegroundColor Green

    Remove-Item "$($folder)\en-US" -Force -Recurse -ErrorAction SilentlyContinue
    New-ExternalHelp -Path ".\Docs" -OutputPath  "$($folder)\en-US"
    Write-Host "PlatyPS Documentation finished." -ForegroundColor Green

    $BuildTimer.Stop()
    Write-Host "Total Elapsed $($BuildTimer.Elapsed.ToString("hh\:mm\:ss\.ff"))"
} catch {
    throw $_
} finally {
    Pop-Location -StackName BuildTestStack
}