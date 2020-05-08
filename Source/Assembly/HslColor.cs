using PoshCode.Pansies.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoshCode.Pansies
{
    public enum Harmony
    {
        Analogous,
        Complementary,
        Split,
        Triad,
    }
    public partial class HslColor : Hsl
    {
        private static Random random = new Random();

        public IEnumerable<HslColor> GetHarmony(Harmony harmony)
        {
            switch (harmony)
            {
                case Harmony.Analogous:
                    //Analogous: Choose second and third ranges 0.
                    return GetHarmony(3, 30, 30, 30, 0, 0);
                case Harmony.Complementary:
                    //Complementary: Choose the third range 0, and first offset angle 180.
                    return GetHarmony(2, 180, 0, 0, 1, 0);
                case Harmony.Split:
                    //Split Complementary: Choose offset angles 180 +/ -a small angle. The second and third ranges must be smaller than the difference between the two offset angles.
                    return GetHarmony(3, 160, 200, 30, 2, 20);
                case Harmony.Triad:
                    //Triad: Choose offset angles 120 and 240.
                    return GetHarmony(3, 120, 240, 30, 2, 20);
                default:
                    return GetHarmony(6, random.NextDouble() * 180, random.NextDouble() * 180, random.NextDouble() * 60, random.NextDouble() * 60, random.NextDouble() * 60);
            }


        }

        public IEnumerable<HslColor> GetHarmony(int colorCount, double offsetAngle1, double offsetAngle2, double rangeAngle0, double rangeAngle1, double rangeAngle2)
        {
            var distance = 360 / colorCount;
            offsetAngle1 %= distance;
            offsetAngle2 %= distance;
            var minimum = distance - offsetAngle1;
            var maximum = distance + offsetAngle2;
            double newAngle = (random.NextDouble() * (maximum - minimum)) + minimum;

            var next = this;

            for (int i = 0; i < colorCount; i++)
            {
                yield return next;

                next = new HslColor
                {
                    Ordinals = next.Ordinals
                };


                if (next.H > (this.H + distance) % 360 )
                {
                    newAngle *= -1;
                }

                next.H = (H  + newAngle + 360) % 360.0;
            }
        }
    }
}
