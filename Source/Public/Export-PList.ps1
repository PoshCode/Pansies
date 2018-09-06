function Export-PList {
    # .EXTERNALHELP Pansies-help.xml
    [CmdletBinding(SupportsShouldProcess)]
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
        $Parent = if($Parent = Split-Path $Path) {
            if(!(Test-Path -LiteralPath $Parent -PathType Container)) {
                New-Item -ItemType Directory -Path $Parent -Force
            }
            Convert-Path $Parent
        } else {
            Convert-Path (Get-Location -PSProvider FileSystem)
        }
        $Path = Join-Path $Parent -ChildPath (Split-Path $Path -Leaf)

        if($PSCmdlet.ShouldProcess($Path,"Export InputObject as PList $(if($Binary){'Binary'}else{'Xml'})") ) {
            if ($Binary) {
                [PoshCode.Pansies.Parsers.Plist]::WriteBinary($Output, $Path)
            } else {
                [PoshCode.Pansies.Parsers.Plist]::WriteXml($Output, $Path)
            }
        }
    }
}