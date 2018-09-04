---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# ConvertFrom-VSCodeTheme

## SYNOPSIS
Convert a VSCode Theme file into a partial theme

## SYNTAX

```
ConvertFrom-VSCodeTheme [[-Theme] <String>] [-Force] [-Update] [-Passthru] [[-Scope] <String>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Attempts to generate a PANSIES Powershell theme from a VSCode Theme.

This feature is still experimental, and so far, includes theming:

- The ConsoleColor palette (the base 16 ConsoleColors) from the \`terminal.ansi*\` colors in the VSCode theme
- The PSReadLine colors (requires PSReadline 2.0.0 beta 2) from various named scopes
- The PrivateData ConsoleColors (used for foreground of the output streams: Verbose, Error, Warning, Debug, Progress) from various named colors.

NOTE: for now, even if everything works, there may be some colors for PSReadLine that aren't set, or that are set incorrectly (depending on the theme).
If so, please let me know of themes you want to use or of colors that are wrong in the issues at https://GitHub.com/PoshCode/PANSIES/issues

## EXAMPLES

### EXAMPLE 1
```
PS C:\> ConvertFrom-VSCodeTheme Dark+
PS C:\> Import-Theme Dark+
```

This example shows how to convert the VSCode Dark+ default theme and then use it in your console.

## PARAMETERS

### -Theme
The name of (or full path to) a vscode json theme
E.g.
'Dark+' or 'Monokai'

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
Overwrite any existing theme completely

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
Update any existing theme file. You can use this to complement a previously exported iTerm color scheme with complementary PSReadline colors.

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

[ConvertFrom-ITermColors](ConvertFrom-ITermColors.md)
[Export-Theme](Export-Theme.md)
[Import-Theme](Import-Theme.md)
[Get-Theme](Get-Theme.md)
[Show-Theme](Show-Theme.md)