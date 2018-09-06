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

    $FileName = $Name -replace "((\.theme)?\.psd1)?$" -replace '$', ".theme.psd1"

    $Path = if (!(Test-Path -LiteralPath $FileName)) {
        Get-Theme $Name | Select-Object -First 1 -ExpandProperty PSPath
    } else {
        Convert-Path $FileName
    }
    if(!$Path) {
        $Themes = @(Get-Theme "$Name*")
        if($Themes.Count -gt 1) {
            Write-Warning "No exact match for $Name. Using $($Themes[0]), but also found $($Themes[1..$($Themes.Count-1)] -join ', ')"
            $Path = $Themes[0].PSPath
        } elseif($Themes) {
            Write-Warning "No exact match for $Name. Using $($Themes[0])"
            $Path = $Themes[0].PSPath
        } else {
            $Themes = @(Get-Theme "*$Name*")
            if($Themes.Count -gt 1) {
                Write-Warning "No exact match for $Name. Using $($Themes[0]), but also found $($Themes[1..$($Themes.Count-1)] -join ', ')"
                $Path = $Themes[0].PSPath
            } elseif($Themes) {
                Write-Warning "No exact match for $Name. Using $($Themes[0])"
                $Path = $Themes[0].PSPath
            }
        }
        if(!$Path) {
            Write-Error "No theme '$Name' found. Try Get-Theme to see available themes."
            return
        }
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