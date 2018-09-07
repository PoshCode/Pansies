using PoshCode.Pansies.Console;
using System.Linq;
using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Get","ConsolePalette")]
    public class GetConsolePaletteCommand : Cmdlet
    {
        /// <summary>
        /// Determines whether the returned palette is the current console palette or the default settings
        /// </summary>
        [Parameter()]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// Determines whether the returned palette is the current console palette or the default settings
        /// </summary>
        [Parameter()]
        public SwitchParameter AddScreenAndPopup { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            Palettes.ConsolePalette palette;

            if (Default)
            {
                palette = WindowsHelper.GetDefaultConsolePalette(AddScreenAndPopup);
            }
            else
            {
                palette = WindowsHelper.GetCurrentConsolePalette(AddScreenAndPopup);
            }

            WriteObject(palette);
        }
    }
}
