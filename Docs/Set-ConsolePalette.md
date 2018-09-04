---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# Set-ConsolePalette

## SYNOPSIS
Set the 16 color console palette

## SYNTAX

### Palette
```
Set-ConsolePalette [-Palette] <IList`1[RgbColor]> [-Default] [<CommonParameters>]
```

### Colors
```
Set-ConsolePalette [-Colors] <RgbColor[]> [-Default] [<CommonParameters>]
```

## DESCRIPTION
Set the 16 color console palette (and optionally, the default palette)

## EXAMPLES

### Example 1
```
PS C:\> Get-ConsolePalette | Set-ConsolePalette -Default
```

This example shows how to use Set-ConsolePalette with pipeline input to upgrade the current palette to the default palette.

## PARAMETERS

### -Colors
Colors to be used for the console palette. Supports piping in a collection of colors.

```yaml
Type: RgbColor[]
Parameter Sets: Colors
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Default
If set, set the default console palette in addition to the current palette

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

### -Palette
The palette to set the console colors. Note that a Palette is just a `List` of 16 (or more) colors.

```yaml
Type: IList[RgbColor]
Parameter Sets: Palette
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### [IList[RgbColor]] A ConsolePalette or other list of 16 RGBColor values

### [RgbColor[]]

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Get-ConsolePalette](Get-ConsolePalette.md)