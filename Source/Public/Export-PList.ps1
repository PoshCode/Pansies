function Export-PList {
    <#
        .SYNOPSIS
            Convert an object to an XML or Binary PList file.
    #>
    [CmdletBinding()]
    param(
        # The object(s) to convert
        [Parameter(Mandatory, ValueFromPipeline)]
        [Alias("io")]
        [Object[]]$InputObject,

        # The path to an XML or binary plist file (e.g. a .tmTheme file)
        [Parameter(Mandatory, ValueFromPipelineByPropertyName, Position = 0)]
        [Alias("PSPath")]
        [string]$Path,

        [switch]$Binary
    )

    begin {
        $Output = @()
    }
    process {
        $Output += $InputObject
    }
    end {
        $Parent = Split-Path $Path
        if(!(Test-Path -LiteralPath $Parent -PathType Container)) {
            New-Item -ItemType Directory -Path $Parent -Force
        }
        $Parent = Convert-Path $Parent
        $Path = Join-Path $Parent -ChildPath (Split-Path $Path -Leaf)

        if ($Binary) {
            [PoshCode.Pansies.Parsers.Plist]::WriteBinary($Output, $Path)
        } else {
            [PoshCode.Pansies.Parsers.Plist]::WriteXml($Output, $Path)
        }
    }
}