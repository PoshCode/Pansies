function Import-PList {
    <#
        .SYNOPSIS
            Convert an XML or Binary PList (property list) file to objects (arrays, string->object dictionaries, etc).
    #>
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