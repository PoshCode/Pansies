0..255 | % {
    $_ = "xt$_"
    Write-Host "  " -BackgroundColor $_ -NoNewline
    $Bg, $Fg = Get-Complement $_ -Passthru
    Write-Host "  " -BackgroundColor $Fg -NoNewline
    Write-Host " Simple Complement " -ForegroundColor $Fg -BackgroundColor $Bg -NoNewline
    $Bg, $Fg = Get-Complement $_ -Passthru -ForceContrast
    Write-Host "  " -BackgroundColor $Fg -NoNewline
    Write-Host " -ForceContrast " -ForegroundColor $Fg -BackgroundColor $Bg -NoNewline
    Write-Host
}