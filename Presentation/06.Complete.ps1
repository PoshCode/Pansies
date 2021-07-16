param(
    $Top = 3,
    $Left = 6,
    $height = 12,
    $width = 50,
    $bg = "`e[48;2;{0};{1};{2}m" -f (189, 56, 89),
    $fg = "`e[38;2;{0};{1};{2}m" -f (255, 255, 255)
)
$j,$k,$l,$m,$q,$x = [string[]][char[]]"╝╗╔╚═║"

$Height += $Top
$position = "`e[{0};{1}f$bg$fg"

-join @(
    "`e[?1049h"
    $position -f $Top++, $Left
    $l + ($q * $Width) + $k

    while ($Top -lt $Height) {
        $position -f $Top++, $Left
        $x + (" " * $Width) + $x
    }
    $position -f $Top++, $Left
    $m + ($q * $Width) + $j
)
$Top -= [int]($height / 2)
$Left += 8

"`e7" # Save location
-join @(
    $position -f $Top, $Left
    "Your Warning Here"
)
"`e8`e[0m" # Restore Location
Read-Host
"`e[?1049l"