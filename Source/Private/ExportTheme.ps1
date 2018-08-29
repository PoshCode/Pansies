function ExportTheme {
    <#
        .SYNOPSIS
            Imports themes by name
    #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        # The name of the theme
        [Parameter(Position = 0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$Name,

        [Parameter(ValueFromPipeline, Position = 1)]
        $InputObject,

        [switch]$Force,

        [switch]$PassThru,

        [ValidateSet("User", "Machine")]
        [string]$Scope = "User"
    )
    process {
        $NativeThemePath = Join-Path  $(Get-ConfigurationPath -Scope $Scope) "$Name.theme.psd1"

        if(Test-Path $NativeThemePath) {
            if($Force -or $PSCmdlet.ShouldContinue("Overwrite $NativeThemePath?", "Theme exists")) {
                Write-Verbose "Exporting to $NativeThemePath"
                $InputObject | Export-Metadata $NativeThemePath
            }
        } else {
            Write-Verbose "Exporting to $NativeThemePath"
            $InputObject | Export-Metadata $NativeThemePath
        }

        if($PassThru) {
            $InputObject | Add-Member NoteProperty Name $NativeThemePath -Passthru
        }
    }
}