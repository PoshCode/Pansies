---
external help file: Pansies.dll-Help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# Get-ColorWheel

## SYNOPSIS

Get a range of colors from a starting point aiming to avoid repetition. Doesn't return the starting color unless -Passthru is specified.

## SYNTAX

```
Get-Gradient [[-Color] <RgbColor>] [-Count <int>] [-HueStep <int>] [[-BrightStep] <int>]
```

## DESCRIPTION

Get a range of colors that wrap around the hue of the HSL color spectrum.

NOTE: this calls Gradient.GetRainbow, but is named more correctly. When you ask for a lot of colors, the color wheel will wrap around. To avoid repeating colors, it defaults to a HueStep of 50 (meaning it goes a full 360 degrees and wraps around fter 7 steps), but it also changes the brightness, so by the time it wraps around, it's not the same shade. As a result, it doesn't actually repeat unless you ask for hundreds of colors, but the more colors you ask for, the more similar they get. You can tweak that by using BrightStep or HueStep

## EXAMPLES

### ---- Example 1 -------------------------------------------------------------

```
PS C:\> Get-ColorWheel Magenta -Bright 0
```

Get a 7-color rainbow. Since we specified Magenta as the starting point, the first returned color is Red.


### ---- Example 2 -------------------------------------------------------------

```
PS C:\> Get-ColorWheel Magenta
```

Gets a 2D gradient from the ConsoleColor Red to Blue, with as many colors as the current width of the console.


## PARAMETERS

### -BrightStep
How much to adjust the lightness (alias Light step)

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: LightStep

Required: False
Position: Named
Default value: 4
Accept pipeline input: False
Accept wildcard characters: False
```

### -Color
The starting color (as an RGB color value)

```yaml
Type: RgbColor
Parameter Sets: (All)
Aliases: StartColor

Required: False
Position: 0
Default value: Red
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Count
How many colors to return

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 7
Accept pipeline input: False
Accept wildcard characters: False
```

### -HueStep
How many degrees of the color wheel to go for a new color.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 50
Accept pipeline input: False
Accept wildcard characters: False
```

### -Passthru
If set, returns the start color in addition to the Count

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

### PoshCode.Pansies.RgbColor

## OUTPUTS

### PoshCode.Pansies.RgbColor

## NOTES

## RELATED LINKS
