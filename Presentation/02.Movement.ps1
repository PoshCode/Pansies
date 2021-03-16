param(
    $Top = 3,
    $Left = 6,
    $height = 12,
    $width = 50
)
$Height += $Top
$position = "`e[{0};{1}f"
-join @(
    "`e(0"
    $position -f $Top++, $Left
    "l" + ("q" * $Width) + "k"

    while ($Top -lt $Height) {
        $position -f $Top++, $Left
        "x" + (" " * $Width) + "x"
    }
    $position -f $Top++, $Left
    "m" + ("q" * $Width) + "j"
    "`e(B"
)