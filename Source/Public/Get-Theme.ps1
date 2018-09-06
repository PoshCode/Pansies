function Get-Theme {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    param(
        # The name of the theme(s) to show. Supports wildcards, and defaults to * everything.
        [string]$Name = "*",

        # If set, only returns themes that include ConsoleColor
        [switch]$ConsoleColors,

        # If set, only returns themes that include PSReadline Colors
        [switch]$PSReadline
    )

    $Name = $Name -replace "((\.theme)?\.psd1)?$" -replace '$', ".theme.psd1"

    foreach($Theme in Join-Path $(
            Get-ConfigurationPath -Scope User -SkipCreatingFolder
            Get-ConfigurationPath -Scope Machine -SkipCreatingFolder
        ) -ChildPath $Name -Resolve -ErrorAction Ignore ) {
            if ($ConsoleColors -or $PSReadline) {
                $ThemeData = Import-Metadata -Path $Theme
                if($ConsoleColors -and !$ThemeData.ConsoleColors) {
                    continue
                }
                if($PSReadline -and !$ThemeData.PSReadline) {
                    continue
                }
            }
            $Name = if($ThemeData.Name) {
                $ThemeData.Name
            } else {
                [IO.Path]::GetFileName($Theme) -replace "\.theme\.psd1$"
            }
            $Name | Add-Member NoteProperty PSPath $Theme -PassThru |
                    Add-Member NoteProperty Name $Name -PassThru
    }
}