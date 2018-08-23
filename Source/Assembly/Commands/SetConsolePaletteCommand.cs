using ColorMine.Palettes;
using PoshCode.Pansies.Console;
using PoshCode.Pansies.Palettes;
using System.Collections.Generic;
using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Set","ConsolePalette")]
    public class SetConsolePaletteCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Palette")]
        public Palette<RgbColor> Palette { get; set; }

        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Colors")]
        public RgbColor[] Colors { get; set; }

        private Palette<RgbColor> colors = new Palette<RgbColor>();

        /// <summary>
        /// Determines whether the palette is set only for the current console or for the default settings as well
        /// </summary>
        [Parameter()]
        public SwitchParameter Default { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Colors != null)
            {
                foreach (var color in Colors)
                {
                    colors.Add(color);
                }
            }
        }
        protected override void EndProcessing()
        {
            base.EndProcessing();

            if (Palette == null && colors.Count >= 16)
            {
                Palette = colors;
            }

            if (Palette.Count >= 16)
            {
                if (Default)
                {
                    WindowsHelper.SetDefaultConsolePalette(Palette);
                }
                WindowsHelper.SetCurrentConsolePalette(Palette);
            }
        }
    }
}
