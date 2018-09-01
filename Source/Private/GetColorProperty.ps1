function GetColorProperty{
    <#
        .SYNOPSIS
            Search the colors for a matching theme color name and returns the foreground
    #>
    param(
        # The array of colors
        [Array]$colors,

        # An array of (partial) scope names in priority order
        # The foreground color of the first matching scope in the tokens will be returned
        [string[]]$name
    )
    # Since we loaded the themes in order of prescedence, we take the first match that has a foreground color
    foreach ($pattern in $name) {
        # Normalize color
        if($foreground = @($colors.$pattern).Where{$_}[0]) {
            ConvertToCssColor $foreground
            return
        }
    }
}
