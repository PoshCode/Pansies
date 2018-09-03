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

    $Name = $Name -replace "((\.theme)?\.psd1)?$" -replace '$', ".theme.psd1"

    $Path = if (!(Test-Path -LiteralPath $Name)) {
        Get-Theme $Name | Select-Object -First 1 -ExpandProperty PSPath
    } else {
        Convert-Path $Name
    }

    Write-Verbose "Importing $Name theme from $Path"
    $Theme = Import-Metadata $Path -ErrorAction Stop

    # Cast the ConsoleColors here
    if($Theme['ConsoleColors']) {
        $Theme['ConsoleColors'] = $Theme['ConsoleColors'].ForEach([RgbColor])
    }
    # Convert colors to escape sequences for PSReadline.Colors
    if ($Colors = $Theme.PSReadline.Colors) {
        foreach ($color in @($Colors.Keys)) {
            # If it doesn't start with ESC
            if ($Colors[$color] -notmatch "^$([char]27)") {
                try {
                    # Use the RGBColor
                    $Colors[$color] = ([RgbColor]$Colors[$color]).ToVtEscapeSequence() <# |
                        Add-Member -MemberType NoteProperty -Name Color -Value ([RgbColor]$Colors[$color]) -PassThru #>
                } catch {
                    Write-Warning "Skipped 'PSReadLine.$color', because '$($Colors[$color])' is neither a color nor an escape sequence"
                    $null = $Colors.Remove($color)
                }
            }
        }
        $Theme['PSReadline']['Colors'] = $Colors
    }

    $Theme
}