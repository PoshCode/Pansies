#Requires -Module Configuration
[CmdletBinding()]
param(
    [ValidateSet("Release","Debug")]
    $Configuration = "Release",

    [switch]$SkipBinaryBuild,

    [switch]$SkipDocs
)

Push-Location $PSScriptRoot
try {
    $BuildTimer = New-Object System.Diagnostics.Stopwatch
    $BuildTimer.Start()
    $ErrorActionPreference = "Stop"

    # We can't clean unless we're going to rebuild the binary part
    $Target = if ($SkipBinaryBuild) { "Build" } else { "CleanBuild" }

    Write-Host "##  Building Pansies script module" -ForegroundColor Cyan
    Build-Module $PSScriptRoot\Source -Passthru -OutVariable Module -Verbose:$VerbosePreference -Target $Target

    $Folder = $Module | Split-Path

    if (!$SkipBinaryBuild) {
        Write-Host "##  Compiling Pansies binary module" -ForegroundColor Cyan
        # dotnet restore

        # The only framework specific assembly we have is for Windows-only functionality, so ...
        dotnet publish -c $Configuration -o "$($Folder)\lib" -r win10 -f netstandard2.0

        # Make sure we never ship SMA
        Get-ChildItem "$($Folder)\lib" -Filter "System.Management.Automation*" |
            Remove-Item
    }

    if (!$SkipDocs) {
        Write-Host "##  Generating Pansies documentation..." -ForegroundColor Green
        Remove-Item "$($Folder)\en-US" -Force -Recurse -ErrorAction SilentlyContinue
        if(Get-Command New-ExternalHelp -ErrorAction SilentlyContinue) {
            New-ExternalHelp -Path ".\Docs" -OutputPath  "$($Folder)\en-US"
            Write-Host "##  PlatyPS documentation finished." -ForegroundColor Green
        } else {
            Write-Host "!!  PlatyPS not found, skipping documentation." -ForegroundColor Yellow
        }
    }

    $BuildTimer.Stop()
    Write-Host "##  Total Elapsed $($BuildTimer.Elapsed.ToString("hh\:mm\:ss\.ff"))"
} catch {
    throw $_
} finally {
    Pop-Location
}