Import-Module $PSScriptRoot\lib\Pansies.dll

if(-not $IsLinux) {
    [PoshCode.Pansies.Console.WindowsHelper]::EnableVirtualTerminalProcessing()
}

# dot source the functions
(Join-Path $PSScriptRoot Private\*.ps1 -Resolve -ErrorAction SilentlyContinue).ForEach{ . $_ }
(Join-Path $PSScriptRoot Public\*.ps1 -Resolve).ForEach{ . $_ }
