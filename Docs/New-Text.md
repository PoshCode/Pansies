---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# New-Text

## SYNOPSIS
Create a Text object with specified background and foreground colors

## SYNTAX

```
New-Text [-Object] <Object> [-BackgroundColor <RgbColor>] [-Separator <Object>] [-ForegroundColor <RgbColor>]
 [-LeaveColor] [-IgnoreEntities] [<CommonParameters>]
```

## DESCRIPTION
Create a Text object with specified background and foreground colors, and rendering HTML-style entities like `&hearts;` and `&frac12;` or `&uuml;` and numerical unicode character entities in both decimal (e.g. `&#926;`) and hexadeximal (`&#x39E;`)

When this object is rendered to the host with .ToString(), it inserts ANSI Virtual Terminal escape sequences for the specified colors,
and by default, outputs escape sequences to clear those colors after the text.

## EXAMPLES

### Example 1
```
PS C:\> New-Text "&hearts;" -ForegroundColor Red


BackgroundColor ForegroundColor Object       ToString
--------------- --------------- ------       --------
                [101m [0m Red &hearts;[0m [91m?[39m[0m
```

Generates a text object with the hearts symbol (♥) in red. The output shows the BackgroundColor, ForegroundColor, Text (with the entity text in it) and the rendered output when the object is in a string, where the entity will be replaced with the hearts symbol and the colors will take effect.

### Example 2
```
PS C:\> "I $(New-Text "&hearts;" -ForegroundColor "#F00") PS"

I [38;2;255;0;0m?[39m PS
```

Outputs the text "I ♥ PS" with the heart in red.

## PARAMETERS

### -BackgroundColor
The background color. You may specify it as CSS hex "#RRGGBB" (or just "RRGGBB") or as an XTerm index "xt123" (or just "123") or as a ConsoleColor like "Red" or "DarkRed"...

```yaml
Type: RgbColor
Parameter Sets: (All)
Aliases: Bg

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ForegroundColor
The foreground color. You may specify it as CSS hex "#RRGGBB" (or just "RRGGBB") or as an XTerm index "xt123" (or just "123") or as a ConsoleColor like "Red" or "DarkRed"...

```yaml
Type: RgbColor
Parameter Sets: (All)
Aliases: Fg

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IgnoreEntities
If set, don't render the HTML Entities to characters (i.e. leave "&hearts;" as "&hearts;" instead of as "♥")

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

### -LeaveColor
If set, don't clear the colors at the end of the output.

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

### -Object
Specifies objects to display in the console.

```yaml
Type: Object
Parameter Sets: (All)
Aliases: 

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Separator
Specifies a separator string to the output between objects displayed on the console.

```yaml
Type: Object
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

### System.Object

### PoshCode.Pansies.RgbColor

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS

[Write-Host](Write-Host.md)