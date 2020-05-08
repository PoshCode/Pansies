using PoshCode.Pansies.ColorSpaces;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Get","Complement")]
    public class GetComplementCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public RgbColor Color { get; set; }

        // Force the luminance to have "enough" contrast
        [Parameter()]
        [Alias("ForceContrast")]
        public SwitchParameter HighContrast { get; set; }

        // Assume there are only 16 colors
        [Parameter()]
        [Alias("ConsoleColor")]
        public SwitchParameter BlackAndWhite { get; set; }

        // # If set, output the input $Color before the complement
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void EndProcessing()
        {
            base.EndProcessing();

            if (Passthru)
            {
                WriteObject(Color);
            }

            WriteObject(Color.GetComplement(HighContrast, BlackAndWhite));
        }
    }
}
