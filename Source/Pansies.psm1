Add-Type -Path $PSScriptRoot\CSharp\*.cs -ReferencedAssemblies System.Drawing

# dot source the functions
(Join-Path $PSScriptRoot Private\*.ps1 -Resolve -ErrorAction SilentlyContinue).ForEach{ . $_ }
(Join-Path $PSScriptRoot Public\*.ps1 -Resolve).ForEach{ . $_ }
