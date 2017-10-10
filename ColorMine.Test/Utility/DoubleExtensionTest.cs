using ColorMine.ColorSpaces.Conversions.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMine.Test.Utility
{
    public class DoubleExtensionTest
    {
        [TestClass]
        public class BasicallyEqualTo
        {
            [TestMethod]
            public void ReturnsTrueForCloseNumbers()
            {
                Assert.IsTrue(.333.BasicallyEqualTo(1.0/3,.01));
            }

            [TestMethod]
            public void ReturnsFalseForFarNumbers()
            {
                Assert.IsFalse(.9.BasicallyEqualTo(.1,.001));
            }

            [TestMethod]
            public void ReturnsTrueForCloseNumbersWithDefaultPrecision()
            {
                Assert.IsTrue(.33333.BasicallyEqualTo(1.0 / 3));
            }

            [TestMethod]
            public void ReturnsFalseForFarNumbersWithDefaultPrecision()
            {
                Assert.IsFalse(.9.BasicallyEqualTo(.1));
            }
        }
    }
}