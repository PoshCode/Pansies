using ColorMine.ColorSpaces.Comparisons;
using ColorMine.Palettes;
using PoshCode.Pansies.Console;

namespace PoshCode.Pansies.Palettes
{
    public class ConsolePalette : Palette<RgbColor>
    {
        public override IColorSpaceComparison ComparisonAlgorithm { get; set; } = new CieDe2000Comparison();

        public ConsolePalette()
        {
            // The ConsolePalette needs to be in the .NET ConsoleColors enum order
            base.Add(new RgbColor(0x00, 0x00, 0x00));
            base.Add(new RgbColor(0x00, 0x00, 0x80));
            base.Add(new RgbColor(0x00, 0x80, 0x00));
            base.Add(new RgbColor(0x00, 0x80, 0x80));
            base.Add(new RgbColor(0x80, 0x00, 0x00));
            base.Add(new RgbColor(0x80, 0x00, 0x80));
            base.Add(new RgbColor(0x80, 0x80, 0x00));
            base.Add(new RgbColor(0xc0, 0xc0, 0xc0));
            base.Add(new RgbColor(0x80, 0x80, 0x80));
            base.Add(new RgbColor(0x00, 0x00, 0xff));
            base.Add(new RgbColor(0x00, 0xff, 0x00));
            base.Add(new RgbColor(0x00, 0xff, 0xff));
            base.Add(new RgbColor(0xff, 0x00, 0x00));
            base.Add(new RgbColor(0xff, 0x00, 0xff));
            base.Add(new RgbColor(0xff, 0xff, 0x00));
            base.Add(new RgbColor(0xff, 0xff, 0xff));

            // On Windows we can read the _actual_ values from the console API

            if (System.Environment.OSVersion.Platform <= System.PlatformID.Win32NT) {
                try
                {
                    var currentColors = WindowsHelper.GetCurrentColorset();

                    foreach (var color in currentColors.Keys)
                    {
                        base[(int)color] = currentColors[color];
                    }
                }
                catch { }
                finally { }
            } 
        }
    }
}
