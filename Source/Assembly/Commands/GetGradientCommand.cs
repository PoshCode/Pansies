using PoshCode.Pansies.ColorSpaces;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Get","Gradient")]
    public class GetGradientCommand : PSCmdlet
    {
        /// <summary>
        /// Gets or Sets the start color for the gradient
        /// </summary>
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias("SC")]
        public RgbColor StartColor { get; set; }

        /// <summary>
        /// Gets or Sets the end color for the gradient
        /// </summary>
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias("EC")]
        public RgbColor EndColor { get; set; }

        [Parameter(Position = 2)]
        [Alias("Length", "Count", "Steps")]
        [ValidateRange(3, int.MaxValue)]
        public int Width { get; set; } = -1;

        [Parameter(Position = 3)]
        [ValidateRange(1, int.MaxValue)]
        public int Height { get; set; } = 1;

        [Parameter]
        public SwitchParameter Reverse { get; set; }

        [Parameter]
        public SwitchParameter Flatten { get; set; }

        [Parameter(ValueFromPipeline = true)]
        [ValidateSet("Hsl", "Lch", "Rgb", "Lab", "Xyz")]
        public string ColorSpace { get; set; } = "Lch";

        private static System.Reflection.MethodInfo Get2DGradient;

        static GetGradientCommand()
        {
            Get2DGradient = typeof(Gradient).GetMethod("Get2DGradient", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Width <= 0)
            {
                PSHost host = GetVariableValue("Host") as PSHost;
                Width = host.UI.RawUI.WindowSize.Width;
            }
            Height = System.Math.Max(1, Height);
            Width = System.Math.Max(1, Width);
            ColorSpace = ColorSpace.Substring(0, 1).ToUpperInvariant() + ColorSpace.Substring(1).ToLowerInvariant();
            var colorType = GetType().Assembly.GetType($"PoshCode.Pansies.ColorSpaces.{ColorSpace}");
            var colors = (RgbColor[][])Get2DGradient.MakeGenericMethod(typeof(RgbColor), colorType).Invoke(null,
                                            new object[] { StartColor, EndColor, Height, Width, Reverse.ToBool() });

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
