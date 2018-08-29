function FindVsCodeTheme {
    [CmdletBinding()]
    param($Name)

    $VSCodeExtensions = @(
        # VS Code themes are in one of two places: in the app, or in your profile folder:
        Convert-Path "~\.vscode*\extensions\"
        # If `code` is in your path, we can guess where that is...
        Get-Command Code-Insiders, Code  -ErrorAction SilentlyContinue |
            Split-Path | Split-Path | Join-Path -ChildPath "resources\app\extensions\"
    )
    $Warnings = @()

    $Themes = @(
        # If they passed a file path that exists, use just that one file
        $AllThemes = if (Test-Path $Name) {
            Convert-Path $Name
        } else {
            $VSCodeExtensions | Join-Path -ChildPath "\*\themes\*.json" -Resolve
        }

        # Verify that we can parse the .json themes
        foreach ($File in $AllThemes) {
            try {
                ConvertFrom-Json (Get-Content -Path $File -Raw -Encoding utf8) -ErrorAction SilentlyContinue |
                    Add-Member -MemberType NoteProperty -Name Source -Value $File -PassThru
            } catch {
                $Warnings += "Couldn't parse '$File'. $(
                    if($PSVersionTable.PSVersion.Major -lt 6) {
                        'You could try again with PowerShell Core, the JSON parser there works much better!'
                    })"
            }
        }
    )
    if($Themes.Count -eq 0) {
        throw "Could not find any VSCode themes. Please use a full path."
    }

    # Make sure we're comparing the name to a name
    $Name = [IO.Path]::GetFileName(($Name -replace ".json$"))

    if(!($Theme = $Themes.Where{$_.Name -eq $Name})) {
        if (!($Theme = $Themes.Where{$_.Name -like $Name})) {
            if (!($Theme = $Themes.Where{$_.Name -like "*$Name*"})) {
                foreach($Warning in $Warnings) {
                    Write-Warning $Warning
                }
                Write-Error "Couldn't find the theme '$Name', please try another: $(($Theme.Name | Select-Object -Unique) -join ', ')"
            }
        }
    }
    if($Theme.Count -gt 1) {
        Write-Warning "Found more than one theme for '$Name'. Using '$($Theme[0].Name)', but you could try again for one of: $(($Theme.Name | Select-Object -Unique) -join ', ')"
    }
    @($Theme)[0].Source
}
