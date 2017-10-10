using System;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class HunterLabConverter
    {

        internal static void ToColorSpace(IRgb color, IHunterLab item)
        {
            var xyz = color.To<Xyz>();

            item.L = 10.0 * Math.Sqrt(xyz.Y);
            item.A = xyz.Y != 0 ? 17.5 * (((1.02 * xyz.X) - xyz.Y) / Math.Sqrt(xyz.Y)) : 0;
            item.B = xyz.Y != 0 ? 7.0 * ((xyz.Y - (.847 * xyz.Z)) / Math.Sqrt(xyz.Y)) : 0;
        }

        internal static IRgb ToColor(IHunterLab item)
        {
            var x = (item.A / 17.5) * (item.L / 10.0);
            var itemL_10 = item.L / 10.0;
            var y = itemL_10 * itemL_10;
            var z = item.B / 7.0 * item.L / 10.0;

            var xyz = new Xyz
                {
                    X = (x + y) / 1.02,
                    Y = y,
                    Z = -(z - y) / .847
                };
            return xyz.To<Rgb>();
        }
    }
}