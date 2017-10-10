using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using ColorMine.ColorSpaces.Conversions.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMine.Test.ColorSpaces.Comparisons
{
    public class CmcComparisonTest
    {
        [TestClass]
        public class Compare
        {
            private void ReturnsExpectedValueForKnownInput(double expectedValue, double ratio, IColorSpace a, IColorSpace b)
            {
                var target = new CmcComparison(ratio);
                var actualValue = a.Compare(b, target);
                Assert.IsTrue(expectedValue.BasicallyEqualTo(actualValue),expectedValue + " != " + actualValue);
            }

            [TestMethod]
            public void ReturnsKnownValueForRedAndMaroon2()
            {
                // Todo, should be mocking!!
                var a = new Lab
                {
                    L = 24.8290,
                    A = 60.0930,
                    B = 38.1800
                };

                var b = new Lab
                {
                    L = 53.2300,
                    A = 80.1090,
                    B = 67.2200
                };

                ReturnsExpectedValueForKnownInput(23.9165, 2, a, b);
            }


            [TestMethod]
            public void ReturnsKnownValueForRedAndMaroon1()
            {
                // Todo, should be mocking!!
                var a = new Lab
                {
                    L = 24.8290,
                    A = 60.0930,
                    B = 38.1800
                };

                var b = new Lab
                {
                    L = 53.2300,
                    A = 80.1090,
                    B = 67.2200
                };

                ReturnsExpectedValueForKnownInput(42.202017, 1, a, b);
            }

            [TestMethod]
            public void ReturnsKnownValueForWhiteAndEggShell2()
            {
                // Todo, should be mocking!!
                var a = new Lab
                {
                    L = 100,
                    A = 0,
                    B = 0
                };

                var b = new Lab
                {
                    L = 89.9490,
                    A = 13.8320,
                    B = 6.8160
                };

                ReturnsExpectedValueForKnownInput(24.406318, 2, a, b);
            }

            [TestMethod]
            public void ReturnsKnownValueForWhiteAndEggShell1()
            {
                // Todo, should be mocking!!
                var a = new Lab
                {
                    L = 100,
                    A = 0,
                    B = 0
                };

                var b = new Lab
                {
                    L = 89.9490,
                    A = 13.8320,
                    B = 6.8160
                };

                ReturnsExpectedValueForKnownInput(25.103175, 1, a, b);
            }
        }
    }
}
