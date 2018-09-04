---
external help file: Pansies-help.xml
Module Name: Pansies
online version: 
schema: 2.0.0
---

# Show-Theme

## SYNOPSIS
Show a preview of a PANSIES theme

## SYNTAX

```
Show-Theme [[-Name] <String>] [-Tiny] [-MoreText] [-NoCodeSample] [<CommonParameters>]
```

## DESCRIPTION
Show a preview of a theme, using the old-school gYw table, a simple side-by-side view of bright and dark colors, a code sample, and optionally a larger block of text colors on common backgrounds

## EXAMPLES

### Example 1
```
PS C:\> Show-Theme Zenburn -Tiny
```

Shows a small version of the preview with just the side-by-side colors and code preview

## PARAMETERS

### -Name
The name of the color theme to display

```yaml
Type: String
Parameter Sets: (All)
Aliases: Theme, PSPath

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MoreText
Adds an additional block with more text on the most common background colors

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

### -Tiny
Supresses the large color table

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

### -NoCodeSample
{{Fill NoCodeSample Description}}

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

### System.String

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS

[ConvertFrom-ITermColors]()
[ConvertFrom-VSCodeTheme]()
[Export-Theme]()
[Import-Theme]()
[Get-Theme]()