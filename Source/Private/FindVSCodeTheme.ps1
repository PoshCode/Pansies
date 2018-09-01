function FindVsCodeTheme {
    [CmdletBinding()]
    param($Name)

    $VSCodeExtensions = @(
        # VS Code themes are in one of two places: in the app, or in your profile folder:
        Convert-Path "~\.vscode*\extensions\"
        # If `code` is in your path, we can guess where that is...
        Get-Command Code-Insiders, Code -ErrorAction Ignore |
            Split-Path | Split-Path | Join-Path -ChildPath "resources\app\extensions\"
    )
    $Warnings = @()

    $Themes = @(
        # If they passed a file path that exists, use just that one file
        $AllThemes = if (Test-Path $Name) {
            Convert-Path $Name
        } else {
            $VSCodeExtensions | Join-Path -ChildPath "\*\themes\*.json" -Resolve
            $VSCodeExtensions | Join-Path -ChildPath "\*\themes\*.tmtheme" -Resolve
        }

        # Verify that we can parse the .json themes
        foreach ($File in $AllThemes) {
            try {
                if($File.EndsWith(".json")) {
                    ConvertFrom-Json (Get-Content -Path $File -Raw -Encoding utf8) -ErrorAction SilentlyContinue |
                        Add-Member -MemberType NoteProperty -Name Source -Value $File -PassThru
                } else {
                    Import-PList -Path $File |
                        Add-Member -MemberType NoteProperty -Name Source -Value $File -PassThru
                }
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


    if ($VerbosePreference -eq "Continue") {
        $ThemeNames = $Themes.name | Sort-Object
        Write-Verbose "Found Themes: $($ThemeNames -join ', ')"
    }

    # Make sure we're comparing the name to a name
    $Name = [IO.Path]::GetFileName(($Name -replace "\.json$|\.tmtheme$"))
    Write-Verbose "Testing theme names for '$Name'"

    if(!($Theme = $Themes.Where{$_.name -eq $Name})) {
        if (!($Theme = $Themes.Where{$_.name -like $Name})) {
            if (!($Theme = $Themes.Where{$_.name -like "*$Name*"})) {
                foreach($Warning in $Warnings) {
                    Write-Warning $Warning
                }
                Write-Error "Couldn't find the theme '$Name', please try another: $(($Theme.name | Select-Object -Unique) -join ', ')"
            }
        }
    }
    if(($Theme.name | Select-Object -Unique).Count -gt 1) {
        Write-Warning "Found more than one theme for '$Name'. Using '$($Theme[0].name)', but you could try again for one of: $(($Theme.name | Select-Object -Unique) -join ', ')"
    }
    @($Theme)[0].Source
}
