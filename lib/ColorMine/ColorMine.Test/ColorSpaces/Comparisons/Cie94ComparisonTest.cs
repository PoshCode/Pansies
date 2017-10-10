using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using ColorMine.ColorSpaces.Conversions.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMine.Test.ColorSpaces.Comparisons
{
    public class Cie94ComparisonTest
    {
        public class ApplicationConstants
        {
            [TestClass]
            public class ApplicationConstantsConstructor
            {
                [TestMethod]
                public void GraphicArtConstantsCorrect()
                {
                    var constants = new Cie94Comparison.ApplicationConstants(Cie94Comparison.Application.GraphicArts);
                    Assert.AreEqual(1.0, constants.Kl);
                }

                [TestMethod]
                public void TextilesConstantsCorrect()
                {
                    var constants = new Cie94Comparison.ApplicationConstants(Cie94Comparison.Application.Textiles);
                    Assert.AreEqual(2.0, constants.Kl);
                }
            }
        }

        [TestClass]
        public class Cie94ComparisonConstructor
        {
            [TestMethod]
            public void DefaultsToGraphicArts()
            {
                var c = new Cie94Comparison();
                var constants = new Cie94Comparison.ApplicationConstants(Cie94Comparison.Application.GraphicArts);
                Assert.AreEqual(constants.Kl, c.Constants.Kl);
                Assert.AreEqual(constants.K1, c.Constants.K1);
                Assert.AreEqual(constants.K2, c.Constants.K2);
            }

            [TestMethod]
            public void TextilesApplication()
            {
                var c = new Cie94Comparison(Cie94Comparison.Application.Textiles);
                var constants = new Cie94Comparison.ApplicationConstants(Cie94Comparison.Application.Textiles);
                Assert.AreEqual(constants.Kl, c.Constants.Kl);
                Assert.AreEqual(constants.K1, c.Constants.K1);
                Assert.AreEqual(constants.K2, c.Constants.K2);
            }
        }

        [TestClass]
        public class Compare
        {
            private static void ReturnsExpectedValueForKnownInput(double expectedValue, Cie94Comparison.Application application, IColorSpace a, IColorSpace b)
            {
                var target = new Cie94Comparison(application);
                var actualValue = a.Compare(b, target);
                Assert.IsTrue(expectedValue.BasicallyEqualTo(actualValue), expectedValue + " != " + actualValue);
            }

            [TestMethod]
            public void ReturnsKnownValueForGraphicArtsPinks()
            {
                // Todo, should be mocking!!
                var a = new Lab
                    {
                        L = 70.1,
                        A = 53,
                        B = -3.2
                    };

                // Todo, should be mocking!!
                var b = new Lab
                {
                    L = 67.4,
                    A = 47.7,
                    B = -5.34
                };

                ReturnsExpectedValueForKnownInput(3.408967, Cie94Comparison.Application.GraphicArts, a, b);
            }
        }
    }
}
