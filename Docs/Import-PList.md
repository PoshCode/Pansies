---
external help file: Pansies-help.xml
Module Name: Pansies
online version: 
schema: 2.0.0
---

# Import-PList

## SYNOPSIS
Convert an XML or Binary PList (property list) file to objects (arrays, string-\>object dictionaries, etc).

## SYNTAX

```
Import-PList [-Path] <String> [<CommonParameters>]
```

## DESCRIPTION
Import a PList (XML or binary file) as a string dictionary or array of objects.

Note that JSON and psd1 filesare not supported, since ConvertFrom-JSON and ConvertFrom-Metadata work fine.

## EXAMPLES

### Example 1
```
PS C:\> Import-PList argonaut.itermcolors
```

Import a PList XML file

## PARAMETERS

### -Path
The path to an XML or binary plist file (e.g. a .tmTheme or .itermcolors file)

```yaml
Type: String
Parameter Sets: (All)
Aliases: PSPath

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[Export-PList](Export-PList.md)