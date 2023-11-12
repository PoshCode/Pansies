#requires -Module Configuration, @{ ModuleName = "ModuleBuilder"; ModuleVersion = "1.6.0" }

[CmdletBinding()]
param(
    [ValidateSet("Release","Debug")]
    $Configuration = "Release",

    # The version of the output module
    [Alias("ModuleVersion","Version")]
    [string]$SemVer
)
$ErrorActionPreference = "Stop"
Push-Location $PSScriptRoot -StackName BuildTestStack

if (!$SemVer -and (Get-Command gitversion -ErrorAction Ignore)) {
    $SemVer = gitversion -showvariable nugetversion
}

try {
    if (!$SkipBinaryBuild) {
        Write-Host "## Compiling Pansies binary module" -ForegroundColor Cyan
        # dotnet restore
        # dotnet build -c $Configuration -o "$($folder)\lib" | Write-Host -ForegroundColor DarkGray
        dotnet publish -c $Configuration -o "$($Folder)/lib" | Write-Host -ForegroundColor DarkGray
        # We don't need to ship any of the System DLLs because they're all in PowerShell
        Get-ChildItem $Folder -Filter System.* -Recurse | Remove-Item
    }
    Write-Host "## Calling Build-Module" -ForegroundColor Cyan

    $null = $PSBoundParameters.Remove("Configuration")
    $Module = Build-Module @PSBoundParameters -Passthru

    Write-Host "## Compiling Documentation" -ForegroundColor Cyan
    $Folder  = Split-Path $Module.Path

    Remove-Item "$($folder)\en-US" -Force -Recurse -ErrorAction SilentlyContinue
    $null = New-ExternalHelp -Path ".\Docs" -OutputPath  "$($folder)\en-US"

    $Folder

} catch {
    throw $_
} finally {
    Pop-Location -StackName BuildTestStack
}