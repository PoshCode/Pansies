using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using System.Collections.Generic;

namespace ColorMine.Palettes
{
    public interface IPalette<T> : IList<T> where T : IColorSpace, new()
    {
        /// <summary>
        /// A <see cref="IColorSpaceComparison"/> used to find the closest match from a Palette to another color
        /// </summary>
        IColorSpaceComparison ComparisonAlgorithm { get; set; }

        /// <summary>
        /// Find the closest match in the palette to the specified color
        /// </summary>
        /// <param name="color">A color to match</param>
        /// <returns>The closest color that's already in the palette</returns>
        T FindClosestColor(IColorSpace color);

        /// <summary>
        /// Find the Palette index of the closest match to the specified color
        /// </summary>
        /// <param name="color">A color to match</param>
        /// <returns>The index of the closest color that's already in the palette</returns>
        int FindClosestColorIndex(IColorSpace color);
    }
}