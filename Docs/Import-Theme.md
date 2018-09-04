---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# Import-Theme

## SYNOPSIS
Imports a PANSIES .theme.psd1 file to style your Console, Host, and PSReadLine

## SYNTAX

```
Import-Theme [[-Name] <String>] [-SkipDefault] [<CommonParameters>]
```

## DESCRIPTION
Imports the theme and applies colors if they're defined to the Console, to PSReadline and to Host.PrivateData (the colors of Error/Warning/Verbose/etc)

## EXAMPLES

### Example 1
```
PS C:\> Import-Theme Zenburn
```

Shows how to import the Zenburn theme by name

## PARAMETERS

### -Name
A theme to import (can be the name of an installed PANSIES theme, or the full path to a psd1 file)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipDefault
By default, imported themes will update the default console colors (if they define console colors)
If SkipDefault is set, Import-Theme will leave the default Console Colors alone

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[ConvertFrom-ITermColors](ConvertFrom-ITermColors.md)
[ConvertFrom-VSCodeTheme](ConvertFrom-VSCodeTheme.md)
[Export-Theme](Export-Theme.md)
[Get-Theme](Get-Theme.md)
[Show-Theme](Show-Theme.md)