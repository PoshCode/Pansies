---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# Get-ConsolePalette

## SYNOPSIS
Returns the 16 Color Palette configured for the console

## SYNTAX

```
Get-ConsolePalette [-Default] [<CommonParameters>]
```

## DESCRIPTION
Returns the 16 Color palette defined for the current console (or the defaults for the system)

## EXAMPLES

### Example 1
```
PS C:\> Get-ConsolePalette -Default
```

Get the default console palette

### Example 2
```
PS C:\> Get-ConsolePalette
```

Get the current console palette

### Example 3
```
PS C:\> (Get-ConsolePalette).FindClosestColor([PoshCode.Pansies.RgbColor]"#336699")
```

Finds the closest color in the current 16 color console palette to the specified #336699 shade of blue

## PARAMETERS

### -Default
If set, attempts to read the default console colortable values from the registry, rather than the palette of the current console.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### ConsolePalette

## NOTES

## RELATED LINKS

[Set-ConsolePalette](Set-ConsolePalette.md)