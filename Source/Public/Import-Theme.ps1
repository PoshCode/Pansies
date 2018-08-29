function Import-Theme {
    <#
        .SYNOPSIS
            Import a theme file to style your console window and PowerShell session
    #>
    [CmdletBinding()]
    param(
        # A theme to import (can be the name of an installed PANSIES theme, or the full path to a psd1 file)
        [string]$Name
    )
    $Theme = ImportTheme $Name

    if ($ConsoleColors = $Theme.ConsoleColors) {
        for ($i = 0; $i -lt $ConsoleColors.Count; $i++) {
            $ConsoleColors[$i] = [RgbColor]$ConsoleColors[$i]
        }
        Write-Verbose "Setting the console palette"
        Set-ConsolePalette -Colors $ConsoleColors
    }

    if ($PSReadLine = $Theme.PSReadLine) {
        foreach($key in @($PSReadLine.Colors.Keys)) {
            try {
                $PSReadLine.Colors[$key] = [RgbColor]::new($PSReadLine.Colors[$key]).ToVtEscapeSequence()
            } catch {
                Write-Warning "Skipped 'PSReadLine.$key', because '$($PSReadLine.Colors[$key])' is not a color..."
                $null = $PSReadLine.Colors.Remove($key)
            }
        }
        Write-Verbose "Applying PSReadLineOptions"
        Set-PSReadLineOption @PSReadLine
    }

    if ($HostSettings = $Theme.Host) {
        function Update-Host {
            param($HostObject, $Settings)
            foreach ($key in $Settings.Keys) {
                try {
                    Write-Verbose "Applying Host settings: $key"
                    if ($HostObject.$key -is [ConsoleColor]) {
                        Write-Verbose "Converting '$($Settings.$Key)' to ConsoleColor"
                        $HostObject.$key = [ConsoleColor]([RgbColor]::ConsolePalette.FindClosestColorIndex([RgbColor]::new($Settings.$Key)))
                    } elseif ($Settings.$key -is [hashtable]) {
                        Update-Host $HostObject.$key $Settings.$Key
                    } else {
                        $HostObject.$key = $Settings.$Key
                    }
                } catch {
                    Write-Warning "Failed to apply Host '$key' = '$($Settings.$Key)' (it should have been a $($HostObject.$key.GetType().FullName)"
                }
            }
        }
        Update-Host $Host $HostSettings
    }
}