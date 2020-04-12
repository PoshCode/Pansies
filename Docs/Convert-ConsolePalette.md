---
external help file: Pansies-help.xml
online version:
schema: 2.0.0
---

# Convert-ConsolePalette

## SYNOPSIS
Converts the current 16 Color console palette by shifting either the dark or light colors to darker or lighter shades.

## SYNTAX

### Dark (Default)
```
Convert-ConsolePalette [-DarkShift] <Int32> [-Copy] [-WhatIf] [-Confirm]
```

### Bright
```
Convert-ConsolePalette -BrightShift <Int32> [-Copy] [-WhatIf] [-Confirm]
```

## DESCRIPTION
Converts a 16 Color console palette by shifting either the dark or light colors to darker or lighter shades.

## EXAMPLES

### Example 1
```
PS C:\> Convert-ConsolePalette -BrightShift 20 -Copy
```

Copies the Dark* colors from the console palette to the bright side and brightens them by 20 (of 100) luminance.

## PARAMETERS

### -BrightShift
How much to shift the bright colors. Positive values make the colors brighter, negative values make them darker

```yaml
Type: Int32
Parameter Sets: Bright
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Copy
By default, the colors are modified in-place. If copy is set:
- the dark colors start with the value of the bright colors
- the light colors start at the value of the dark colors

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

### -DarkShift
How much to shift the dark colors. Positive values make the colors brighter, negative values make them darker

```yaml
Type: Int32
Parameter Sets: Dark
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### None


## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS

