using System;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class LabConverter
    {
        internal static void ToColorSpace(IRgb color, ILab item)
        {
            var xyz = new Xyz();
            xyz.Initialize(color);

            var white = XyzConverter.WhiteReference;
            var x = PivotXyz(xyz.X / white.X);
            var y = PivotXyz(xyz.Y / white.Y);
            var z = PivotXyz(xyz.Z / white.Z);

            item.L = Math.Max(0, 116 * y - 16);
            item.A = 500 * (x - y);
            item.B = 200 * (y - z);
        }

        internal static IRgb ToColor(ILab item)
        {
            var y = (item.L + 16.0) / 116.0;
            var x = item.A / 500.0 + y;
            var z = y - item.B / 200.0;

            var white = XyzConverter.WhiteReference;
            var x3 = x * x * x;
            var z3 = z * z * z;
            var xyz = new Xyz
                {
                    X = white.X * (x3 > XyzConverter.Epsilon ? x3 : (x - 16.0 / 116.0) / 7.787),
                    Y = white.Y * (item.L > (XyzConverter.Kappa * XyzConverter.Epsilon) ? Math.Pow(((item.L + 16.0) / 116.0), 3) : item.L / XyzConverter.Kappa),
                    Z = white.Z * (z3 > XyzConverter.Epsilon ? z3 : (z - 16.0 / 116.0) / 7.787)
                };

            return xyz.ToRgb();
        }

        private static double PivotXyz(double n)
        {
            return n > XyzConverter.Epsilon ? CubicRoot(n) : (XyzConverter.Kappa * n + 16) / 116;
        }

        private static double CubicRoot(double n)
        {
            return Math.Pow(n, 1.0 / 3.0);
        }
    }
}