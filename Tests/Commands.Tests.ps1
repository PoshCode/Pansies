<#
    Pester tests validating new cursor commands, position helper, and Expand-Variable functionality.
#>

BeforeAll {
    $scriptPath = if ($PSCommandPath) {
        $PSCommandPath 
    } elseif ($MyInvocation.MyCommand.Path) {
        $MyInvocation.MyCommand.Path 
    } else {
        $null 
    }
    if (-not $scriptPath) {
        throw 'Unable to determine script path for test execution.'
    }

    $testsFolder = Split-Path -Parent $scriptPath
    $moduleRoot = Split-Path -Parent $testsFolder
    $script:PublishPath = Join-Path $moduleRoot ('.pester-publish-' + [Guid]::NewGuid().ToString('N'))

    New-Item -ItemType Directory -Path $script:PublishPath | Out-Null

    Push-Location $moduleRoot
    try {
        $publishResult = dotnet publish -c Release -o $script:PublishPath 2>&1
        if ($LASTEXITCODE -ne 0) {
            throw "dotnet publish failed:`n$publishResult"
        }
    } finally {
        Pop-Location
    }

    $path = Join-Path $script:PublishPath 'Pansies.dll'
    Import-Module $path -Force
    Set-Variable -Name ModuleAssemblyPath -Scope Script -Value $path
}

AfterAll {
    Remove-Module Pansies -Force -ErrorAction SilentlyContinue
    [System.GC]::Collect()
    [System.GC]::WaitForPendingFinalizers()

    if ($script:PublishPath -and (Test-Path -LiteralPath $script:PublishPath)) {
        Remove-Item -LiteralPath $script:PublishPath -Recurse -Force -ErrorAction SilentlyContinue
    }
}

Describe 'Position helper' {
    It 'formats absolute positions with row and column' {
        $position = [PoshCode.Pansies.Position]::new(3, 4, $true)
        $position.ToString() | Should -Be (([char]0x1b) + '[3;4H')
    }

    It 'formats absolute positions with only column' {
        $position = [PoshCode.Pansies.Position]::new($null, 12, $true)
        $position.ToString() | Should -Be (([char]0x1b) + '[12G')
    }

    It 'formats relative movement' {
        $position = [PoshCode.Pansies.Position]::new(-2, 5, $false)
        $position.ToString() | Should -Be (([char]0x1b) + '[5C') # column takes precedence when positive
    }

    It 'formats relative movement vertically' {
        $position = [PoshCode.Pansies.Position]::new(-3, $null, $false)
        $position.ToString() | Should -Be (([char]0x1b) + '[3A')
    }

    It 'round trips metadata' {
        $original = [PoshCode.Pansies.Position]::new(7, 9, $true)
        $metadata = $original.ToPsMetadata()
        $metadata | Should -Be '7;9;1'

        $roundTrip = [PoshCode.Pansies.Position]::new($metadata)
        $roundTrip.Absolute | Should -BeTrue
        $roundTrip.Line | Should -Be 7
        $roundTrip.Column | Should -Be 9
    }

    It 'throws when metadata is null' {
        $position = [PoshCode.Pansies.Position]::new()
        try {
            $position.FromPsMetadata($null)
            $true | Should -BeFalse -Because 'Expected a MethodInvocationException wrapping ArgumentNullException'
        } catch [System.Management.Automation.MethodInvocationException] {
            $_.Exception.InnerException | Should -BeOfType ([System.ArgumentNullException])
        }
    }
}

Describe 'Set-CursorPosition cmdlet' {
    It 'emits absolute escape sequence' {
        $result = Set-CursorPosition -Line 2 -Column 5 -Absolute
        $result | Should -Be (([char]0x1b) + '[2;5H')
    }

    It 'emits relative escape sequence when Line is negative' {
        $result = Set-CursorPosition -Line -4
        $result | Should -Be (([char]0x1b) + '[4A')
    }

    It 'emits relative escape sequence when Column is positive' {
        $result = Set-CursorPosition -Column 6
        $result | Should -Be (([char]0x1b) + '[6C')
    }
}

Describe 'Save/Restore-CursorPosition cmdlets' {
    It 'writes ESC[s to the console' {
        $writer = New-Object System.IO.StringWriter
        $original = [Console]::Out
        try {
            [Console]::SetOut($writer)
            Save-CursorPosition
        } finally {
            [Console]::SetOut($original)
        }

        $writer.ToString() | Should -Be (([char]0x1b) + '[s')
    }

    It 'writes ESC8 to the console' {
        $writer = New-Object System.IO.StringWriter
        $original = [Console]::Out
        try {
            [Console]::SetOut($writer)
            Restore-CursorPosition
        } finally {
            [Console]::SetOut($original)
        }

        $writer.ToString() | Should -Be (([char]0x1b) + '8')
    }
}

Describe 'Expand-Variable cmdlet' {
    BeforeEach {
        Set-Variable -Name foo -Value 'rainbow' -Scope Script
    }

    AfterEach {
        Remove-Variable -Name foo -Scope Script -ErrorAction SilentlyContinue
        Remove-Variable -Name bar -Scope Script -ErrorAction SilentlyContinue
    }

    It 'replaces variables outside expandable strings with quoted value' {
        $result = Expand-Variable -Content 'Write-Host $variable:foo' -Drive variable
        $result | Should -Be 'Write-Host "rainbow"'
    }

    It 'replaces variables within expandable strings without extra quotes' {
        $result = Expand-Variable -Content '"Color: $variable:foo"' -Drive variable
        $result | Should -Be '"Color: rainbow"'
    }

    It 'escapes characters by default' {
        Set-Variable -Name foo -Value 'He said "Go!"' -Scope Script
        $result = Expand-Variable -Content 'Write-Host $variable:foo' -Drive variable
        $result | Should -Be 'Write-Host "He said `"Go!`""'
    }

    It 'supports unescaped output when requested' {
        Set-Variable -Name foo -Value "Line1`nLine2" -Scope Script
        $result = Expand-Variable -Content 'Write-Host $variable:foo' -Drive variable -Unescaped
        $expected = "Write-Host `"Line1`nLine2`""
        $result | Should -Be $expected
    }

    It 'updates variables in place when requested' {
        Set-Variable -Name bar -Value 'azure' -Scope Script
        Set-Variable -Name foo -Value 'Write-Host $variable:bar' -Scope Script

        $result = Expand-Variable -Path 'variable:foo' -Drive variable -InPlace -Passthru

        $result | Should -Be 'Write-Host "azure"'
        (Get-Variable -Name foo -Scope Script).Value | Should -Be 'Write-Host "azure"'
    }
}

Describe 'XyzConverter white reference' {
    AfterEach {
        [PoshCode.Pansies.ColorSpaceConfiguration]::ResetWhiteReference()
    }

    It 'allows customizing the default white reference point' {
        $custom = [PoshCode.Pansies.ColorSpaces.Xyz]::new(96.5, 101.2, 110.3)
        [PoshCode.Pansies.ColorSpaceConfiguration]::SetWhiteReference($custom)

        $updated = [PoshCode.Pansies.ColorSpaceConfiguration]::GetWhiteReference()
        $updated.X | Should -Be 96.5
        $updated.Y | Should -Be 101.2
        $updated.Z | Should -Be 110.3
    }

    It 'clones the input value when setting the white reference' {
        $custom = [PoshCode.Pansies.ColorSpaces.Xyz]::new(90.1, 92.2, 93.3)
        [PoshCode.Pansies.ColorSpaceConfiguration]::SetWhiteReference($custom)
        $custom.X = 0

        $stored = [PoshCode.Pansies.ColorSpaceConfiguration]::GetWhiteReference()
        $stored.X | Should -Be 90.1
    }

    It 'returns a copy of the white reference when reading' {
        $original = [PoshCode.Pansies.ColorSpaceConfiguration]::GetWhiteReference()
        $copy = [PoshCode.Pansies.ColorSpaceConfiguration]::GetWhiteReference()
        $copy.X = 0

        $current = [PoshCode.Pansies.ColorSpaceConfiguration]::GetWhiteReference()
        $current.X | Should -Be $original.X
    }
}

Describe 'RgbColor XTerm helpers' {
    It 'creates full RGB values when converting from an xterm index' {
        $expected = ([PoshCode.Pansies.RgbColor]::XTermPalette)[42]
        $color = [PoshCode.Pansies.RgbColor]::FromXTermIndex('42')

        $color.RGB | Should -Be $expected.RGB
        $color.Mode | Should -Be ([PoshCode.Pansies.ColorMode]::XTerm256)
    }

    It 'parses prefixed xterm indexes' {
        $expected = ([PoshCode.Pansies.RgbColor]::XTermPalette)[5]
        $color = [PoshCode.Pansies.RgbColor]::FromXTermIndex('xt5')

        $color.RGB | Should -Be $expected.RGB
        $color.Mode | Should -Be ([PoshCode.Pansies.ColorMode]::XTerm256)
    }

    It 'populates RGB values when converting from a byte' {
        $expected = ([PoshCode.Pansies.RgbColor]::XTermPalette)[12]
        $color = [PoshCode.Pansies.RgbColor]::ConvertFrom([byte]12)

        $color.RGB | Should -Be $expected.RGB
        $color.Mode | Should -Be ([PoshCode.Pansies.ColorMode]::XTerm256)
    }
}

Describe 'RgbColor color mode detection' {
    It 'initializes ColorMode to the recommended default' {
        $expected = [PoshCode.Pansies.RgbColor]::GetRecommendedColorMode()
        [PoshCode.Pansies.RgbColor]::ColorMode | Should -Be $expected
    }

    It 'prefers truecolor when COLORTERM advertises support' {
        $env = New-Object 'System.Collections.Generic.Dictionary[string,string]' ([System.StringComparer]::OrdinalIgnoreCase)
        $env['COLORTERM'] = 'truecolor'
        $os = New-Object System.OperatingSystem ([System.PlatformID]::Unix), (New-Object System.Version 5, 4)

        [PoshCode.Pansies.RgbColor]::GetRecommendedColorMode($env, $os) | Should -Be ([PoshCode.Pansies.ColorMode]::Rgb24Bit)
    }

    It 'selects xterm256 when TERM advertises 256 colors' {
        $env = New-Object 'System.Collections.Generic.Dictionary[string,string]' ([System.StringComparer]::OrdinalIgnoreCase)
        $env['TERM'] = 'xterm-256color'
        $os = New-Object System.OperatingSystem ([System.PlatformID]::Unix), (New-Object System.Version 6, 2)

        [PoshCode.Pansies.RgbColor]::GetRecommendedColorMode($env, $os) | Should -Be ([PoshCode.Pansies.ColorMode]::XTerm256)
    }

    It 'falls back to console colors when hints are absent' {
        $env = New-Object 'System.Collections.Generic.Dictionary[string,string]' ([System.StringComparer]::OrdinalIgnoreCase)
        $env['TERM'] = 'vt100'
        $os = New-Object System.OperatingSystem ([System.PlatformID]::Unix), (New-Object System.Version 4, 19)

        [PoshCode.Pansies.RgbColor]::GetRecommendedColorMode($env, $os) | Should -Be ([PoshCode.Pansies.ColorMode]::ConsoleColor)
    }

    It 'prefers RGB24 for Windows 10 and newer' {
        $env = New-Object 'System.Collections.Generic.Dictionary[string,string]' ([System.StringComparer]::OrdinalIgnoreCase)
        $os = New-Object System.OperatingSystem ([System.PlatformID]::Win32NT), (New-Object System.Version 10, 0)

        [PoshCode.Pansies.RgbColor]::GetRecommendedColorMode($env, $os) | Should -Be ([PoshCode.Pansies.ColorMode]::Rgb24Bit)
    }

    It 'uses console colors for legacy Windows releases' {
        $env = New-Object 'System.Collections.Generic.Dictionary[string,string]' ([System.StringComparer]::OrdinalIgnoreCase)
        $os = New-Object System.OperatingSystem ([System.PlatformID]::Win32NT), (New-Object System.Version 6, 1)

        [PoshCode.Pansies.RgbColor]::GetRecommendedColorMode($env, $os) | Should -Be ([PoshCode.Pansies.ColorMode]::ConsoleColor)
    }
}
