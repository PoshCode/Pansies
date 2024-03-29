# <img src="https://github.com/Jaykul/Pansies/blob/resources/Pansies_64.gif?raw=true" alt="Pansies" />Pansies

<strong>P</strong>owershell <strong>ANSI E</strong>scape <strong>S</strong>equences

This MIT Licensed cross-platform binary module contains classes and functions for doing ANSI colored output, named entities, and more in the console from .NET and PowerShell on platforms where they are supported: Windows 10, Linux, OS X, etc.

```posh
PS>function prompt { "I $(Text '&redheart; ' -fg Red) PS> " }
I ❤️ PS>
```

The goal of this project was to experiment with some classes and interfaces to address [PowerShell #2381](https://github.com/PowerShell/PowerShell/issues/2381) and give PowerShell full RGB support for Write-Host, but also provide full color support in format files, etc. Along the way, I've incorporated a whole library worth of color space theory to make comparing colors and generating gradients and complementary colors easy.

## Installing

For terminal output, you require an ANSI-capable host like xTerm, wezterm, contour, ConEmu (Cmder), or Windows Terminal (or just PowerShell.exe) on Windows 10 or later.

For PowerShell support, you need PowerShell 5.x or higher. You can [install PANSIES from the PowerShell Gallery](https://www.powershellgallery.com/packages/Pansies):

```posh
Install-Module Pansies -AllowClobber
```

For .NET Projects, you can find PANSIES on NuGet. Install with:

```posh
dotnet add reference PoshCode.Pansies
```

If you have troubles, please file [issues](https://github.com/PoshCode/Pansies/issues).

## Building from source

First things first: there is a submodule being used (my [personally modified version](https://github.com/Jaykul/p2f) version of [beefarino/p2f](https://github.com/beefarino/p2f)), so you need to `git clone --recursive` or run `git submodule update --init --recursive` after cloning. You will also occasionally need to update it with `git submodule update --init --recursive`.

The easiest, fastest build uses [earthly](https://docs.earthly.dev/). Earthly builds use containers, so on Windows it requires WSL2, Docker Desktop, and then the earthly CLI. If you already have those, you can just run `earthly +build` to build the module.

### Building without earthly

Compiling Pansies requires the [.NET SDK](https://dotnet.microsoft.com/en-us/download), and building the module additionally requires [Invoke-Build](https://github.com/nightroman/Invoke-Build), [ModuleBuilder](https://github.com/PoshCode/ModuleBuilder), and my [Configuration](http://github.com/PoshCode/Configuration) and [Metadata](https://github.com/PoshCode/Metadata) modules. Once you have `dotnet`, you can install all of the PowerShell dependencies with:

```PowerShell
Install-Script Install-RequiredModule
Install-RequiredModule
```

With those dependencies installed and on your path, you can just run `Invoke-Build`.

### Currently Pansies provides six commands

Cmdlet         | Description
------         | -----------
New-Text       | Creates a `Text` object. Provides parameters for `BackgroundColor` and `ForegroundColor` properties, that renders in console
New-Hyperlink  | Takes a Uri and optional text and writes [a hyperlink](https://gist.github.com/egmontkob/eb114294efbcd5adb1944c9f3cb5feda#file-hyperlinks_in_terminal_emulators-md) supported by most terminals
Write-Host     | Writes to host just like Write-Host, but with full RGBColor support and a -PersistentColor option
Get-Gradient   | Get a range of colors between two colors
Get-ColorWheel | Like Get-Gradient, but allows you to specify the Hue step and by default adjusts the brightness so you don't get exact color repeatition
Get-Complement | Get the Hue complement of a color

One key feature is that `New-Text` and `Write-Host` both support [HTML named entities](https://www.w3schools.com/charsets/ref_html_entities_4.asp) like `&hearts;` and `&frac12;` or `&uuml;`, and numerical unicode character entities in both decimal (e.g. `&#926;`) and hexadeximal (`&#x39E;`), so you can easily embed characters, and even color them, so to write out "I ♥ PS" with a red heart you can just:

```posh
"I $(Text "&hearts;" -Fg Red) PS"
```

### Pansies also provides a couple of important classes:

*RgbColor* is a powerful representation of RGB colors with support for parsing CSS style color strings "#RRGGBB" and XTerm indexes, as well as handling the ConsoleColor values PowerShell users are used to. In addition to that, it has conversions to other color spaces for the purpose of doing color math like generating palettes and gradients, etc. The `ToString()` implementation shows the properties, but there is an overload which takes a boolean for Background or Foreground and renders to ANSI escape sequences. It has built-in palette for XTerm, and a built-in ConsoleColor palette which (on Windows) sniffs the current console's current color configuration. It uses these palettes to automatically downsample RGB colors to the nearest match when it's necessary to render in those color spaces.

*Text* is a text class which contains BackgroundColor and ForegroundColor properties and a `ToString()` implementation based on VT escape sequences.  It also supports HTML named enties like the `&hearts;` example above.

There are also *Palette* classes which support the XTerm 256 color palette and the default ConsoleColor 16 color palette (which currently supports loading the actual palette of the console in Windows, but may _therefore_ break off of Windows), with the ability to find the closest match to any RgbColor.

You can play with setting `[PoshCode.Pansies.RgbColor]::ColorMode` to change how the colors are down-sampled, and modify the actual palettes in `[PoshCode.Pansies.RgbColor]::ConsolePalette` and `[PoshCode.Pansies.RgbColor]::XTermPalette`


## Contribute

The end goal for this project is for the Color and Text classes (possibly without the color space conversions) to make it into the core PowerShell product, so what I'm most interested in here is [any ideas](https://github.com/PoshCode/Pansies/issues) people have for a better user experience for writing text and partially colored text, as well as other ANSI Virtual Terminal escape sequences.

For the sake of PowerShell 5, I intend to keep this module around, and features that don't belong in PowerShell core for awhile, and we'll even make _some_ attempt to support older versions of PowerShell for Windows (running in ConEmu with ANSI support, or just downsampling everything to ConsoleColors).
