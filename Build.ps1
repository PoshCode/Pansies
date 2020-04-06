#Requires -Module Configuration
[CmdletBinding()]
param(
    [ValidateSet("Release","Debug")]
    $Configuration = "Release"
)

Push-Location $PSScriptRoot
try {
    $BuildTimer = New-Object System.Diagnostics.Stopwatch
    $BuildTimer.Start()

    $ModuleName = Split-Path $PSScriptRoot -Leaf
    $ErrorActionPreference = "Stop"
    $version = Get-Metadata ".\Source\${ModuleName}.psd1"
    $folder = mkdir $version -Force

    # dotnet restore
    dotnet build -c $Configuration -o "$($folder.FullName)\lib"

    if (!$SkipBinaryBuild) {
        Write-Host "##  Compiling Pansies binary module" -ForegroundColor Cyan
        # dotnet restore

        # The only framework specific assembly we have is for Windows-only functionality, so ...
        dotnet publish -c $Configuration -o "$($Folder)\lib" -r win10

        # Make sure we never ship SMA
        Get-ChildItem "$($Folder)\lib" -Filter "System.Management.Automation*" |
            Remove-Item
    }

    Write-Host
    Write-Host "Module build finished." -ForegroundColor Green

    Remove-Item "$($folder.FullName)\en-US" -Force -Recurse -ErrorAction SilentlyContinue
    New-ExternalHelp -Path ".\Docs" -OutputPath  "$($folder.FullName)\en-US"
    Write-Host "PlatyPS Documentation finished." -ForegroundColor Green

    $BuildTimer.Stop()
    Write-Host "Total Elapsed $($BuildTimer.Elapsed.ToString("hh\:mm\:ss\.ff"))"
} catch {
    throw $_
} finally {
    Pop-Location
}