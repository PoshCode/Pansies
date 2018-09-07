using ColorMine.ColorSpaces.Comparisons;
using ColorMine.Palettes;
using PoshCode.Pansies.Console;
using System.Collections.Generic;

namespace PoshCode.Pansies.Palettes
{
    public class ConsolePalette : Palette<RgbColor>
    {
        public override IColorSpaceComparison ComparisonAlgorithm { get; set; } = new CieDe2000Comparison();

        public ConsolePalette(bool defaultColors = false) : this(defaultColors, false)
        {

        }

        internal ConsolePalette(bool defaultColors = false, bool addScreenAndPopup = false)
        {
            if (!defaultColors)
            {
                if (System.Environment.OSVersion.Platform <= System.PlatformID.Win32NT)
                {
                    try
                    {
                        this.LoadCurrentColorset(addScreenAndPopup);
                    }
                    catch
                    {
                        LoadDefaults();
                    }
                }
            }
            else
            {
                LoadDefaults();
            }
        }

        private void LoadDefaults()
        {
            Clear();

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
