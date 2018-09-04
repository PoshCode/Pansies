# <img src="https://github.com/Jaykul/Pansies/blob/resources/Pansies_64.gif?raw=true" alt="Pansies" />Pansies

<strong>P</strong>owershell <strong>ANSI E</strong>scape <strong>S</strong>equences

This module contains classes and functions for doing ANSI colored output using Virtual Terminal escape sequences in the console from .Net and PowerShell on platforms where they are supported: Windows 10, Linux, OS X, etc.

```posh
I ♥ PS> function prompt { "I $(New-Text "&hearts;" -fg "DarkRed") PS> " }
```

The goal of this project is to experiment with some classes and interfaces to try and address [PowerShell #2381](https://github.com/PowerShell/PowerShell/issues/2381) and give PowerShell full RGB support for Write-Host, but also provide full color support in format files, etc.

## Installing

It requires PowerShell 5 or higher and an ANSI-capable host like xTerm, the Windows 10 Console, or ConEmu. If you can satisfy those requirements, you can install it from [the gallery](https://www.powershellgallery.com/packages/Pansies):

```posh
Install-Module Pansies -AllowClobber
```

If you have troubles, please file [issues](https://github.com/PoshCode/Pansies/issues):

## Building from source.

Compiling Pansies requires the .NET Command Line Tools (v2.0.2 or newer) and my [Configuration](http://github.com/PoshCode/Configuration) module. With those dependencies preinstalled and on your path, you can just:

```posh
git clone https://github.com/PoshCode/Pansies.git
.\Pansies\Build.ps1
```

## For more details ...

See the Docs/Pansies.md

