---
external help file: Pansies-help.xml
Module Name: Pansies
online version: 
schema: 2.0.0
---

# Export-PList

## SYNOPSIS
Convert an object to an XML or Binary PList file.

## SYNTAX

```
Export-PList -InputObject <Object[]> [-Path] <String> [-Binary] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Exports simple data to an Apple-compatible Property List (PList) file.

Note that this function is only lightly tested, and only supports strings, integers and doubles, dates, bytes, and booleans, as well as arrays or dictionaries of them (as long as the dictionary has string keys).

## EXAMPLES

### Example 1
```
PS C:\> (Get-ConsolePalette).ForEach([string]) | Export-PList -Path palette.xml
```

Exports the palette as a PList XML file -- note that this isn't anything like the format that itermcolors files use -- they export colors as dictionaries of Red, Green, Blue ...

## PARAMETERS

### -Binary
If set, Export-PList creates a binary file rather than an XML file

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

### -InputObject
The object(s) to convert. Must be a date, string, double, integer, byte, bool, or an array or dictionary with only those in it.

```yaml
Type: Object[]
Parameter Sets: (All)
Aliases: io

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Path
The path where the export will be written

```yaml
Type: String
Parameter Sets: (All)
Aliases: PSPath

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[Import-PList](Import-PList.md)