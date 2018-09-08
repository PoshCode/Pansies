function Import-Theme {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    param(
        # A theme to import (can be the name of an installed PANSIES theme, or the full path to a psd1 file)
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName)]
        [string]$Name,

        # By default, imported themes will update the default console colors (if they define console colors)
        # If SkipDefault is set, Import-Theme will leave the default Console Colors alone
        [switch]$SkipDefault
    )
    $Theme = ImportTheme $Name

    if ($ConsoleColors = $Theme.ConsoleColors) {
        Write-Verbose "Setting the console palette"

        # Handle setting the default foreground and background
        if($Theme.ConsoleBackground) {
            if($Theme.ConsoleForeground) {
                $ConsoleColors += [RgbColor]$Theme.ConsoleForeground
            } else {
                $ConsoleColors += Get-Complement $Theme.ConsoleBackground
            }
            $ConsoleColors += [RgbColor]$Theme.ConsoleBackground
        } elseif($Theme.ConsoleForeground) {
            $ConsoleColors += [RgbColor]$Theme.ConsoleForeground
            $ConsoleColors += Get-Complement $Theme.ConsoleForeground -ConsoleColor
        } else {
            # As a fall back, always force the background to black and the foreground to white
            # Because if you're in a default PowerShell window, you're using DarkYellow on DarkMagenta, which will be awful
            $ConsoleColors += [RgbColor]"White"
            $ConsoleColors += [RgbColor]"Black"
        }

        # Handle setting the popup foreground and background
        if($Theme.PopupBackground) {
            if($Theme.PopupForeground) {
                $ConsoleColors += [RgbColor]$Theme.PopupForeground
            } else {
                $ConsoleColors += Get-Complement $Theme.PopupBackground
            }
            $ConsoleColors += [RgbColor]$Theme.PopupBackground
        } elseif($Theme.PopupForeground) {
            $ConsoleColors += [RgbColor]$Theme.PopupForeground
            $ConsoleColors += Get-Complement $Theme.PopupForeground -ConsoleColor
        }

        Set-ConsolePalette -Colors $ConsoleColors -Default:(!$SkipDefault)
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