@{
    # The module version should be SemVer.org compatible
    ModuleVersion           = '1.4.0'

    # PrivateData is where all third-party metadata goes
    PrivateData = @{
        # PrivateData.PSData is the PowerShell Gallery data
        PSData = @{
            # Prerelease string of this module
            Prerelease      = '-beta02'

            # ReleaseNotes of this module
            ReleaseNotes    = '
            1.4.0-beta: Add commands to expose the console palette features
            1.3.0:      Add support for x11 color names to make the drives more useful
                        Add a RgbColor content provider that can convert colors to their escape sequences
                        Add Fg: drive and Bg: drive so you can "$Fg:Red$Bg:Blue$YourMessage"
            '

            # Tags applied to this module. These help with module discovery in online galleries.
            Tags            = 'ANSI','EscapeSequences','VirtualTerminal','Color'

            # A URL to the license for this module.
            LicenseUri      = 'https://github.com/PoshCode/Pansies/blob/master/LICENSE'

            # A URL to the main website for this project.
            ProjectUri      = 'https://github.com/PoshCode/Pansies'

            # A URL to an icon representing this module.
            IconUri         = 'https://github.com/PoshCode/Pansies/blob/resources/Pansies_32.gif?raw=true'
        } # End of PSData
    } # End of PrivateData

    # Script module or binary module file associated with this manifest.
    RootModule              = 'Pansies.psm1'

    # Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
    NestedModules           = @( "lib\Pansies.dll" )

    # Modules that must be imported into the global environment prior to importing this module
    RequiredModules         = @('Configuration')

    # Format files (.ps1xml) to be loaded when importing this module
    FormatsToProcess        = 'Pansies.format.ps1xml'

    # Always define FunctionsToExport as an empty @() which will be replaced on build
    FunctionsToExport       = @()

    # Cmdlets to export from this module
    CmdletsToExport         = @('New-Text', 'Write-Host', 'Get-ConsolePalette', 'Set-ConsolePalette')

    # Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
    AliasesToExport         = 'Text'

    # ID used to uniquely identify this module
    GUID                    = '6c376de1-1baf-4d52-9666-d46f6933bc16'
    Description             = 'A PowerShell module for handling color and cursor positioning via ANSI escape sequences'

    # Common stuff for all our modules:
    CompanyName             = 'PoshCode'
    Author                  = 'Joel Bennett'
    Copyright               = 'Copyright 2018 Joel Bennett'

    # Minimum version of the Windows PowerShell engine required by this module
    PowerShellVersion       = '5.1'
    CompatiblePSEditions    = @('Core','Desktop')
}

