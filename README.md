# <img src="https://github.com/Jaykul/Pansies/blob/resources/Pansies_64.gif?raw=true" alt="Pansies" />Pansies

<strong>P</strong>owershell <strong>ANSI E</strong>scape <strong>S</strong>equences

This module contains classes and functions for doing ANSI colored output and positioning using Virtual Terminal escape sequences on platforms where they are supported: Windows 10, Linux, OS X, etc.

```
I ♥ PS> function prompt { -join [TextSpan[]]( "I ", @{ fg = "DarkRed"; text = [char]0x2665 }, " PS> ") }
```

My current primary goal is to create a **great** <strong>u</strong>ser e<strong>x</strong>perience around writing code to output colored things in the console. So for instance, the example prompt above currently works (the :heart: comes out dark red), but I'm not sure if that's really the best we can do as far as an interface for coloring part of a line of text. Do you [have a better idea](https://github.com/PoshCode/Pansies/issues)?


NOTE: We will make _some_ attempt to support older versions of PowerShell and Windows running in ConEmu with ANSI support, but it's not a priority.
