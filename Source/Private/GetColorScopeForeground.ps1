function GetColorScopeForeground {
    <#
        .SYNOPSIS
            Search the tokens for a scope name with a foreground color
    #>
    param(
        # The array of tokens
        [Array]$tokens,

        # An array of (partial) scope names in priority order
        # The foreground color of the first matching scope in the tokens will be returned
        [string[]]$name
    )
    # Since we loaded the themes in order of prescedence, we take the first match that has a foreground color
    foreach ($pattern in $name) {
        foreach ($token in $tokens) {
            if (($token.scope -split "\s*,\s*" -match $pattern) -and $token.settings.foreground) {
                ConvertToCssColor $token.settings.foreground
                return
            }
        }
    }
}
