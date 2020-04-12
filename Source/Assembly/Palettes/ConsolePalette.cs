using ColorMine.ColorSpaces.Comparisons;
using ColorMine.Palettes;

namespace PoshCode.Pansies.Palettes
{
    public class ConsolePalette : Palette<RgbColor>
    {
        public override IColorSpaceComparison ComparisonAlgorithm { get; set; } = new CieDe2000Comparison();

        public ConsolePalette()
        {
            // The ConsolePalette needs to be in the .NET ConsoleColors enum order
            Add(new RgbColor(0x00, 0x00, 0x00));
            Add(new RgbColor(0x00, 0x00, 0x80));
            Add(new RgbColor(0x00, 0x80, 0x00));
            Add(new RgbColor(0x00, 0x80, 0x80));
            Add(new RgbColor(0x80, 0x00, 0x00));
            Add(new RgbColor(0x80, 0x00, 0x80));
            Add(new RgbColor(0x80, 0x80, 0x00));
            Add(new RgbColor(0xc0, 0xc0, 0xc0));
            Add(new RgbColor(0x80, 0x80, 0x80));
            Add(new RgbColor(0x00, 0x00, 0xff));
            Add(new RgbColor(0x00, 0xff, 0x00));
            Add(new RgbColor(0x00, 0xff, 0xff));
            Add(new RgbColor(0xff, 0x00, 0x00));
            Add(new RgbColor(0xff, 0x00, 0xff));
            Add(new RgbColor(0xff, 0xff, 0x00));
            Add(new RgbColor(0xff, 0xff, 0xff));
        }
    }
}
