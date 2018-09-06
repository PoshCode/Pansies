function Import-PList {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding()]
    param(
        # The path to an XML or binary plist file (e.g. a .tmTheme file)
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName, Position = 0)]
        [Alias("PSPath")]
        [string]$Path
    )

    process {
        $Path = Convert-Path $Path

        [PoshCode.Pansies.Parsers.Plist]::ReadPlist($Path)
    }
}