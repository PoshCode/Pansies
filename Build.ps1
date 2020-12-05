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
    $SemVer = gitversion -showvariable nugetversion
}

try {
    $ErrorActionPreference = "Stop"
    Write-Host "## Calling Build-Module" -ForegroundColor Cyan

    $Module = Build-Module -Passthru -SemVer $SemVer
    $Folder  = Split-Path $Module.Path

    if (!$SkipBinaryBuild) {
        Write-Host "## Compiling Pansies binary module" -ForegroundColor Cyan
        # dotnet restore
        # dotnet build -c $Configuration -o "$($folder)\lib"
        dotnet publish -c $Configuration -o "$($Folder)\lib" | Write-Host -ForegroundColor DarkGray
    }

    Write-Host "## Compiling Documentation" -ForegroundColor Cyan

    Remove-Item "$($folder)\en-US" -Force -Recurse -ErrorAction SilentlyContinue
    $null = New-ExternalHelp -Path ".\Docs" -OutputPath  "$($folder)\en-US"

    $Folder

} catch {
    throw $_
} finally {
    Pop-Location -StackName BuildTestStack
}