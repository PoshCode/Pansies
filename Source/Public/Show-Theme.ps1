function Show-Theme {
    [OutputType([string])]
    [CmdletBinding(DefaultParameterSetName="CurrentTheme")]
    param(
        [Alias("Theme","PSPath")]
        [Parameter(ValueFromPipelineByPropertyName)]
        [string]$Name,

        [Switch]$Tiny,

        [Switch]$MoreText,

        [Switch]$NoCodeSample
    )
    process {
        if(!$Name) {
            $Palette = Get-ConsolePalette
            ShowPreview $Palette @PSBoundParameters
        } else {
            foreach($Theme in Get-Theme $Name) {
                $Theme = ImportTheme $Theme.PSPath | Add-Member -Type NoteProperty -Name Name -Value $Theme.Name -Passthru -Force
                $Palette = $Theme.ConsoleColors
                $Syntax  = [PSCustomObject]$Theme.PSReadLine.Colors

                $PSBoundParameters['Name'] = $Theme.Name
                ShowPreview $Palette $Syntax @PSBoundParameters
            }
        }
    }
}