using ColorMine.Palettes;
using PoshCode.Pansies.Console;
using PoshCode.Pansies.Palettes;
using System.Collections.Generic;
using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Set","ConsolePalette", DefaultParameterSetName = "Palette")]
    public class SetConsolePaletteCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Palette")]
        public IList<RgbColor> Palette { get; set; }

        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Colors")]
        public RgbColor[] Colors { get; set; }

        private Palette<RgbColor> colors = new Palette<RgbColor>();

        private string parameterName = "Palette";

        /// <summary>
        /// Determines whether the palette is set only for the current console or for the default settings as well
        /// </summary>
        [Parameter()]
        public SwitchParameter Default { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            // If they are using the Colors parameter set, we're collecting all the colors as they come in
            if (Colors != null)
            {
                parameterName = "Colors";
                foreach (var color in Colors)
                {
                    colors.Add(color);
                }
            }
        }
        protected override void EndProcessing()
        {
            base.EndProcessing();

            // if they used the Colors parameter set, move the collection into the Palette
            if (Palette == null && colors.Count >= 16)
            {
                Palette = colors;
            }

            // Only do anything if they passed enough colors
            if (Palette.Count >= 16)
            {
                if (Default)
                {
                    WindowsHelper.SetDefaultConsolePalette(Palette);
                }
                WindowsHelper.SetCurrentConsolePalette(Palette);
            }
            else
            {
                WriteError(new ErrorRecord(new PSArgumentException("You must provide all 16 colors to set the Console Palette", parameterName), "InsufficientColors", ErrorCategory.InvalidData, Palette));
            }
        }
    }
}
