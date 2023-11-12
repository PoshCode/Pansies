filter Out-Colors {
    [CmdletBinding()]
    param(
        [HslEnumerator]$Colors = @{ Start = "GoldenRod"; HueStep = 18 },

        [Parameter(ValueFromPipeline)]
        $FormatInfoData
    )
    # Only color the data (not the headers)
    if ($_.GetType().Name -eq "FormatEntryData") {
        # Listview
        @($_.FormatEntryInfo.ListViewFieldList).Where{ $_ }.ForEach{
            $null = $Colors.MoveNext()
            $_.FormatPropertyField.propertyValue = $Colors.Current.ToVt() + $_.FormatPropertyField.propertyValue
        }
        # Tableview
        @($_.FormatEntryInfo.FormatPropertyFieldList).Where{ $_ }.ForEach{
            $null = $Colors.MoveNext()
            $_.propertyValue = $Colors.Current.ToVt() + $_.propertyValue
        }
    }
    # Pass through everything
    $_
}