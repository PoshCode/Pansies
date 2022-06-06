---
Module Name: Pansies
Module Guid: 6c376de1-1baf-4d52-9666-d46f6933bc16
Download Help Link: {{Please enter FwLink manually}}
Help Version: {{2.0}}
Locale: en-US
---

# <strong>P</strong>owershell <strong>ANSI E</strong>scape <strong>S</strong>equences Module

## Description
This module contains classes and functions for doing ANSI colored output using Virtual Terminal escape sequences in the console from .Net and PowerShell on platforms where they are supported: Windows 10, Linux, OS X, etc.

It also contains commands for manipulating the Windows console color palette and converting color settings from iTerm and VSCode, including a "theme" format based on psd1 files.

It requires PowerShell 5 or higher, and an ANSI-capable host like the Windows 10 console, or xTerm, iTerm, or ConEmu.

## Cmdlets

### [Get-ColorWheel](Get-ColorWheel.md)
Like Get-Gradient, but allows you to specify the Hue step and by default adjusts the brightness so you don't get exact color repeatition

### [Get-Complement](Get-Complement.md)
Get the Hue complement color

### [Get-Gradient](Get-Gradient.md)
Get a range of colors between two colors

### [New-Hyperlink](New-Hyperlink.md)
Takes a Uri and optional text and writes [a hyperlink](https://gist.github.com/egmontkob/eb114294efbcd5adb1944c9f3cb5feda#file-hyperlinks_in_terminal_emulators-md) supported by most terminals

### [New-Text](New-Text.md)
Create a Text object with specified background and foreground colors. Supports [HTML named entities](https://www.w3schools.com/charsets/ref_html_entities_4.asp) like `&hearts;` and `&frac12;` or `&uuml;` and numerical unicode character entities in both decimal (e.g. `&#926;`) and hexadeximal (`&#x39E;`)

### [Write-Host](Write-Host.md)
Backwards compatible Write-Host replacement which writes customized output to a host, but using full RGB color values.

## Providers

Pansies provides an RgbColor provider, with two default drives: Fg, and Bg. These are "content" drives which provide a way for you to refer to any color as though it were a variable, like: `${fg:red}` or `${fg:#FF0000}` and even `${fg:clear}` so you can change colors in the middle of a string:

```
C:\PS> "I ${fg:red}♥${fg:clear} PS"
```

## Classes

Pansies provides a few important classes:

*RgbColor* is a powerful representation of RGB colors with support for parsing CSS style color strings "#RRGGBB" and XTerm indexes, as well as handling the ConsoleColor values PowerShell users are used to. In addition to that, it has conversions to other color spaces for the purpose of doing color math like generating palettes and gradients, etc. The `ToString()` implementation shows the properties, but there is an overload which takes a boolean for Background or Foreground and renders to ANSI escape sequences. It has built-in palette for XTerm, and a built-in ConsoleColor palette which (on Windows) sniffs the current console's current color configuration. It uses these palettes to automatically downsample RGB colors to the nearest match when it's necessary to render in those color spaces.

*Text* is a text class which contains BackgroundColor and ForegroundColor properties and a `ToString()` implementation based on VT escape sequences.  It also supports HTML named enties like the `&hearts;` example above.

### Color Spaces

Pansies includes versions of all the old Colormine library classes, so it's a very complete library for color spaces regardless of whether you're doing terminal work or color work in graphical or web applications. There are colorspace classes for HSL, HSB, LAB, and Hunter LAB, CMY and CMYK, LCH, LUV, XYZ, YXY, and of course, RGB. These color spaces support converting colors between them, doing color math like measuring distance, calculating midpoints and gradients, etc. There are also color comparison libraries with implementations of [Cie 76]9http://en.wikipedia.org/wiki/Color_difference#CIE76), [Cie 94](http://en.wikipedia.org/wiki/Color_difference#CIE94), [Cie delta-e 2000](http://en.wikipedia.org/wiki/Color_difference#CIEDE2000) and [Cmc l:c](http://en.wikipedia.org/wiki/Color_difference#CMC_l:c_.281984.29).


We also have *palette* classes which support the XTerm 256 color palette, the X11 named colors palette (with over 650 named colors), and the default 16 color console palette (which includes loading the actual palette of the `console` when in Windows console). Any palette has the ability to find the closest match to any given RgbColor (by default, using the Cie De2000 algorithm) so you can downscale nicely from full color to 256 or 16 color.

You can play with setting `[PoshCode.Pansies.RgbColor]::ColorMode` to change how the colors are down-sampled, and you can even modify the actual palettes in `[PoshCode.Pansies.RgbColor]::ConsolePalette` and `[PoshCode.Pansies.RgbColor]::XTermPalette`

