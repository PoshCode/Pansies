using PoshCode.Pansies.ColorSpaces;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Get","Gradient")]
    public class GetGradientCommand : PSCmdlet
    {
        /// <summary>
        /// Gets or Sets the background color for the block
        /// </summary>
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias("SC")]
        public RgbColor StartColor { get; set; }

        /// <summary>
        /// Gets or Sets the foreground color for the block
        /// </summary>
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias("EC")]
        public RgbColor EndColor { get; set; }

        [Parameter(Position = 2)]
        [Alias("Length", "Count", "Steps")]
        [ValidateRange(3, int.MaxValue)]
        public int Width { get; set; } = 1;

        [Parameter(Position = 3)]
        [ValidateRange(1, int.MaxValue)]
        public int Height { get; set; } = 1;

        [Parameter]
        public SwitchParameter Reverse { get; set; }

        [Parameter]
        public SwitchParameter Flatten { get; set; }

        [Parameter]
        [ValidateSet("CMY", "CMYK", "LAB", "LCH", "LUV", "HunterLAB", "HSL", "HSV", "HSB", "RGB", "XYZ", "YXY")]
        public string ColorSpace { get; set; } = "HunterLab";

        private static System.Reflection.MethodInfo GetGradient;

        static GetGradientCommand()
        {
            GetGradient = typeof(Gradient).GetMethod("GetGradient", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();

            if (Width == 1)
            {
                PSHost host = GetVariableValue("Host") as PSHost;
                Width = host.UI.RawUI.WindowSize.Width;
            }
            Height = System.Math.Max(1, Height);
            Width = System.Math.Max(1, Width);

            var colorType = GetType().Assembly.GetType($"PoshCode.Pansies.ColorSpaces.{ColorSpace}");
            var colors = (RgbColor[][])GetGradient.MakeGenericMethod(colorType).Invoke(null, new object[] { StartColor, EndColor, Height, Width, Reverse.ToBool() });

            if (Flatten || Height == 1)
            {
                foreach(var row in colors)
                {
                    foreach(var col in row)
                    {
                        WriteObject(col);
                    }
                }
            }
            else
            {
                WriteObject(colors, false);
            }
        }
    }
}
