---
external help file: Pansies-help.xml
Module Name: Pansies
online version: 
schema: 2.0.0
---

# Get-Theme

## SYNOPSIS
List available PANSIES themes, optionally filtering

## SYNTAX

```
Get-Theme [[-Name] <String>] [-ConsoleColors] [-PSReadline] [<CommonParameters>]
```

## DESCRIPTION
List available PANSIES themes, optionally filtering by partial name or functionality. Specifically, there are switches to only return themes which have full ConsoleColors and/or PSReadline colors defined.

## EXAMPLES

### Example 1
```
PS C:\> Get-Theme -ConsoleColors -PSReadline
```

Show all themes which contain both Console colors and PSReadline colors

## PARAMETERS

### -Name
The name of the theme(s) to show. Supports wildcards, and defaults to * everything.```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: 1
Default value: *
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConsoleColors
If set, only returns themes that include ConsoleColor

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

### -PSReadline
If set, only returns themes that include PSReadline Colors

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

[ConvertFrom-ITermColors]()
[ConvertFrom-VSCodeTheme]()
[Export-Theme]()
[Import-Theme]()
[Show-Theme]()