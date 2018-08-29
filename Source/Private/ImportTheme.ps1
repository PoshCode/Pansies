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

    if (!$Name.EndsWith(".psd1")) {
        $Name += ".psd1"
    }

    $Path = if (!(Test-Path $Name)) {
        [IO.Path]::Combine($PSScriptRoot, "Themes", $Name)
    } else {
        $Name
    }

    Write-Verbose "Importing $Name theme from $Path"
    $Theme = Import-Metadata $Path -ErrorAction Stop

    # Cast the ConsoleColors here
    $Theme.ConsoleColors = $Theme.ConsoleColors.ForEach([RgbColor])

    $Theme
}