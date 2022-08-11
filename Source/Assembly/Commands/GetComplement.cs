using PoshCode.Pansies.ColorSpaces;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Get","Complement")]
    public class GetComplementCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        [Alias("BackgroundColor")]
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

        [Parameter()]
        public SwitchParameter AsObject { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (!AsObject)
            {
                if (Passthru)
                {
                    WriteObject(Color);
                }

                WriteObject(Color.GetComplement(HighContrast, BlackAndWhite));
            }
            else
            {
                WriteObject(new PSObject(new {
                    BackgroundColor = Color,
                    ForegroundColor = Color.GetComplement(HighContrast, BlackAndWhite)
                }));
            }
        }
    }
}
