using System;

namespace ColorMine.ColorSpaces.Comparisons
{
    /// <summary>
    /// Implements the CMC l:c (1984) method of delta-e: http://en.wikipedia.org/wiki/Color_difference#CMC_l:c_.281984.29
    /// </summary>
    public class CmcComparison : IColorSpaceComparison
    {
        public const double DefaultLightness = 2.0;
        public const double DefaultChroma = 1.0;

        private readonly double _lightness;
        private readonly double _chroma;

        /// <summary>
        /// Create CMC l:c comparison with DefaultLightness and DefaultChroma values.
        /// </summary>
        public CmcComparison()
        {
            _lightness = DefaultLightness;
            _chroma = DefaultChroma;
        }

        /// <summary>
        /// Create CMC l:c comparison with specific lightness (l) and chroma (c) values.
        /// </summary>
        /// <param name="lightness"></param>
        /// <param name="chroma"></param>
        public CmcComparison(double lightness = DefaultLightness, double chroma = DefaultChroma)
        {
            _lightness = lightness;
            _chroma = chroma;
        }

        /// <summary>
        /// Calculates the CMC l:c (1984) delta-e value: http://en.wikipedia.org/wiki/Color_difference#CMC_l:c_.281984.29
        /// </summary>
        /// <param name="colorA"></param>
        /// <param name="colorB"></param>
        /// <returns></returns>
        public double Compare(IColorSpace colorA, IColorSpace colorB)
        {
            var aLab = colorA.To<Lab>();
            var bLab = colorB.To<Lab>();

            var deltaL = aLab.L - bLab.L;
            var h = Math.Atan2(aLab.B, aLab.A);

            var c1 = Math.Sqrt(aLab.A * aLab.A + aLab.B * aLab.B);
            var c2 = Math.Sqrt(bLab.A * bLab.A + bLab.B * bLab.B);
            var deltaC = c1 - c2;

            var deltaH = Math.Sqrt(
                (aLab.A - bLab.A) * (aLab.A - bLab.A) +
                (aLab.B - bLab.B) * (aLab.B - bLab.B) - 
                deltaC * deltaC);

            var c1_4 = c1 * c1;
            c1_4 *= c1_4;
            var t = 164 <= h && h <= 345
                        ? .56 + Math.Abs(.2 * Math.Cos(h + 168.0))
                        : .36 + Math.Abs(.4 * Math.Cos(h + 35.0));
            var f = Math.Sqrt(c1_4 / (c1_4 + 1900.0));

            var sL = aLab.L < 16 ? .511 : (.040975 * aLab.L) / (1.0 + .01765 * aLab.L);
            var sC = (.0638 * c1) / (1 + .0131 * c1) + .638;
            var sH = sC * (f * t + 1 - f);

            var differences = DistanceDivided(deltaL, _lightness * sL) +
                              DistanceDivided(deltaC, _chroma * sC) +
                              DistanceDivided(deltaH, sH);

            return Math.Sqrt(differences);
        }

        private static double DistanceDivided(double a, double dividend)
        {
            var adiv = a / dividend;
            return adiv * adiv;
        }
    }
}
