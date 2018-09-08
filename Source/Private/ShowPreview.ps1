function ShowPreview {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory, Position = 0)]
        $Palette,

        [Parameter(Position = 1)]
        $Syntax,

        $Name,

        [Switch]$Tiny,

        [Switch]$MoreText,

        [Switch]$NoCodeSample
    )
    if($null -eq $Syntax) {
        $Syntax  = Get-PSReadLineOption | Select-Object -Property @(
                @{Name = "ContinuationPrompt"; Expression = { $_.ContinuationPromptColor }}
                @{Name = "Parameter"; Expression = { $_.ParameterColor }}
                @{Name = "Member"; Expression = { $_.MemberColor }}
                @{Name = "Command"; Expression = { $_.CommandColor }}
                @{Name = "Operator"; Expression = { $_.OperatorColor }}
                @{Name = "Emphasis"; Expression = { $_.EmphasisColor }}
                @{Name = "Selection"; Expression = { $_.SelectionColor }}
                @{Name = "Variable"; Expression = { $_.VariableColor }}
                @{Name = "Type"; Expression = { $_.TypeColor }}
                @{Name = "Keyword"; Expression = { $_.KeywordColor }}
                @{Name = "String"; Expression = { $_.StringColor }}
                @{Name = "Error"; Expression = { $_.ErrorColor }}
                @{Name = "Number"; Expression = { $_.NumberColor }}
                @{Name = "Default"; Expression = { $_.DefaultTokenColor }}
                @{Name = "Comment"; Expression = { $_.CommentColor }}
            )
    }
    $e = [char]27

    -join $(
        "$($Name)`n"
        if($Palette) {
            if($Palette.Count -lt 16) {
                throw "Invalid theme: missing ConsoleColors (there should be 16, but are only $($Palette.Count))"
            }

            if (!$Tiny) {
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
                        $(if($null -ne $bg) { $bg.ToVtEscapeSequence($true) })
                        $(if($null -ne $fg) { $fg.ToVtEscapeSequence() })
                        "  gYw  $([char]27)[0m "
                    }
                    "`n"
                }
                "`n"
            }

            0..7 | ForEach-Object {
                $Dark = $Palette[$_]
                $Lite = $Palette[($_ + 8)]

                New-Text (" $([ConsoleColor]$_)".PadRight(12) + " $Dark ") -Fore (Get-Complement $Dark -Force) -Back $Dark -LeaveColor
                New-Text (" $([ConsoleColor]$_+8)".PadRight(9) + " $Lite ") -Fore (Get-Complement $Lite -Force) -Back $Lite
                "`n"
            }
        }

        if($Syntax -and !$NoCodeSample) {
            "$e[8A$e[45G$($Syntax.Keyword)function $($Syntax.Default)Test-Syntax $($Syntax.Default){"
            "$e[B$e[45G    $($Syntax.Comment)# Demo Syntax Highlighting"
            "$e[B$e[45G    $($Syntax.Default)[$($Syntax.Type)CmdletBinding$($Syntax.Default)()]"
            "$e[B$e[45G    $($Syntax.Keyword)param$($Syntax.Default)( [$($Syntax.Type)IO.FileInfo$($Syntax.Default)]$($Syntax.Variable)`$Path $($Syntax.Default))"
            "$e[B"
            "$e[B$e[45G    $($Syntax.Command)Write-Verbose $($Syntax.String)`"Testing in $($Syntax.Variable)`$($($Syntax.Command)Split-Path $($Syntax.Variable)`$PSScriptRoot $($Syntax.Parameter)-Leaf$($Syntax.Variable))$($Syntax.String)`" $($Syntax.Parameter)-Verbose"
            "$e[B$e[45G    $($Syntax.Variable)`$Env:PSModulePath $($Syntax.Operator)-split $($Syntax.String)';' $($Syntax.Operator)-notcontains $($Syntax.Variable)`$Path$($Syntax.Default).$($Syntax.Member)FullName"
            "$e[B$e[45G$($Syntax.Default)}$e[39m$e[B"
        }

        if($Palette -and $MoreText) {
            0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 | ForEach-Object {
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