---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# ConvertFrom-iTermColors

## SYNOPSIS
Convert a .itermcolors XML file into a (partial) PANSIES theme for your PowerShell console

## SYNTAX

```
ConvertFrom-iTermColors [[-Theme] <String>] [-Force] [-Update] [-Passthru] [[-Scope] <String>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Generate a PANSIES PowerShell theme from iTermColors.

If you start with a working iTerm theme, you will get the 16 ConsoleColors plus the foreground/background, but they often include the colors for selection and emphasis in PSReadLine, as well.

For a huge collection of iTermColors files you can use, visit https:#ithub.com/mbadolato/iTerm2-Color-Schemes

## EXAMPLES

### EXAMPLE 1
```
ConvertFrom-iTermColors Argonaut
```

Will find the Argonaut.itermcolors file in the current directory (or in the module storage path)

## PARAMETERS

### -Theme
The name of (or full path to) an XML PList itermcolors scheme
If you provide just a name, will search recursively for an .itermcolors file in the current folder

```yaml
Type: String
Parameter Sets: (All)
Aliases: PSPath, Name

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Force
Overwrite any existing theme file

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

### -Update
Update any existing theme file. You can use this to complement a previously exported VSCode theme with matching ConsoleColors.

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
Output the theme after converting it so you can, for instance, pipe it to Show-Theme

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

### -Scope
User or Machine (supports storing themes per-user or shared for all users). Defaults to User.


```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: User
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows the file names that would be converted if the conversion runs.
The conversion is not run.

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

### -Confirm
Prompts you with the file names for confirmation before running the conversion.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### [string] the name of the theme

## OUTPUTS

### [PSCustomObject] representing the theme (if -Pasthru)

## NOTES
The conversion outputs a .theme.psd1 file to AppData or ProgramData

## RELATED LINKS

[ConvertFrom-VSCodeTheme](ConvertFrom-VSCodeTheme.md)
[Export-Theme](Export-Theme.md)
[Import-Theme](Import-Theme.md)
[Get-Theme](Get-Theme.md)
[Show-Theme](Show-Theme.md)