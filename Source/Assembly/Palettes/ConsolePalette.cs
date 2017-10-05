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
            // On Windows we can read the _actual_ values from the console API
            #if NET451

            if(System.Environment.OSVersion.Platform <= System.PlatformID.Win32NT) {
                var currentColors = WindowsHelper.GetCurrentColorset();
                // TODO: there should be a way to force the palette to a certain capacity
                // For now, loop and call ADD, then loop again to make sure they're in the right order
                foreach (var color in currentColors.Values)
                {
                    base.Add(color);
                }

                foreach (var color in currentColors.Keys)
                {
                    base[(int)color] = currentColors[color];
                }
            } else {

            #endif
                base.Add(new RgbColor(0x00, 0x00, 0x00));
                base.Add(new RgbColor(0x80, 0x00, 0x00));
                base.Add(new RgbColor(0x00, 0x80, 0x00));
                base.Add(new RgbColor(0x80, 0x80, 0x00));
                base.Add(new RgbColor(0x00, 0x00, 0x80));
                base.Add(new RgbColor(0x80, 0x00, 0x80));
                base.Add(new RgbColor(0x00, 0x80, 0x80));
                base.Add(new RgbColor(0xc0, 0xc0, 0xc0));
                base.Add(new RgbColor(0x80, 0x80, 0x80));
                base.Add(new RgbColor(0xff, 0x00, 0x00));
                base.Add(new RgbColor(0x00, 0xff, 0x00));
                base.Add(new RgbColor(0xff, 0xff, 0x00));
                base.Add(new RgbColor(0x00, 0x00, 0xff));
                base.Add(new RgbColor(0xff, 0x00, 0xff));
                base.Add(new RgbColor(0x00, 0xff, 0xff));
                base.Add(new RgbColor(0xff, 0xff, 0xff));

            #if NET451
            }
            #endif
        }
    }
}
