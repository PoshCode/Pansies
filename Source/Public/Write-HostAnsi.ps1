function Write-HostAnsi {
    <#
    .Synopsis
        Write output to host using ANSI escape sequences to color the output
     #>
    [CmdletBinding(HelpUri='http://go.microsoft.com/fwlink/?LinkID=113426', RemotingCapability='None')]
    param(
        [Parameter(Position=0, ValueFromPipeline=$true, ValueFromRemainingArguments=$true)]
        [System.Object]
        ${Object},

        [switch]
        ${NoNewline},

        [System.Object]
        ${Separator},

        [System.Drawing.Color]
        [Poshcode.Ansi.Color()]
        ${ForegroundColor},

        [System.Drawing.Color]
        [Poshcode.Ansi.Color()]
        ${BackgroundColor}
    )
    begin
    {
        try {
            $outBuffer = $null
            if ($PSBoundParameters.TryGetValue('OutBuffer', [ref]$outBuffer))
            {
                $PSBoundParameters['OutBuffer'] = 1
            }
            if($ForegroundColor) {
                Write-Verbose "ForegroundColor: $ForegroundColor"
                $FgCode = [Poshcode.Ansi.RGB]::GetForegroundCode($ForegroundColor)
                $null = $PSBoundParameters.Remove("ForegroundColor")
            }
            if($BackgroundColor) {
                Write-Verbose "BackgroundColor: $BackgroundColor"
                $BgCode = [Poshcode.Ansi.RGB]::GetBackgroundCode($BackgroundColor)
                $null = $PSBoundParameters.Remove("BackgroundColor")
            }
            if($PSBoundParameters.ContainsKey("Object")) {
                $PSBoundParameters["Object"] = ${Object} = $FgCode + $BgCode + [System.Management.Automation.LanguagePrimitives]::ConvertTo($Object,[string])
            }

            $wrappedCmd = $ExecutionContext.InvokeCommand.GetCommand('Microsoft.PowerShell.Utility\Write-Host', [System.Management.Automation.CommandTypes]::Cmdlet)
            $scriptCmd = {& $wrappedCmd @PSBoundParameters }
            $steppablePipeline = $scriptCmd.GetSteppablePipeline($myInvocation.CommandOrigin)
            $steppablePipeline.Begin($PSCmdlet)
        } catch {
            throw
        }
    }

    process
    {
        try {
            $_ = @(
                if($FgCode) {$FgCode}
                if($BgCode) {$BgCode}
            ) + $_
            $steppablePipeline.Process($_)
        } catch {
            throw
        }
    }

    end
    {
        try {
            $steppablePipeline.End()
        } catch {
            throw
        }
    }
    <#

    .ForwardHelpTargetName Microsoft.PowerShell.Utility\Write-Host
    .ForwardHelpCategory Cmdlet

    #>

}