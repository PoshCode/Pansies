function ImportTheme {
    <#
        .SYNOPSIS
            Imports themes by name
    #>
    [CmdletBinding()]
    param(
        # The name of the theme
        [Parameter(Position=0)]
        [string]$Name
    )

    $Name = $Name -replace "(\.?(theme\.)?psd1)?$", ".theme.psd1"

    $Path = if (!(Test-Path $Name)) {
        Get-ChildItem $(
            Get-ConfigurationPath -Scope User -SkipCreatingFolder
            Get-ConfigurationPath -Scope Machine -SkipCreatingFolder
        ) -Filter $Name -ErrorAction SilentlyContinue | Select-Object -First 1 -ExpandProperty FullName
    } else {
        $Name
    }

    Write-Verbose "Importing $Name theme from $Path"
    $Theme = Import-Metadata $Path -ErrorAction Stop

    # Cast the ConsoleColors here
    $Theme.ConsoleColors = $Theme.ConsoleColors.ForEach([RgbColor])

    $Theme
}