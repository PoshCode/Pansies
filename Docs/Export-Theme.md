---
external help file: Pansies-help.xml
Module Name: Pansies
online version:
schema: 2.0.0
---

# Export-Theme

## SYNOPSIS

Exports the current console and PSReadLine colors as a theme

## SYNTAX

```
Export-Theme [-Name] <String> [-Update <String>] [-Force] [-Passthru] [-Scope <String>] [<CommonParameters>]
```

## DESCRIPTION

Read the current values for the 16 console colors and PSReadLine color options and exports them to a .theme.psd1 file for use with Pansies Theme commands.

## EXAMPLES

### Example 1
```powershell
PS C:\> Export-Theme Default
```

Saves your current settings as "Default" so you can re-import them later to revert

## PARAMETERS

### -Force

Overwrite any existing theme file with the given name

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

### -Name

The name of the theme to export the current settings to

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
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
Accepted values: User, Machine

Required: False
Position: Named
Default value: User
Accept pipeline input: False
Accept wildcard characters: False
```

### -Update
Update any existing theme file. You can use this to complement a previously exported VSCode theme with matching ConsoleColors.

```yaml
Type: String
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
[string] the name of the theme

## OUTPUTS
[PSCustomObject] representing the theme (if -Pasthru)

## NOTES
Generates a .theme.psd1 file to AppData or ProgramData

## RELATED LINKS
ConvertFrom-ITermColors
ConvertFrom-VSCodeTheme
Import-Theme
Get-Theme
Show-Theme