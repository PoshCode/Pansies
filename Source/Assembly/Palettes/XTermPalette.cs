using ColorMine.ColorSpaces.Comparisons;
using ColorMine.Palettes;

namespace PoshCode.Pansies.Palettes
{
    /// <summary>
    /// This is the xTerm color palette with the default colors.
    /// On Windows, in the default terminal there's no way to customize them (yet).
    /// </summary>
    public class XTermPalette : Palette<RgbColor>
    {
        public override IColorSpaceComparison ComparisonAlgorithm { get; set; } = new CieDe2000Comparison();

        public XTermPalette()
        {
            // Sixteen Console Colors
            Add(new RgbColor { R = 0x00, G = 0x00, B = 0x00 });
                     
            Add(new RgbColor { R = 0x80, G = 0x00, B = 0x00 });
            Add(new RgbColor { R = 0x00, G = 0x80, B = 0x00 });
            Add(new RgbColor { R = 0x80, G = 0x80, B = 0x00 });
            Add(new RgbColor { R = 0x00, G = 0x00, B = 0x80 });
            Add(new RgbColor { R = 0x80, G = 0x00, B = 0x80 });
            Add(new RgbColor { R = 0x00, G = 0x80, B = 0x80 });
                     
            Add(new RgbColor { R = 0xc0, G = 0xc0, B = 0xc0 });
            Add(new RgbColor { R = 0x80, G = 0x80, B = 0x80 });

            Add(new RgbColor { R = 0xff, G = 0x00, B = 0x00 });
            Add(new RgbColor { R = 0x00, G = 0xff, B = 0x00 });
            Add(new RgbColor { R = 0xff, G = 0xff, B = 0x00 });
            Add(new RgbColor { R = 0x00, G = 0x00, B = 0xff });
            Add(new RgbColor { R = 0xff, G = 0x00, B = 0xff });
            Add(new RgbColor { R = 0x00, G = 0xff, B = 0xff });

            Add(new RgbColor { R = 0xff, G = 0xff, B = 0xff });

            // 215 Color Table
            int[] colorValue = new[] { 0x00, 0x5f, 0x87, 0xaf, 0xd7, 0xff };

            for (var r = 0; r <= 5; r++)
            {
                for (var g = 0; g <= 5; g++)
                {
                    for (var b = 0; b <= 5; b++)
                    {
                        var index = 16 + 36 * r + 6 * g + b;
                        Add(new RgbColor
                        {
                            R = colorValue[r],
                            G = colorValue[g],
                            B = colorValue[b]
                        });
                    }
                }
            }

            // 24 Shades of Gray
            for (var x = 0; x < 24; x++)
            {
                Add(new RgbColor
                { 
                    R = 0x8 + (0xA * x),
                    G = 0x8 + (0xA * x),
                    B = 0x8 + (0xA * x)
                });
            }
        }
    }
}
