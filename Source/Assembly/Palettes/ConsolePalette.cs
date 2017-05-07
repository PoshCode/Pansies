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
            // TODO: make this work in Linux: use the xTerm values as a backup?
            // on Windows we can read the _actual_ values from the console API
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
        }
    }
}
