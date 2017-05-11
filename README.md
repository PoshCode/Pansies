# <img src="https://github.com/Jaykul/Pansies/blob/resources/Pansies_64.gif?raw=true" alt="Pansies" />Pansies

<strong>P</strong>owershell <strong>ANSI E</strong>scape <strong>S</strong>equences

This module contains classes and functions for doing ANSI colored output using Virtual Terminal escape sequences in the console from .Net and PowerShell on platforms where they are supported: Windows 10, Linux, OS X, etc.

```posh
I ♥ PS> function prompt { "I $(New-Text "&hearts;" -fg "DarkRed") PS> " }
```

The goal of this project is to experiment with some classes and interfaces to try and address [PowerShell #2381](https://github.com/PowerShell/PowerShell/issues/2381) and give PowerShell full RGB support for Write-Host, but also provide full color support in format files, etc.

You can install it from [the gallery](https://www.powershellgallery.com/packages/Pansies) (but it may not work on your machine -- please file [issues](https://github.com/PoshCode/Pansies/issues):

```posh
Install-Module Pansies
```

### Currently Pansies provides a couple of important classes:

*RgbColor* is a powerful representation of RGB colors with support for parsing CSS style color strings "#RRGGBB" and XTerm indexes, as well as handling the ConsoleColor values PowerShell users are used to. In addition to that, it has conversions to other color spaces for the purpose of doing color math like generating palettes and gradients, etc. The `ToString()` implementation shows the properties, but there is an overload which takes a boolean for Background or Foreground and renders to ANSI escape sequences. It has built-in palette for XTerm, and a built-in ConsoleColor palette which (on Windows) sniffs the current console's current color configuration. It uses these palettes to automatically downsample RGB colors to the nearest match when it's necessary to render in those color spaces.

*Text* is a text class which contains BackgroundColor and ForegroundColor properties and a `ToString()` implementation based on VT escape sequences.  It also supports HTML named enties like the `&hearts;` example above.

There are also *Palette* classes which support the XTerm 256 color palette and the default ConsoleColor 16 color palette (which currently supports loading the actual palette of the console in Windows, but may _therefore_ break off of Windows), with the ability to find the closest match to any RgbColor.

You can play with setting `[PoshCode.Pansies.RgbColor]::ColorMode` to change how the colors are down-sampled.

### Pansies also provides two commands:

Cmdlet | Description
------ | -----------
New-Text | Creates a `Text` object. Provides parameters for `BackgroundColor` and `ForegroundColor` properties, that renders in console
Write-Host | Writes to host just like Write-Host, but with full RGBColor support

## Contribute

The end goal for this project is for the Color and Text classes (possibly without the color space conversions) to make it into the core PowerShell product, so what I'm most interested in here is [any ideas](https://github.com/PoshCode/Pansies/issues) people have for a better user experience for writing text and partially colored text, as well as other ANSI Virtual Terminal escape sequences.

For the sake of PowerShell 5, I intend to keep this module around, and features that don't belong in PowerShell core for awhile, and we'll even make _some_ attempt to support older versions of PowerShell for Windows (running in ConEmu with ANSI support, or just downsampling everything to ConsoleColors).
