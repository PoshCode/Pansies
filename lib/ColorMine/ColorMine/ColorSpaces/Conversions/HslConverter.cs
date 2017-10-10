using ColorMine.ColorSpaces.Conversions.Utility;
using System;
using static System.Math;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class HslConverter
    {
        public static IHsl ToColorSpace(double R, double G, double B)
        {
            var min = Min(R, Min(G, B));    //Min. value of RGB
            var max = Max(R, Max(G, B));    //Max. value of RGB
            var chroma = max - min;

            double H = 0, S = 0, L = 0;
            L = (max + min) / 2;

            if (chroma == 0) // This is a gray, no chroma...
            {
                H = 0;
                S = 0;
            }
            else
            {
                if (L <= 0.5)
                {
                    S = chroma / (2 * L); //max / (max + min);
                }
                else
                {
                    S = chroma / (2 - 2 * L); // max / (2 - max - min);
                }

                if (R == max)
                {
                    H = (G - B) / chroma;
                    if (G < B)
                    {
                        H += 6;
                    }
                }
                else if (B == max)
                {
                    H = 4 + ((R - G) / chroma);
                }
                else if (G == max)
                {
                    H = 2 + ((B - R) / chroma);
                }


                    H *= 60;
            }
            return new Hsl(H, S, L);
        }

        internal static void ToColorSpace(IRgb color, IHsl item)
        {
            var result = ToColorSpace(color.R / 255d, color.G / 255d, color.B / 255d);
            item.H = result.H;
            item.S = result.S;
            item.L = result.L;

            // Range expected by HSL is integer
            item.S = Round(item.S * 100, 3);
            item.L = Round(item.L * 100, 3);
        }

        private static IRgb Rotate(double h, double s, ref double l)
        {
            // Avoid exactly zero hue, it does weird things
            if(h==0) { h = 0.00001; }
            var chroma = (1 - Abs(2 * l - 1)) * s;
            var x = chroma * (1 - Abs((h % 2d) - 1));
            l -= 0.5 * chroma;

            switch (Ceiling(h))
            {
                case 1d:
                    return new Rgb(chroma, x, 0);
                case 2d:
                    return new Rgb(x, chroma, 0);
                case 3d:
                    return new Rgb(0, chroma, x);
                case 4d:
                    return new Rgb(0, x, chroma);
                case 5d:
                    return new Rgb(x, 0, chroma);
                case 6d:
                    return new Rgb(chroma, 0, x);
                default:
                    return new Rgb(0, 0, 0);
            }
        }

        internal static IRgb ToColor(IHsl item)
        {
            var h = item.H / 60.0;
            var s = item.S / 100.0;
            var l = item.L / 100.0;
            if (s > 0)
            {
                var result = Rotate(h, s, ref l);

                return new Rgb
                {
                    R = (result.R + l) * 255,
                    G = (result.G + l) * 255,
                    B = (result.B + l) * 255
                };
            }
            else
            {
                return new Rgb
                {
                    R = l * 255,
                    G = l * 255,
                    B = l * 255
                };
            }
        }

        private static double GetColorComponent(double temp1, double temp2, double temp3)
        {
            temp3 = MoveIntoRange(temp3);
            if (temp3 < 1.0 / 6.0)
            {
                return temp1 + (temp2 - temp1) * 6.0 * temp3;
            }

            if (temp3 < 0.5)
            {
                return temp2;
            }

            if (temp3 < 2.0 / 3.0)
            {
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
            }

            return temp1;
        }

        private static double MoveIntoRange(double temp3)
        {
            if (temp3 < 0.0) return temp3 + 1.0;
            if (temp3 > 1.0) return temp3 - 1.0;
            return temp3;
        }
    }
}