function Get-Theme {
    <#
        .SYNOPSIS
            List available PANSIES themes, optionally filtering
    #>
    [CmdletBinding()]
    param(
        # If set, only returns themes that include ConsoleColor
        [switch]$ConsoleColors,

        # If set, only returns themes that include PSReadline Colors
        [switch]$PSReadline
    )
    foreach($Theme in Join-Path $(
            Get-ConfigurationPath -Scope User -SkipCreatingFolder
            Get-ConfigurationPath -Scope Machine -SkipCreatingFolder
        ) -ChildPath *.theme.psd1 -Resolve -ErrorAction Ignore ) {
            if ($ConsoleColors -or $PSReadline) {
                $ThemeData = Import-Metadata -Path $Theme
                if($ConsoleColors -and !$ThemeData.ConsoleColors) {
                    continue
                }
                if($PSReadline -and !$ThemeData.PSReadline) {
                    continue
                }
            }
            $Name = [IO.Path]::GetFileName($Theme) -replace "\.theme\.psd1$"
            $Name | Add-Member NoteProperty PSPath $Theme -PassThru |
                    Add-Member NoteProperty Name $Name -PassThru
    }
}