using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using ColorMine.ColorSpaces.Conversions.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMine.Test.ColorSpaces.Comparisons
{
    public class Cie1976ComparisonTest
    {
        [TestClass]
        public class Compare
        {
            private void ReturnsExpectedValueForKnownInput(double expectedValue, IColorSpace a, IColorSpace b)
            {
                var target = new Cie1976Comparison();
                var actualValue = a.Compare(b, target);
                Assert.IsTrue(expectedValue.BasicallyEqualTo(actualValue));
            }

            [TestMethod]
            public void ReturnsZeroForSameColors()
            {
                var compareColor = new Rgb {R = 140, G = 130, B = 23};
                // Todo, should be mocking!!
                var a = new Rgb();
                a.Initialize(compareColor);

                var b = new Rgb();
                b.Initialize(compareColor);

                ReturnsExpectedValueForKnownInput(0.0, a, b);
            }

            [TestMethod]
            public void ReturnsKnownValueForDissimilarColors()
            {
                // Todo, should be mocking!!
                var a = new Lab
                    {
                        L = 50,
                        A = 67,
                        B = 88
                    };

                var b = new Lab
                    {
                        L = 50,
                        A = 15,
                        B = 22
                    };

                ReturnsExpectedValueForKnownInput(84.0238, a, b);
            }

            [TestMethod]
            public void ReturnsKnownValueForSimilarColors()
            {
                // Todo, should be mocking!!
                var a = new Lab
                {
                    L = 88.17,
                    A = 67,
                    B = 88
                };

                var b = new Lab
                {
                    L = 87.16,
                    A = 65,
                    B = 66
                };

                ReturnsExpectedValueForKnownInput(22.1138, a, b);
            }
        }
    }
}
