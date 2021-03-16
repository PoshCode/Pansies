using PoshCode.Pansies.ColorSpaces;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Get","ColorWheel")]
    public class GetColorWheel : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Position = 0)]
        [Alias("StartColor")]
        public RgbColor Color { get; set; } = new RgbColor(255, 0, 0);

        // Force the luminance to have "enough" contrast
        [Parameter()]
        public int Count { get; set; } = 7;

        // Assume there are only 16 colors
        [Parameter()]
        public int HueStep { get; set; } = 50;

        [Parameter()]
        [Alias("LightStep")]
        public int BrightStep { get; set; } = 4;

        // # If set, output the input $Color before the complement
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (Passthru) {
                Color.Mode = ColorMode.Rgb24Bit;
                WriteObject(Color);
            }
        }
        protected override void EndProcessing()
        {
            base.EndProcessing();

            WriteObject(Gradient.GetRainbow(Color, Count, HueStep, BrightStep).Select( c => { c.Mode = ColorMode.Rgb24Bit; return c; }), true);
        }
    }
}
