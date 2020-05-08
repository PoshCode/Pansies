using PoshCode.Pansies.ColorSpaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoshCode.Pansies
{
    public class Gradient
    {
        public static T[][] Get2DGradient<T,WorkT>(T startColor, T endColor, int height = 1, int width = 120, bool reverseHue = false) where T : IColorSpace, new() where WorkT : IColorSpace, new()
        {
            var colors = new T[height][];

            //Simple pythagorean distance
            WorkT start = startColor.To<WorkT>();
            WorkT end = endColor.To<WorkT>();

            double size = Math.Sqrt((height - 1) * (height - 1) + (width - 1) * (width - 1));
            var sizeOrdinals = new double[start.Ordinals.Length];
            for (var i = 0; i < sizeOrdinals.Length; i++)
            {
                sizeOrdinals[i] = (end.Ordinals[i] - start.Ordinals[i]) / size;
            }
            var stepSize = new WorkT
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
            bool hued = start is IHue;
            if (hued)
            {
                var change = Math.Abs(((IHue)stepSize).H) > 180 / size;
                if (reverseHue)
                {
                    change = !change;
                }
                if (change)
                {
                    if (((IHue)stepSize).H > 0)
                    {
                        ((IHue)stepSize).H -= 360 / size;
                    }
                    else
                    {
                        ((IHue)stepSize).H += 360 / size;
                    }
                }
            }

            for (var line = 1; line <= height; line++)
            {
                colors[line - 1] = new T[width];

                for (var column = 1; column <= width; column++)
                {
                    var d = Math.Sqrt((line - 1) * (line - 1) + (column - 1) * (column - 1));

                    var stepOrdinals = new double[sizeOrdinals.Length];

                    for (var i = 0; i < sizeOrdinals.Length; i++)
                    {
                        stepOrdinals[i] = start.Ordinals[i] + stepSize.Ordinals[i] * d;
                    }
                    var stepColor = new WorkT
                    {
                        Ordinals = stepOrdinals
                    };
                    // For colors based on hue rotation, the math is slightly more complex:
                    if (hued)
                    {
                        if (((IHue)stepColor).H < 0)
                        {
                            ((IHue)stepColor).H += 360;
                        }
                        ((IHue)stepColor).H %= ceiling;
                    }

                    colors[line - 1][column - 1] = stepColor.To<T>();
                }
            }
            return colors;
        }

        public static RgbColor[][] GetRgbGradient(RgbColor startColor, RgbColor endColor, int height = 1, int width = 120, bool reverseHue = false)
        {
            return (RgbColor[][])Get2DGradient<RgbColor,HslColor>(startColor, endColor, height, width, reverseHue);
        }

        public static IEnumerable<T> GetGradient<T>(T start, T end, int size = 10, bool reverseHue = false) where T : IColorSpace, new()
        {
            var sizeOrdinals = new double[start.Ordinals.Length];
            for (var i = 0; i < sizeOrdinals.Length; i++)
            {
                sizeOrdinals[i] = (end.Ordinals[i] - start.Ordinals[i]) / size;
            }
            var stepSize = new T
            {
                Ordinals = sizeOrdinals
            };

            // For colors based on hue rotation, the math is slightly more complex:
            double ceiling = 360;
            bool hued = start is IHue;
            if (hued)
            {
                var change = Math.Abs(((IHue)stepSize).H) > 180 / size;
                if (reverseHue)
                {
                    change = !change;
                }
                if (change)
                {
                    if (((IHue)stepSize).H > 0)
                    {
                        ((IHue)stepSize).H -= 360 / size;
                    }
                    else
                    {
                        ((IHue)stepSize).H += 360 / size;
                    }
                }
            }


            for (var column = 1; column <= size; column++)
            {
                var stepOrdinals = new double[sizeOrdinals.Length];

                for (var i = 0; i < sizeOrdinals.Length; i++)
                {
                    stepOrdinals[i] = start.Ordinals[i] + stepSize.Ordinals[i] * column;
                }
                var stepColor = new T
                {
                    Ordinals = stepOrdinals
                };
                // For colors based on hue rotation, the math is slightly more complex:
                if (hued)
                {
                    if (((IHue)stepColor).H < 0)
                    {
                        ((IHue)stepColor).H += 360;
                    }
                    ((IHue)stepColor).H %= ceiling;
                }

                yield return stepColor;
            }
        }


        public static IEnumerable<T> GetRainbow<T>(T start, int size, int hueStep = 50, int lightStep = 3, int satStep = 0) where T : IColorSpace, new()
        {
            Hsl next = start.To<Hsl>();

            for (int i = 0; i < size; i++)
            {
                next = new Hsl { Ordinals = next.Ordinals };
                next.H += hueStep;
                next.H %= 360;

                if (next.S + satStep > 100) {
                    next.S = 20;
                } else {
                    next.S += satStep;
                }
                if (next.L + lightStep > 80) {
                    next.L = 20;
                } else {
                    next.L += lightStep;
                }

                yield return next.To<T>();
            }
        }
    }
}
