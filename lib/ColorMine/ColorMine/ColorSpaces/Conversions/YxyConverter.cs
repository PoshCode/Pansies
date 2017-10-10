using ColorMine.ColorSpaces.Conversions.Utility;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class YxyConverter
    {
        internal static void ToColorSpace(IRgb color, IYxy item)
        {
            var xyz = new Xyz();
            xyz.Initialize(color);

            item.Y1 = xyz.Y;

            var xDividend = xyz.X + xyz.Y + xyz.Z;
            item.X = xDividend.BasicallyEqualTo(0) ? 0.0 : xyz.X / xDividend;

            var y2Dividend = xyz.X + xyz.Y + xyz.Z;
            item.Y2 = y2Dividend.BasicallyEqualTo(0) ? 0.0 : xyz.Y / (xyz.X + xyz.Y + xyz.Z);
        }

        internal static IRgb ToColor(IYxy item)
        {
            var xyz = new Xyz
                {
                    X = item.X * (item.Y1 / item.Y2),
                    Y = item.Y1,
                    Z = (1.0 - item.X - item.Y2) * (item.Y1 / item.Y2)
                };
            return xyz.ToRgb();
        }
    }
}