using System;
using ColorMine.ColorSpaces.Conversions.Utility;

namespace ColorMine.ColorSpaces.Conversions
{
    public static class CmykConverter
    {
        public static void ToColorSpace(IRgb color, ICmyk item)
        {
            var cmy = new Cmy();
            cmy.Initialize(color);

            var k = 1.0;
            if (cmy.C < k)
                k = cmy.C;
            if (cmy.M < k)
                k = cmy.M;
            if (cmy.Y < k)
                k = cmy.Y;
            item.K = k;

            if (k.BasicallyEqualTo(1))
            {
                item.C = 0;
                item.M = 0;
                item.Y = 0;
            }
            else
            {
                item.C = (cmy.C - k) / (1 - k);
                item.M = (cmy.M - k) / (1 - k);
                item.Y = (cmy.Y - k) / (1 - k);
            }
        }

        public static IRgb ToColor(ICmyk item)
        {
            var cmy = new Cmy
                {
                    C = (item.C * (1 - item.K) + item.K),
                    M = (item.M * (1 - item.K) + item.K),
                    Y = (item.Y * (1 - item.K) + item.K)
                };

            return cmy.ToRgb();
        }
    }
}