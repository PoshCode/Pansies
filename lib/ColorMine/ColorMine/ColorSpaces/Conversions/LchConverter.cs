using System;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class LchConverter
    {
        internal static void ToColorSpace(IRgb color, ILch item)
        {
            var lab = color.To<Lab>();
            var h = Math.Atan2(lab.B, lab.A);

            // convert from radians to degrees
            if (h > 0)
            {
                h = (h / Math.PI) * 180.0;
            }
            else
            {
                h = 360 - (Math.Abs(h) / Math.PI) * 180.0;
            }

            if (h < 0)
            {
                h += 360.0;
            }
            else if (h >= 360)
            {
                h -= 360.0;
            }

            item.L = lab.L;
            item.C = Math.Sqrt(lab.A * lab.A + lab.B * lab.B);
            item.H = h;
        }

        internal static IRgb ToColor(ILch item)
        {
            var hRadians = item.H * Math.PI / 180.0;
            var lab = new Lab
                {
                    L = item.L,
                    A = Math.Cos(hRadians) * item.C,
                    B = Math.Sin(hRadians) * item.C
                };
            return lab.To<Rgb>();
        }
    }
}