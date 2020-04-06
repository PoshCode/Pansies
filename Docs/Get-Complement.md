---
external help file: Pansies-help.xml
Module Name: Pansies
online version: 
schema: 2.0.0
---

# Get-Complement

## SYNOPSIS

Get the Hue complement color

## SYNTAX

```
Get-Complement [-Color] <RgbColor> [-ForceContrast] [-ConsoleColor] [-Passthru]
```

## DESCRIPTION

Returns a color that is 180 degrees opposite around the Hue component of the HSL color space.

The primary reason for Get-Complement to exist is for generating contrasting colors for foreground and background. For that reason, it's usually called with `-ForceContrast` mode, which changes the lightness or darkness to increase the contrast of the returned color.

It also has a `-ConsoleColor` switch that causes it to assume only 16 colors will work (PowerLine currently uses this mode by default because so many terminals don't support more --including Windows 10 prior to Creators Update). In ConsoleColor mode, it always returns White or Black.

## EXAMPLES

### ---- Example 1 -------------------------------------------------------------

```
PS C:\> Get-Complement Cyan
```

Gets the color Red back, as the complement for Cyan.


### ---- Example 2 -------------------------------------------------------------

```
PS C:\> $Background, $Foreground = Get-Complement Cyan -Passthru -ConsoleColor
PS C:\> Write-Host " Hello World " -Foreground $Foreground.ConsoleColor -Background $Background.ConsoleColor
```

This example shows how using `-Passthru` returns both the original color and the contrasting color, and how using `-ConsoleColor` results in a better contrast when you're being forced to use ConsoleColor (as with the built-in Write-Host command).

You can try the example without `-ConsoleColor` to see the difference: with it, you'll get Black on Cyan, without, you'll get Red on Cyan. Note that using -ForceContrast will make almost no difference if you're using the `ConsoleColor` property, because downsampling to 16 colors has to result in either Red or DarkRed...



## PARAMETERS

### -Color

The source color to calculate the complement of

```yaml
Type: RgbColor
Parameter Sets: (All)
Aliases: 

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ForceContrast
Force the luminance to have "enough" contrast

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

### -ConsoleColor
Assume there are only 16 colors. Return either black or white to get a readable contrast color.

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

### -Passthru

If set, output the input $Color before the complement

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

## INPUTS

### PoshCode.Pansies.RgbColor

The color to find a complement for


## OUTPUTS

### PoshCode.Pansies.RgbColor

The complement of the input color

## NOTES

## RELATED LINKS

