using ColorMine.ColorSpaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMine.Test.ColorSpaces
{
    [TestClass]
    public class RoundTripTests
    {
        [TestMethod]
        public void ConsoleColors()
        {
            var Colors = new[] {
                new Rgb { R = 0x00, G = 0x00, B = 0x00 },
                new Rgb { R = 0x80, G = 0x00, B = 0x00 },
                new Rgb { R = 0x00, G = 0x80, B = 0x00 },
                new Rgb { R = 0x80, G = 0x80, B = 0x00 },
                new Rgb { R = 0x00, G = 0x00, B = 0x80 },
                new Rgb { R = 0x80, G = 0x00, B = 0x80 },
                new Rgb { R = 0x00, G = 0x80, B = 0x80 },
                new Rgb { R = 0xc0, G = 0xc0, B = 0xc0 },
                new Rgb { R = 0x80, G = 0x80, B = 0x80 },
                new Rgb { R = 0xff, G = 0x00, B = 0x00 },
                new Rgb { R = 0x00, G = 0xff, B = 0x00 },
                new Rgb { R = 0xff, G = 0xff, B = 0x00 },
                new Rgb { R = 0x00, G = 0x00, B = 0xff },
                new Rgb { R = 0xff, G = 0x00, B = 0xff },
                new Rgb { R = 0x00, G = 0xff, B = 0xff },
                new Rgb { R = 0xff, G = 0xff, B = 0xff }
            };

            foreach (var color in Colors)
            {
                ExpectedValuesForRoundTrip<Hsl>(color);
            }
        }
        protected static void ExpectedValuesForRoundTrip<T>(Rgb knownColor) where T : IColorSpace, new()
        {
            var midColor = knownColor.To<T>();
            var target = midColor.To<Rgb>();

            Assert.AreEqual(knownColor.R, target.R, 1.2, knownColor.ToString() + " => " + midColor.ToString() + " => " + target.ToString() + " R: " + knownColor.R + " != " + target.R);
            Assert.AreEqual(knownColor.G, target.G, 1.2, knownColor.ToString() + " => " + midColor.ToString() + " => " + target.ToString() + " G: " + knownColor.G + " != " + target.G);
            Assert.AreEqual(knownColor.B, target.B, 1.2, knownColor.ToString() + " => " + midColor.ToString() + " => " + target.ToString() + " B: " + knownColor.B + " != " + target.B);
        }
    }
}
