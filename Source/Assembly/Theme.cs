using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using ColorMine.Palettes;
using PoshCode.Pansies.Palettes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PoshCode.Pansies
{
    public class Theme : Dictionary<string, RgbColor>
    {
        public ConsolePalette ConsolePalette { get; } = new ConsolePalette();

    }
}
