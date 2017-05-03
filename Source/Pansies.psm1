if(-not $IsCoreCLR) {
    Add-Type -Path lib\net452\Pansies.dll
} else {
    Add-Type -Path lib\netstandard1.6\Pansies.dll
}

# dot source the functions
(Join-Path $PSScriptRoot Private\*.ps1 -Resolve -ErrorAction SilentlyContinue).ForEach{ . $_ }
(Join-Path $PSScriptRoot Public\*.ps1 -Resolve).ForEach{ . $_ }
