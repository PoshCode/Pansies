using PoshCode.Pansies.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoshCode.Pansies
{
    public class Gradient
    {
        public static RgbColor[][] GetGradient<T>(RgbColor startColor, RgbColor endColor, int height = 1, int width = 120, bool reverseHue = false) where T : IColorSpace, new()
        {
            var colors = new RgbColor[height][];
            //Simple pythagorean distance
            double size = Math.Sqrt((height - 1) * (height - 1) + (width - 1) * (width - 1));
            T start = startColor.To<T>();
            T end = endColor.To<T>();

            var sizeOrdinals = new double[start.Ordinals.Length];
            for (var i = 0; i < sizeOrdinals.Length; i++)
            {
                sizeOrdinals[i] = (end.Ordinals[i] - start.Ordinals[i]) / size;
            }
            var stepSize = new T
            {
                Ordinals = sizeOrdinals
            };
            /*
            Write-Verbose "Size: $('{0:N2}' -f $Size) (width x height) ($($Colors.Length) x $($Colors[0].Length))"
            Write-Verbose "Diff: {$StepSize}"
            Write-Verbose "From: {$Left} $($StartColor.Ordinals)"
            Write-Verbose "To:   {$Right} $($EndColor.Ordinals)"
            */

            // For colors based on hue rotation, the math is slightly more complex:
            double ceiling = 360;
            var prop = stepSize.GetType().GetProperties().Where( p => p.Name == "H").FirstOrDefault();
            if (null != prop)
            {
                var H = (double)prop.GetValue(stepSize);
                var change = Math.Abs(H) > 180 / size;
                if (reverseHue)
                {
                    change = !change;
                }
                if (change) {
                    if (H > 0) {
                        H -= 360 / size;
                    } else {
                        H += 360 / size;
                    }
                    prop.SetValue(stepSize, H);
                }
            }

            for (var line = 1; line <= height; line++)
            {
                colors[line - 1] = new RgbColor[width];

                for (var column = 1; column <= width; column++)
                {
                    var d = Math.Sqrt((line - 1) * (line - 1) + (column - 1) * (column - 1));

                    var stepOrdinals = new double[sizeOrdinals.Length];

                    for (var i = 0; i < sizeOrdinals.Length; i++)
                    {
                        stepOrdinals[i] = start.Ordinals[i] + stepSize.Ordinals[i] * d;
                    }
                    var stepColor = new T
                    {
                        Ordinals = stepOrdinals
                    };
                    // For colors based on hue rotation, the math is slightly more complex:
                    if (null != prop)
                    {
                        var H = (double)prop.GetValue(stepColor);
                        if (H < 0)
                        {
                            H += 360;
                        }
                        H %= ceiling;
                        prop.SetValue(stepSize, H);
                    }

                    colors[line - 1][column - 1] = stepColor.To<RgbColor>();
                }
            }
            return colors;
        }
    }
}
