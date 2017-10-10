using System;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class XyzConverter
    {
        #region Constants/Helper methods for Xyz related spaces
        internal static IXyz WhiteReference { get; private set; } // TODO: Hard-coded!
        internal const double Epsilon = 0.008856; // Intent is 216/24389
        internal const double Kappa = 903.3; // Intent is 24389/27
        static XyzConverter()
        {
            WhiteReference = new Xyz
            {
                X = 95.047,
                Y = 100.000,
                Z = 108.883
            };
        }

        internal static double CubicRoot(double n)
        {
            return Math.Pow(n, 1.0 / 3.0);
        }
        #endregion

        internal static void ToColorSpace(IRgb color, IXyz item)
        {
            var r = PivotRgb(color.R / 255.0);
            var g = PivotRgb(color.G / 255.0);
            var b = PivotRgb(color.B / 255.0);

            // Observer. = 2°, Illuminant = D65
            item.X = r * 0.4124 + g * 0.3576 + b * 0.1805;
            item.Y = r * 0.2126 + g * 0.7152 + b * 0.0722;
            item.Z = r * 0.0193 + g * 0.1192 + b * 0.9505;
        }

        internal static IRgb ToColor(IXyz item)
        {
            // (Observer = 2°, Illuminant = D65)
            var x = item.X / 100.0;
            var y = item.Y / 100.0;
            var z = item.Z / 100.0;

            var r = x * 3.2406 + y * -1.5372 + z * -0.4986;
            var g = x * -0.9689 + y * 1.8758 + z * 0.0415;
            var b = x * 0.0557 + y * -0.2040 + z * 1.0570;

            r = r > 0.0031308 ? 1.055 * Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
            g = g > 0.0031308 ? 1.055 * Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
            b = b > 0.0031308 ? 1.055 * Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

            return new Rgb
            {
                R = ToRgb(r),
                G = ToRgb(g),
                B = ToRgb(b)
            };
        }

        private static double ToRgb(double n)
        {
            var result = 255.0 * n;
            if (result < 0) return 0;
            if (result > 255) return 255;
            return result;
        }

        private static double PivotRgb(double n)
        {
            return (n > 0.04045 ? Math.Pow((n + 0.055) / 1.055, 2.4) : n / 12.92) * 100.0;
        }
    }
}