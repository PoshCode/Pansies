function Show-Theme {
    [CmdletBinding(DefaultParameterSetName="CurrentTheme")]
    param(
        [Alias("Theme")]
        [Parameter(ValueFromPipelineByPropertyName)]
        [string]$Name,

        [Switch]$NoTable,

        [Switch]$MoreText,

        [Switch]$CodeSample
    )
    process {
        $Theme = ImportTheme $Name
        $Palette = $Theme.ConsoleColors

        -join $(
            "$Name`n"
            if(!$NoTable) {
                $ansi = 30
                $bold = $false
                "             Black   Red     Green   Yellow  Blue    Magenta Cyan    White   Gray    Dark Gray `n"
                "       49m     40m     41m     42m     43m     44m     45m     46m     47m     100m    107m    `n"
                "  39m"

                foreach($fg in @($null) + $Palette[0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15]) {
                    if($fg) {
                        "$(if($bold){"1;"}else{"  "})$($ansi)m"
                        $ansi += $bold
                        $bold = !$bold
                    }
                    foreach($bg in @($null) + $Palette[0, 4, 2, 6, 1, 5, 3, 15, 7, 8]) {
                        $(if($bg -ne $null) { $bg.ToVtEscapeSequence($true) })
                        $(if($fg -ne $null) { $fg.ToVtEscapeSequence() })
                        "  gYw  $([char]27)[0m "
                    }
                    "`n"
                }
                "`n"
            }

            0..7 | % {
                $Dark = $Palette[$_]
                $Lite = $Palette[($_ + 8)]

                New-Text " $([ConsoleColor]$_) $Dark ".PadRight(22) -Fore (Get-Complement $Dark -Force) -Back $Dark -LeaveColor
                New-Text " $([ConsoleColor]$_+8) $Lite ".PadRight(18) -Fore (Get-Complement $Lite -Force) -Back $Lite
                "`n"
            }

            if($MoreText) {
                0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 | % {
                    $Color = $Palette[$_]
                    New-Text " This is a test " -Back Black -Fore $Color -LeaveColor
                    New-Text " showing more " -Back DarkGray -Fore $Color -LeaveColor
                    New-Text " sample text on " -Back Gray -Fore $Color -LeaveColor
                    New-Text " common backgrounds: " -Back White -Fore $Color -LeaveColor
                    New-Text " $Color $([ConsoleColor]$_) ".PadRight(22) -Fore $(if($_ -le 8 -and $_ -ne 7){"White"}else{"Black"}) -Back $Color
                    "`n"
                }
                "`n"
            }
        )
    }
}