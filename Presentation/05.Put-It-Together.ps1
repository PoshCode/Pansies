param(
    $Top = 3,
    $Left = 6,
    $height = 12,
    $width = 50
)
$Height += $Top
$position = "`e[{0};{1}f"
"`e[?1049h`e[38;2;255;200;68m`e[48;2;200;79;104m`e(0"
-join @(
    $position -f $Top++, $Left
    "l" + ("q" * $Width) + "k"
    while ($Top -lt $Height) {
        $position -f $Top++, $Left
        "x" + (" " * $Width) + "x"
    }
    $position -f $Top++, $Left
    "m" + ("q" * $Width) + "j"
)
"`e(B"
Read-Host
"`e[?1049l"