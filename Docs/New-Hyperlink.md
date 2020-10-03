---
external help file: Pansies-help.xml
online version:
schema: 2.0.0
---

# New-Hyperlink

## SYNOPSIS
Create a hyperlink with the specified Uri, and optionally using different text, background and foreground colors

## SYNTAX

```
New-Hyperlink [-Uri] <String> [[-Object] <Object>] [-Separator <Object>] [-BackgroundColor <RgbColor>] [-ForegroundColor <RgbColor>] [-LeaveColor] [-IgnoreEntities] [-Passthru]
```

## DESCRIPTION
Create a hyperlink with the specified Uri, using ANSI Virtual Terminal escape sequences.
As with New-Text, there's full support for setting background and foreground colors, and rendering HTML-style entities.

With -Passthru, returns the Text object, but normally outputs the text string with the Uri hyperlink embedded.

## EXAMPLES

### Example 1
```
PS C:\> New-Hyperlink https://PoshCode.org -ForegroundColor Green

`e[92m`e]8;;https://PoshCode.org`ahttps://PoshCode.org`e]8;;`a`e[39m
```

Generates a hyperlink to https://PoshCode.org with the text https://PoshCode.org and a green foreground color.

This use, without text (the `-Object` parameter) and with a highlight color, is the most compatible use, because if the terminal doesn't support links, at least the URL will be visible, and highlighted in color

### Example 2
```
PS C:\> "Please visit $(New-Hyperlink https://PoshCode.org PoshCode)"

Please visit `e]8;;https://PoshCode.org`aPoshCode`e]8;;`a
```

Generates a hyperlink to https://PoshCode.org with the text "PoshCode". You should be careful of this syntax, where you don't include the full URL in the display, because terminals which don't support hyperlinks (like the default Windows console) will not display the Url, nor any indication that it should be a link.

## PARAMETERS

### -Uri
Specifies the Uri to link to.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -BackgroundColor
The background color. You may specify it as CSS hex "#RRGGBB" (or just "RRGGBB") or as an XTerm index "xt123" (or just "123") or as a ConsoleColor like "Red" or "DarkRed"...

```yaml
Type: RgbColor
Parameter Sets: (All)
Aliases: Bg

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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
Accept pipeline input: False
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
Specifies objects to display as the text of the link.

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, FromRemainingArguments)
Accept wildcard characters: False
```

### -Separator
Specifies a separator string to output between objects displayed on the console.

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

### -Passthru
If set, outputs a Text object, rather than simple string

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

## INPUTS

### System.Object

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS

