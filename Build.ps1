#requires -Version "4.0" -Module PackageManagement, Information, Configuration, Pester
using namespace System.Windows.Markup
[CmdletBinding()]
param($Step = $('clean','update','build','test'))


# First call to Write-Host, pass in our TraceTimer to make sure we time EVERYTHING.
Write-Host "BUILDING: $ModuleName in $PSScriptRoot"

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

function init {
    #.Synopsis
    #   The init step always has to run.
    #   Calculate your paths and so-on here.
    [CmdletBinding()]
    param(
        # The path to the folder containing the module to build.
        # By default, the folder containing this file
        [Alias("PSPath")]
        [string]$Path = $PSScriptRoot,

        # The name of the module to build.
        # By default, the name of the last folder in -Path
        [string]$ModuleName = $(Split-Path $Path -Leaf),

        # The build's revision number
        # By convention, pulled from the environment APPVEYOR_BUILD_NUMBER
        # Otherwise, calculated as one larger than the current manifest
        [Nullable[int]]$RevisionNumber = ${Env:APPVEYOR_BUILD_NUMBER},

        # An api token for codecov.io (pulled from the environment CODECOV_TOKEN)
        [ValidateNotNullOrEmpty()]
        [String]$CodeCovToken = ${ENV:CODECOV_TOKEN},

        # The default language is your current UICulture
        [Globalization.CultureInfo]$DefaultLanguage = $((Get-Culture).Name)
    )


    # Calculate Paths
    # The output path is a temporary output and logging location
    $Script:Path = $Path
    $Script:ModuleName = $ModuleName
    $Script:DefaultLanguage = $DefaultLanguage
    $Script:OutputPath = Join-Path $Path output
    $null = mkdir $OutputPath -Force

    # We expect the source for the module in a subdirectory called one of three things:
    $Script:SourcePath = "src", "source", ${ModuleName} | ForEach { Join-Path $Path $_ -Resolve -ErrorAction Ignore } | Select -First 1
    if(!$SourcePath) {
        Write-Warning "This Build script expects a 'Source' or '$ModuleName' folder to be alongside it."
        throw "Can't find module source folder."
    }
    Write-Host "SourcePath: $SourcePath"

    $Script:ManifestPath = Join-Path $SourcePath "${ModuleName}.psd1" -Resolve -ErrorAction Ignore
    if(!$ManifestPath) {
        Write-Warning "This Build script expects a '${ModuleName}.psd1' in the '$SourcePath' folder."
        throw "Can't find module source files"
    }
    $Script:TestPath = "Tests", "Specs" | ForEach { Join-Path $Path $_ -Resolve -ErrorAction Ignore } | Select -First 1
    if(!$TestPath) {
        Write-Warning "This Build script expects a 'Tests' or 'Specs' folder to contain tests."
    }
    # Calculate Version here, because we need it for the release path
    [Version]$Script:Version = Get-Metadata $ManifestPath -PropertyName ModuleVersion

    # If the RevisionNumber is specified as ZERO, this is a release build ...
    # If the RevisionNumber is not specified, this is a dev box build
    # If the RevisionNumber is specified, we assume this is a CI build
    if($RevisionNumber -ge 0) {
        # For CI builds we don't increment the build number
        $Script:Build = if($Version.Build -le 0) { 0 } else { $Version.Build }
    } else {
        # For dev builds, assume we're working on the NEXT release
        $Script:Build = if($Version.Build -le 0) { 1 } else { $Version.Build + 1}
    }

    if([string]::IsNullOrEmpty($RevisionNumber) -or $RevisionNumber -eq 0) {
        $Script:Version = New-Object Version $Version.Major, $Version.Minor, $Build
    } else {
        $Script:Version = New-Object Version $Version.Major, $Version.Minor, $Build, $RevisionNumber
    }

    # The release path is where the final module goes
    $Script:ReleasePath = Join-Path $Path $Version
    $Script:ReleaseManifest = Join-Path $ReleasePath "${ModuleName}.psd1"

}

function clean {
    #.Synopsis
    #   Clean output and old log
    [DependsOn("init")]
    [CmdletBinding()]
    param(
        # Also clean packages
        [Switch]$Packages
    )

    Write-Host "OUTPUT Release Path: $ReleasePath"
    if(Test-Path $ReleasePath) {
        Write-Host "       Clean up old build"
        Write-Host "DELETE $ReleasePath\"
        Remove-Item $ReleasePath -Recurse -Force
    }
    if(Test-Path $Path\packages) {
        Write-Host "DELETE $Path\packages"
        # force reinstall by cleaning the old ones
        Remove-Item $Path\packages\ -Recurse -Force
    }
    if(Test-Path $Path\packages\build.log) {
        Write-Host "DELETE $OutputPath\build.log"
        Remove-Item $OutputPath\build.log -Recurse -Force
    }

}

function update {
    #.Synopsis
    #   Nuget restore and git submodule update
    #.Description
    #   This works like nuget package restore, but using PackageManagement
    #   The benefit of using PackageManagement is that you can support any provider and any source
    #   However, currently only the nuget providers supports a -Destination
    #   So for most cases, you could use nuget restore instead:
    #      nuget restore $(Join-Path $Path packages.config) -PackagesDirectory "$Path\packages" -ExcludeVersion -PackageSaveMode nuspec
    [DependsOn("init")]
    [CmdletBinding()]
    param(
        # Force reinstall
        [switch]$Force, #=$($Step -contains "Clean"),

        # Remove packages first
        [switch]$Clean
    )
    $ErrorActionPreference = "Stop"
    Set-StrictMode -Version Latest
    Write-Host "UPDATE $ModuleName in $Path"

    if(Test-Path (Join-Path $Path packages.config)) {
        if(!($Name = Get-PackageSource | ? Location -eq 'https://www.nuget.org/api/v2' | % Name)) {
            Write-Warning "Adding NuGet package source"
            $Name = Register-PackageSource NuGet -Location 'https://www.nuget.org/api/v2' -ForceBootstrap -ProviderName NuGet | % Name
        }

        if($Force -and (Test-Path $Path\packages)) {
            # force reinstall by cleaning the old ones
            remove-item $Path\packages\ -Recurse -Force
        }
        $null = mkdir $Path\packages\ -Force

        # Remember, as of now, only nuget actually supports the -Destination flag
        foreach($Package in ([xml](gc .\packages.config)).packages.package) {
            Write-Host "Installing $($Package.id) v$($Package.version) from $($Package.Source)"
            $install = Install-Package -Name $Package.id -RequiredVersion $Package.version -Source $Package.Source -Destination $Path\packages -Force:$Force -ErrorVariable failure
            if($failure) {
                throw "Failed to install $($package.id), see errors above."
            }
        }
    }

    # we also check for git submodules...
    git submodule update --init --recursive
}

function build {
    [DependsOn("update")]
    [CmdletBinding()]
    param()
    Write-Host "BUILDING: $ModuleName from $Path"
    # Copy NuGet dependencies
    $PackagesConfig = (Join-Path $Path packages.config)
    if(Test-Path $PackagesConfig) {
        Write-Host "       Copying Packages"
        foreach($Package in ([xml](Get-Content $PackagesConfig)).packages.package) {
            $LibPath = "$ReleasePath\lib"
            $folder = Join-Path $Path "packages\$($Package.id)*"

            # The git NativeBinaries are special -- we need to copy all the "windows" binaries:
            if($Package.id -eq "LibGit2Sharp.NativeBinaries") {
                $targets = Join-Path $folder 'libgit2\windows'
                $LibPath = Join-Path $LibPath "NativeBinaries"
            } else {
                # Check for each TargetFramework, in order of preference, fall back to using the lib folder
                $targets = ($TargetFramework -replace '^','lib\') + 'lib' | ForEach-Object { Join-Path $folder $_ }
            }

            $PackageSource = Get-Item $targets -ErrorAction SilentlyContinue | Select -First 1 -Expand FullName
            if(!$PackageSource) {
                throw "Could not find a lib folder for $($Package.id) from package. You may need to run Setup.ps1"
            }

            Write-Host "robocopy $PackageSource $LibPath /E /NP /LOG+:'$OutputPath\build.log' /R:2 /W:15"
            $null = robocopy $PackageSource $LibPath /E /NP /LOG+:"$OutputPath\build.log" /R:2 /W:15
            if($LASTEXITCODE -ne 0 -and $LASTEXITCODE -ne 1 -and $LASTEXITCODE -ne 3) {
                throw "Failed to copy Package $($Package.id) (${LASTEXITCODE}), see build.log for details"
            }
        }
    }


    ## Copy PowerShell source Files (support for my new Public|Private folders, and the old simple copy way)
    # if the Source folder has "Public" and optionally "Private" in it, then the psm1 must be assembled:
    if(Test-Path (Join-Path $SourcePath Public) -Type Container){
        Write-Host "       Collating Module Source"
        $RootModule = Get-Metadata -Path $ManifestPath -PropertyName RootModule -ErrorAction SilentlyContinue
        if(!$RootModule) {
            $RootModule = Get-Metadata -Path $ManifestPath -PropertyName ModuleToProcess -ErrorAction SilentlyContinue
            if(!$RootModule) {
                $RootModule = "${ModuleName}.psm1"
            }
        }
        $null = mkdir $ReleasePath -Force
        $ReleaseModule = Join-Path $ReleasePath ${RootModule}
        $FunctionsToExport = Join-Path $SourcePath Public\*.ps1 -Resolve | % { [System.IO.Path]::GetFileNameWithoutExtension($_) }
        Write-Host "       Setting content for $ReleaseModule from $(Resolve-Path $SourcePath -Relative)"

        Set-Content $ReleaseModule $(
            @(
                (Join-Path $SourcePath Classes\*.ps1 -Resolve -ErrorAction Ignore | Sort).ForEach{ Get-Content $_ -Raw }
                (Join-Path $SourcePath Private\*.ps1 -Resolve -ErrorAction Ignore | Sort).ForEach{ Get-Content $_ -Raw }
                (Join-Path $SourcePath Public\*.ps1 -Resolve -ErrorAction Ignore | Sort).ForEach{ Get-Content $_ -Raw }
            ) -join "`r`n`r`n`r`n") -Encoding UTF8

        # If there are any folders that aren't Public, Private, Tests, or Specs ...
        if($OtherFolders = Get-ChildItem $SourcePath -Directory -Exclude Classes, Public, Private, Tests, Specs) {
            # Then we need to copy everything in them
            Copy-Item $OtherFolders -Recurse -Destination $ReleasePath
        }

        # Finally, we need to copy any files in the Source directory
        Get-ChildItem $SourcePath -File |
            Where Name -ne $RootModule |
            Copy-Item -Destination $ReleasePath

        Update-Manifest $ReleaseManifest -Property FunctionsToExport -Value $FunctionsToExport
    } else {
        # Legacy modules just have "stuff" in the source folder and we need to copy all of it
        Write-Host "       Copying Module Source"
        Write-Host "COPY   $SourcePath\"
        $null = robocopy $SourcePath\  $ReleasePath /E /NP /LOG+:"$OutputPath\build.log" /R:2 /W:15
        if($LASTEXITCODE -ne 3 -AND $LASTEXITCODE -ne 1) {
            throw "Failed to copy Module (${LASTEXITCODE}), see build.log for details"
        }
    }

    # Copy the readme file as an about_ help
    $ReadMe = Join-Path $Path Readme.md
    if(Test-Path $ReadMe -PathType Leaf) {
        $LanguagePath = Join-Path $ReleasePath $DefaultLanguage
        $null = mkdir $LanguagePath -Force
        $about_module = Join-Path $LanguagePath "about_${ModuleName}.help.txt"
        if(!(Test-Path $about_module)) {
            Write-Host "Turn readme into about_module"
            Copy-Item -LiteralPath $ReadMe -Destination $about_module
        }
    }

    ## Update the PSD1 Version:
    Write-Host "       Update Module Version"
    Push-Location $ReleasePath
    try {
        $FileList = Get-ChildItem -Recurse -File | Resolve-Path -Relative
        Update-Metadata -Path $ReleaseManifest -PropertyName 'ModuleVersion' -Value $Version
        Update-Metadata -Path $ReleaseManifest -PropertyName 'FileList' -Value $FileList
    } catch {
        Write-Error -Exception $_ -ErrorId "InvalidManifest" -Message "Couldn't update manifest. Your manifest must exits have a FileList field (not commented out)." -RecommendedAction "Add FileList = @()"
    } finally {
        Pop-Location
    }
    (Get-Module $ReleaseManifest -ListAvailable | Out-String -stream) -join "`n" | Write-Host
}

function test {
    [DependsOn("build")]
    [CmdletBinding()]
    param(
        [Switch]$Quiet,

        [Switch]$ShowWip,

        [int]$FailLimit=${Env:ACCEPTABLE_FAILURE},

        [ValidateNotNullOrEmpty()]
        [String]$JobID = ${Env:APPVEYOR_JOB_ID}
    )

    if(!$TestPath) {
        Write-Warning "No tests folder found. Invoking Pester in root: $Path"
        $TestPath = $Path
    }

    Write-Host "TESTING: $ModuleName with $TestPath"
    Write-Host "TESTING $ModuleName v$Version" -Verbose:(!$Quiet)
    Remove-Module $ModuleName -ErrorAction Ignore

    $Options = @{
        OutputFormat = "NUnitXml"
        OutputFile = (Join-Path $OutputPath TestResults.xml)
    }
    if($Quiet) { $Options.Quiet = $Quiet }
    if(!$ShowWip){ $Options.ExcludeTag = @("wip") }

    Set-Content "$TestPath\.Do.Not.COMMIT.This.Steps.ps1" "
        Remove-Module $ModuleName -ErrorAction Ignore
        Import-Module $ReleasePath\${ModuleName}.psd1 -Force"

    # Show the commands they would have to run to get these results:
    # if(Get-Command prompt -ErrorAction Ignore) {
    #     Write-Host $(prompt) -NoNewLine
    #     Write-Host Import-Module $ReleasePath\${ModuleName}.psd1 -Force
    #     Write-Host $(prompt) -NoNewLine
    # }

    # TODO: Update dependency to Pester 4.0 and use just Invoke-Pester
    if(Get-Command Invoke-Gherkin -ErrorAction SilentlyContinue) {
        Write-Host "Invoke-Gherkin -Path $TestPath -CodeCoverage `"$ReleasePath\*.psm1`" -PassThru @Options"
        $TestResults = Invoke-Gherkin -Path $TestPath -CodeCoverage "$ReleasePath\*.psm1" -PassThru @Options
    }

    # Write-Host Invoke-Pester -Path $TestPath -CodeCoverage "$ReleasePath\*.psm1" -PassThru @Options
    # $TestResults = Invoke-Pester -Path $TestPath -CodeCoverage "$ReleasePath\*.psm1" -PassThru @Options

    Remove-Module $ModuleName -ErrorAction Ignore
    Remove-Item "$TestPath\.Do.Not.COMMIT.This.Steps.ps1"

    $script:failedTestsCount = 0
    $script:passedTestsCount = 0
    foreach($result in $TestResults)
    {
        if($result -and $result.CodeCoverage.NumberOfCommandsAnalyzed -gt 0)
        {
            $script:failedTestsCount += $result.FailedCount
            $script:passedTestsCount += $result.PassedCount
            $CodeCoverageTitle = 'Code Coverage {0:F1}%'  -f (100 * ($result.CodeCoverage.NumberOfCommandsExecuted / $result.CodeCoverage.NumberOfCommandsAnalyzed))

            # TODO: this file mapping does not account for the new Public|Private module source (and I don't know how to make it do so)
            # Map file paths, e.g.: \1.0 back to \src
            for($i=0; $i -lt $TestResults.CodeCoverage.HitCommands.Count; $i++) {
                $TestResults.CodeCoverage.HitCommands[$i].File = $TestResults.CodeCoverage.HitCommands[$i].File.Replace($ReleasePath, $SourcePath)
            }
            for($i=0; $i -lt $TestResults.CodeCoverage.MissedCommands.Count; $i++) {
                $TestResults.CodeCoverage.MissedCommands[$i].File = $TestResults.CodeCoverage.MissedCommands[$i].File.Replace($ReleasePath, $SourcePath)
            }

            if($result.CodeCoverage.MissedCommands.Count -gt 0) {
                $result.CodeCoverage.MissedCommands |
                    ConvertTo-Html -Title $CodeCoverageTitle |
                    Out-File (Join-Path $OutputPath "CodeCoverage-${Version}.html")
            }
            if(Test-Path Variable:CodeCovToken) #${CodeCovToken})
            {
                # TODO: https://github.com/PoshCode/PSGit/blob/dev/test/Send-CodeCov.ps1
                Write-Host "Sending CI Code-Coverage Results" -Verbose:(!$Quiet)
                $response = &"$TestPath\Send-CodeCov" -CodeCoverage $result.CodeCoverage -RepositoryRoot $Path -OutputPath $OutputPath -Token ${CodeCovToken}
                Write-Host $response.message -Verbose:(!$Quiet)
            }
        }
    }

    # If we're on AppVeyor ....
    if(Get-Command Add-AppveyorCompilationMessage -ErrorAction Ignore) {
        Add-AppveyorCompilationMessage -Message ("{0} of {1} tests passed" -f @($TestResults.PassedScenarios).Count, (@($TestResults.PassedScenarios).Count + @($TestResults.FailedScenarios).Count)) -Category $(if(@($TestResults.FailedScenarios).Count -gt 0) { "Warning" } else { "Information"})
        Add-AppveyorCompilationMessage -Message ("{0:P} of code covered by tests" -f ($TestResults.CodeCoverage.NumberOfCommandsExecuted / $TestResults.CodeCoverage.NumberOfCommandsAnalyzed)) -Category $(if($TestResults.CodeCoverage.NumberOfCommandsExecuted -lt $TestResults.CodeCoverage.NumberOfCommandsAnalyzed) { "Warning" } else { "Information"})
    }

    if(${JobID}) {
        if(Test-Path $Options.OutputFile) {
            Write-Host "Sending Test Results to AppVeyor backend" -Verbose:(!$Quiet)
            $wc = New-Object 'System.Net.WebClient'
            $response = $wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit/${JobID}", $Options.OutputFile)
            if($response) {
                Write-Host ([System.Text.Encoding]::ASCII.GetString($response)) -Verbose:(!$Quiet)
            }
        } else {
            Write-Warning "Couldn't find Test Output: $($Options.OutputFile)"
        }
    }

    if($FailedTestsCount -gt $FailLimit) {
        $exception = New-Object AggregateException "Failed Scenarios:`n`t`t'$($TestResults.FailedScenarios.Name -join "'`n`t`t'")'"
        $errorRecord = New-Object System.Management.Automation.ErrorRecord $exception, "FailedScenarios", "LimitsExceeded", $TestResults
        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

function package {
    [DependsOn("build","test")]
    [CmdletBinding()]
    param()

    Write-Host "robocopy '$ReleasePath' '${OutputPath}\${ModuleName}' /MIR /NP "
    $null = robocopy $ReleasePath "${OutputPath}\${ModuleName}" /MIR /NP /LOG+:"$OutputPath\build.log"

    $zipFile = Join-Path $OutputPath "${ModuleName}-${Version}.zip"
    Add-Type -assemblyname System.IO.Compression.FileSystem
    Remove-Item $zipFile -ErrorAction SilentlyContinue
    Write-Host "ZIP    $zipFile"
    [System.IO.Compression.ZipFile]::CreateFromDirectory((Join-Path $OutputPath $ModuleName), $zipFile)

    # You can add other artifacts here
    ls $OutputPath -File
}

Push-Location $PSScriptRoot

Trace-Info {
    init
    foreach($s in $Step){
        Write-Host "Invoking Step: $s"
        & $s
    }
} -infa Continue

Pop-Location
Write-Host "FINISHED: $ModuleName in $Path"
