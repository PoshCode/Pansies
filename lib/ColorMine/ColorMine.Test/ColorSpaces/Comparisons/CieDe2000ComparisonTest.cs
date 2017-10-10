using System;
using System.Collections.Generic;
using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using ColorMine.ColorSpaces.Conversions.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ColorMine.Test.ColorSpaces.Comparisons
{
    public class CieDe2000ComparisonTest
    {
        [TestClass]
        public class Compare
        {
            private IDictionary<Tuple<ILab, ILab>, double> TestData { get; set; }

            public string AssemblyDirectory
            {
                get
                {
                    string codeBase = GetType().GetTypeInfo().Assembly.CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    string path = Uri.UnescapeDataString(uri.Path);
                    return System.IO.Path.GetDirectoryName(path);
                }
            }

            [TestInitialize]
            public void Initialize()
            {
                TestData = new Dictionary<Tuple<ILab, ILab>, double>();
                var path = System.IO.Path.Combine(AssemblyDirectory, @"TestData\CieDe2000TestData.dat");
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read))
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {

                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (string.IsNullOrEmpty(line)) continue;
                            if (line.StartsWith("//")) continue;

                            var parts = line.Split('\t');
                            var a = new Lab
                            {
                                L = Double.Parse(parts[0]),
                                A = Double.Parse(parts[1]),
                                B = Double.Parse(parts[2])
                            };
                            var b = new Lab
                            {
                                L = Double.Parse(parts[3]),
                                A = Double.Parse(parts[4]),
                                B = Double.Parse(parts[5])
                            };
                            var input = new Tuple<ILab, ILab>(a, b);
                            var expected = Double.Parse(parts[6]);
                            TestData[input] = expected;
                        }
                    }
                }
            }

            private void ReturnsExpectedValueForKnownInput(double expectedValue, IColorSpace a, IColorSpace b)
            {
                var target = new CieDe2000Comparison();
                var actualValue = a.Compare(b, target);
                Assert.IsTrue(expectedValue.BasicallyEqualTo(actualValue), expectedValue + " != " + actualValue);
            }

            [TestMethod]
            public void ReturnsExpectedValueForKnownInputs()
            {
                foreach (var test in TestData)
                {
                    ReturnsExpectedValueForKnownInput(test.Value, test.Key.Item1, test.Key.Item2);
                }
            }
        }
    }
}
