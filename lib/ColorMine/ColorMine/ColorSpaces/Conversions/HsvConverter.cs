using System;
using static System.Math;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class HsvConverter
    {
        public static IHsv ToColorSpace(double R, double G, double B)
        {
            var min = Min(R, Min(G, B));    //Min. value of RGB
            var max = Max(R, Max(G, B));    //Max. value of RGB
            var chroma = max - min;

            double H = 0, S = 0, V = 0;
            V = max;

            if (chroma == 0) // This is a gray, no chroma...
            {
                H = 0;
                S = 0;
            }
            else
            {
                S = chroma / max;

                if (R == max)
                {
                    H = (G - B) / chroma;
                    if (G < B)
                    {
                        H += 6;
                    }
                }
                else if (G == max)
                {
                    H = 2 + ((B - R) / chroma);
                }
                else if (B == max)
                {
                    H = 4 + ((R - G) / chroma);
                }

                H *= 60;
            }
            return new Hsv(H, S, V);
        }

        internal static void ToColorSpace(IRgb color, IHsv item)
        {
            var result = ToColorSpace(color.R / 255d, color.G / 255d, color.B / 255d);
            item.H = result.H;
            item.S = result.S;
            item.V = result.V;
                        
            //item.H = Color.FromArgb(255, (int)color.R, (int)color.G, (int)color.B).GetHue();
            //item.S = (max <= 0) ? 0 : 1d - (1d * min / max);
            //item.V = max / 255d;
        }

        internal static IRgb ToColor(IHsv item)
        {
            var range = Convert.ToInt32(Math.Floor(item.H / 60.0)) % 6;
            var f = item.H / 60.0 - Math.Floor(item.H / 60.0);

            var v = item.V * 255.0;
            var p = v * (1 - item.S);
            var q = v * (1 - f * item.S);
            var t = v * (1 - (1 - f) * item.S);

            switch (range)
            {
                case 0:
                    return NewRgb(v, t, p);
                case 1:
                    return NewRgb(q, v, p);
                case 2:
                    return NewRgb(p, v, t);
                case 3:
                    return NewRgb(p, q, v);
                case 4:
                    return NewRgb(t, p, v);
            }
            return NewRgb(v, p, q);
        }

        private static IRgb NewRgb(double r, double g, double b)
        {
            return new Rgb { R = r, G = g, B = b };
        }
    }
}