using System;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class LuvConverter
    {
        internal static void ToColorSpace(IRgb color, ILuv item)
        {
            var xyz = new Xyz();
            var white = XyzConverter.WhiteReference;
            xyz.Initialize(color);

            var y = xyz.Y / XyzConverter.WhiteReference.Y;
            item.L = y > XyzConverter.Epsilon ? 116.0 * XyzConverter.CubicRoot(y) - 16.0 : XyzConverter.Kappa * y;

            var targetDenominator = GetDenominator(xyz);
            var referenceDenominator = GetDenominator(white);
            // ReSharper disable CompareOfFloatsByEqualityOperator
            var xTarget = targetDenominator == 0 ? 0 : ((4.0 * xyz.X / targetDenominator) - (4.0 * white.X / referenceDenominator));
            var yTarget = targetDenominator == 0 ? 0 : ((9.0 * xyz.Y / targetDenominator) - (9.0 * white.Y / referenceDenominator));
            // ReSharper restore CompareOfFloatsByEqualityOperator

            item.U = 13.0 * item.L * xTarget;
            item.V = 13.0 * item.L * yTarget;
        }

        internal static IRgb ToColor(ILuv item)
        {
            var white = XyzConverter.WhiteReference;
            const double c = -1.0 / 3.0;
            var uPrime = (4.0 * white.X) / GetDenominator(white);
            var vPrime = (9.0 * white.Y) / GetDenominator(white);
            var a = (1.0 / 3.0) * ((52.0 * item.L) / (item.U + 13 * item.L * uPrime) - 1.0);
            var imteL_16_116 = (item.L + 16.0) / 116.0;
            var y = item.L > XyzConverter.Kappa * XyzConverter.Epsilon
                        ? imteL_16_116 * imteL_16_116 * imteL_16_116
                        : item.L / XyzConverter.Kappa;
            var b = -5.0 * y;
            var d = y * ((39.0 * item.L) / (item.V + 13.0 * item.L * vPrime) - 5.0);
            var x = (d - b) / (a - c);
            var z = x * a + b;
            var xyz = new Xyz
                {
                    X = 100 * x,
                    Y = 100 * y,
                    Z = 100 * z
                };
            return xyz.ToRgb();

        }

        private static double GetDenominator(IXyz xyz)
        {
            return xyz.X + 15.0 * xyz.Y + 3.0 * xyz.Z;
        }
    }
}