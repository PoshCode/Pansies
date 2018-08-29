function ImportJsonIncludeLast {
    <#
        .SYNOPSIS
            Import VSCode json themes, including any included themes
    #>
    [CmdletBinding()]
    param([string[]]$Path)

    # take the first
    $themeFile, $Path = $Path
    $theme = Get-Content $themeFile | ConvertFrom-Json

    # Output all the colors or token colors
    if ($theme.colors) {
        $theme.colors
    }
    if ($theme.tokenColors) {
        $theme.tokenColors
    }

    # Recurse includes
    if ($theme.include) {
        $Path += $themeFile | Split-Path | Join-Path -Child $theme.include | convert-path
    }
    if ($Path) {
        ImportJsonIncludeLast $Path
    }
}
