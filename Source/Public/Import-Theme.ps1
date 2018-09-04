function Import-Theme {
    <#
        .SYNOPSIS
            Import a theme file to style your console window and PowerShell session
    #>
    [CmdletBinding()]
    param(
        # A theme to import (can be the name of an installed PANSIES theme, or the full path to a psd1 file)
        [string]$Name,

        # By default, imported themes will update the default console colors (if they define console colors)
        # If SkipDefault is set, Import-Theme will leave the default Console Colors alone
        [switch]$SkipDefault
    )
    $Theme = ImportTheme $Name

    if ($ConsoleColors = $Theme.ConsoleColors) {
        Write-Verbose "Setting the console palette"
        Set-ConsolePalette -Colors $ConsoleColors -Default:!$SkipDefault
    }

    if ($PSReadLine = $Theme.PSReadLine) {
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