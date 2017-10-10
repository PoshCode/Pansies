//Note: This is a generated file.
using ColorMine.ColorSpaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorMine.Test.ColorSpaces
{

	public class RgbTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void WhiteRgbToCmy()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Cmy { C = 0, M = 0, Y = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToCmyk()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Cmyk { C = 0, M = 0, Y = 0, K = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToHsl()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Hsl { H = 0, S = 0, L = 100, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToLab()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Lab { L = 100, A = 0.00526, B = -.0104, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToLch()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Lch { L = 100, C = .01166, H = 296.813, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToRgb()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Rgb { R = 255, G = 255, B = 255, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToXyz()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Xyz { X = 95.050, Y = 100, Z = 108.900, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToYxy()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Yxy { Y1 = 100, X = .31272, Y2 = .32900, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToLuv()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Luv { L = 100, U = .00089, V = -.017, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToHsv()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Hsv { H = 0, S = 0, V = 1, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToHsb()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new Hsb { H = 0, S = 0, B = 1, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void WhiteRgbToHunterLab()
            {
				var knownColor = new Rgb { R = 255, G = 255, B = 255, };
				var expectedColor = new HunterLab { L = 100, A = -5.336, B = 5.433, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToCmy()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Cmy { C = 1, M = 1, Y = 1, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToCmyk()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Cmyk { C = 0, M = 0, Y = 0, K = 1, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToHsl()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Hsl { H = 0, S = 0, L = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToLab()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Lab { L = 0, A = 0, B = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToLch()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Lch { L = 0, C = 0, H = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToRgb()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Rgb { R = 0, G = 0, B = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToXyz()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Xyz { X = 0, Y = 0, Z = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToYxy()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Yxy { Y1 = 0, X = 0, Y2 = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToLuv()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Luv { L = 0, U = 0, V = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToHsv()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Hsv { H = 0, S = 0, V = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToHsb()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new Hsb { H = 0, S = 0, B = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BlackRgbToHunterLab()
            {
				var knownColor = new Rgb { R = 0, G = 0, B = 0, };
				var expectedColor = new HunterLab { L = 0, A = 0, B = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToCmy()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Cmy { C = .14510, M = .35294, Y = .87451, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToCmyk()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Cmyk { C = 0, M = .24312, Y = .85321, K = .14510, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToHsl()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Hsl { H = 43, S = 74, L = 49, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToLab()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Lab { L = 70.816, A = 8.525, B = 68.765, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToLch()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Lch { L = 70.816, C = 69.291, H = 82.933, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToRgb()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Rgb { R = 218, G = 165, B = 32, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToXyz()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Xyz { X = 42.629, Y = 41.920, Z = 7.211, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToYxy()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Yxy { Y1 = 41.920, X = .46457, Y2 = .45684, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToLuv()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Luv { L = 70.816, U = 44.368, V = 69.994, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToHsv()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Hsv { H = 42.903, S = .85, V = .85, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToHsb()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new Hsb { H = 42.903, S = .85, B = .85, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void GoldenrodRgbToHunterLab()
            {
				var knownColor = new Rgb { R = 218, G = 165, B = 32, };
				var expectedColor = new HunterLab { L = 64.746, A = 4.222, B = 38.719, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class CmyTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void SteelBlueCmyToCmy()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Cmy { C = .72549, M = .49020, Y = .29412, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToCmyk()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Cmyk { C = .61111, M = .27778, Y = 0, K = .29412, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToHsl()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Hsl { H = 207, S = 44, L = 49, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToLab()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Lab { L = 52.467, A = -4.070, B = -32.198, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToLch()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Lch { L = 52.467, C = 32.454, H = 262.796, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToRgb()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Rgb { R = 70, G = 130, B = 180, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToXyz()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Xyz { X = 18.746, Y = 20.562, Z = 46.161, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToYxy()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Yxy { Y1 = 20.562, X = .21934, Y2 = .24058, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SteelBlueCmyToLuv()
            {
				var knownColor = new Cmy { C = .72549, M = .49020, Y = .29412, };
				var expectedColor = new Luv { L = 52.467, U = -25.107, V = -48.374, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class CmykTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void DarkVioletCmykToCmy()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Cmy { C = .41961, M = 1, Y = .17255, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToCmyk()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToHsl()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Hsl { H = 282, S = 100, L = 41, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToLab()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Lab { L = 39.579, A = 76.336, B = -70.378, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToLch()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Lch { L = 39.579, C = 103.828, H = 317.325, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToRgb()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Rgb { R = 148, G = 0, B = 211, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToXyz()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Xyz { X = 23.970, Y = 10.999, Z = 62.487, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void DarkVioletCmykToYxy()
            {
				var knownColor = new Cmyk { C = .29858, M = 1, Y = 0, K = .17255, };
				var expectedColor = new Yxy { Y1 = 10.99, X = .24596, Y2 = .11286, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class HslTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void AliceBlueHslToCmy()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Cmy { C = .06, M = .028, Y = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToCmyk()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Cmyk { C = .06, M = .028, Y = 0, K = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToHsl()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Hsl { H = 208, S = 100, L = 97, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToLab()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Lab { L = 97.179, A = -1.3688, B = -4.358, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToLch()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Lch { L = 97.179, C = 4.5683, H = 252.551, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToRgb()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Rgb { R = 240, G = 248, B = 255, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToXyz()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Xyz { X = 87.553, Y = 92.880, Z = 107.921, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AliceBlueHslToYxy()
            {
				var knownColor = new Hsl { H = 208, S = 100, L = 97, };
				var expectedColor = new Yxy { Y1 = 92.880, X = .30363, Y2 = .32210, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class LabTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void RedLabToCmy()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Cmy { C = 0.0000035, M = .99970, Y = 1, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToCmyk()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Cmyk { C = 0, M = .99970, Y = 1, K = .0000035, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToHsl()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Hsl { H = 0, S = 100, L = 50, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToLab()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToLch()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Lch { L = 53.33, C = 104.575, H = 40, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToRgb()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Rgb { R = 255, G = .076, B = .0017, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToXyz()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Xyz { X = 41.240, Y = 21.260, Z = 1.930, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RedLabToYxy()
            {
				var knownColor = new Lab { L = 53.233, A = 80.109, B = 67.220, };
				var expectedColor = new Yxy { Y1 = 21.260, X = 0.64007, Y2 = .32997, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class LchTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void MaroonLchToCmy()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Cmy { C = .509804, M = .99986, Y = 1, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToCmyk()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Cmyk { C = .0, M = .99972, Y = 1, K = .50980, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToHsl()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Hsl { H = 0, S = 100, L = 24.5, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToLab()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Lab { L = 24.829, A = 47.237, B = 37.146, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToLch()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToRgb()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Rgb { R = 125, G = .03555, B = .00182, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToXyz()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Xyz { X = 8.458, Y = 4.360, Z = .396, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void MaroonLchToYxy()
            {
				var knownColor = new Lch { L = 24.829, C = 60.093, H = 38.180, };
				var expectedColor = new Yxy { Y1 = 4.360, X = 0.64005, Y2 = .32998, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class XyzTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void RivergumXyzToCmy()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Cmy { C = .614, M = .55494, Y = .62627, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToCmyk()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Cmyk { C = .1327, M = 0, Y = .16028, K = .55494, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToHsl()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Hsl { H = 109.999, S = 8.654, L = 40.7843, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToLab()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Lab { L = 46.140, A = -9.421, B = 8.218, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToLch()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Lch { L = 46.140, C = 12.502, H = 138.898, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToRgb()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Rgb { R = 98.43, G = 113.49, B = 95.30, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToXyz()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void RivergumXyzToYxy()
            {
				var knownColor = new Xyz { X = 13.123, Y = 15.372, Z = 13.174, };
				var expectedColor = new Yxy { Y1 = 15.372, X = .31493, Y2 = .36892, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class LuvTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void SilverLuvToCmy()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Cmy { C = .24706, M = .24704, Y = .24706, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToCmyk()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Cmyk { C = .0000148, M = .000, Y = .0000186, K = .24704, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToHsl()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Hsl { H = 108, S = 0.02, L = 75.098, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToLab()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Lab { L = 77.704, A = .0026, B = -.00696, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToLch()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Lch { L = 77.704, C = .0074, H = 290.49463, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToRgb()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Rgb { R = 192, G = 192, B = 192, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToXyz()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Xyz { X = 50.102, Y = 52.711, Z = 57.402, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToYxy()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Yxy { Y1 = 52.711, X = .31272, Y2 = .32900, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void SilverLuvToLuv()
            {
				var knownColor = new Luv { L = 77.704, U = .001, V = -.013, };
				var expectedColor = new Luv { L = 77.704, U = .001, V = -.013, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class HsvTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void AquamarineHsvToCmy()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Cmy { C = .50196, M = 0, Y = .16666, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToCmyk()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Cmyk { C = .5, M = 0, Y = .16666, K = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToHsl()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Hsl { H = 160, S = 100, L = 74.9019, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToLab()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Lab { L = 92.036, A = -45.521, B = 9.49689, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToLch()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Lch { L = 92.036, C = 46.545, H = 167.957, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToRgb()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Rgb { R = 127, G = 255, B = 212, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToXyz()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Xyz { X = 56.396, Y = 80.785, Z = 74.908, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToYxy()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Yxy { Y1 = 80.785, X = .26591, Y2 = .38090, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsvToLuv()
            {
				var knownColor = new Hsv { H = 160, S = .5, V = 1, };
				var expectedColor = new Luv { L = 92.036, U = -55.917, V = 21.99756, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class HsbTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void AquamarineHsbToCmy()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Cmy { C = .50196, M = 0, Y = .16666, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToCmyk()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Cmyk { C = .5, M = 0, Y = .16666, K = 0, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToHsl()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Hsl { H = 160, S = 100, L = 74.9019, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToLab()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Lab { L = 92.036, A = -45.521, B = 9.49689, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToLch()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Lch { L = 92.036, C = 46.545, H = 167.957, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToRgb()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Rgb { R = 127, G = 255, B = 212, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToXyz()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Xyz { X = 56.396, Y = 80.785, Z = 74.908, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToYxy()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Yxy { Y1 = 80.785, X = .26591, Y2 = .38090, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void AquamarineHsbToLuv()
            {
				var knownColor = new Hsb { H = 160, S = .5, B = 1, };
				var expectedColor = new Luv { L = 92.036, U = -55.917, V = 21.99756, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
	public class HunterLabTest
    {
		[TestClass]
        public class To : ColorSpaceTest
        {
			
			[TestMethod]
            public void BabyBlueHunterLabToRgb()
            {
				var knownColor = new HunterLab { L = 85.308, A = -27.915, B = -13.675, };
				var expectedColor = new Rgb { R = 133, G = 237, B = 255, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }
			
			[TestMethod]
            public void BabyBlueHunterLabToXyz()
            {
				var knownColor = new HunterLab { L = 85.308, A = -27.915, B = -13.675, };
				var expectedColor = new Xyz { X = 58.007, Y = 72.775, Z = 105.596, };

                ExpectedValuesForKnownColor(knownColor,expectedColor);
            }

        }
	}
}