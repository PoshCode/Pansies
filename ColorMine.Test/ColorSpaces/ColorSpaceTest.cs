//Note: This is a generated file.
using ColorMine.ColorSpaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ColorMine.Test.ColorSpaces
{
	public abstract class ColorSpaceTest
    {
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, ICmy expectedColor)
		{
			var target = knownColor.To<Cmy>();
			Assert.AreEqual(expectedColor.C, target.C, 0.005, "(C)" + expectedColor.C + " != " + target.C);
			Assert.AreEqual(expectedColor.M, target.M, 0.005, "(M)" + expectedColor.M + " != " + target.M);
			Assert.AreEqual(expectedColor.Y, target.Y, 0.005, "(Y)" + expectedColor.Y + " != " + target.Y);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, ICmyk expectedColor)
		{
			var target = knownColor.To<Cmyk>();
			Assert.AreEqual(expectedColor.C, target.C, 0.005, "(C)" + expectedColor.C + " != " + target.C);
			Assert.AreEqual(expectedColor.M, target.M, 0.005, "(M)" + expectedColor.M + " != " + target.M);
			Assert.AreEqual(expectedColor.Y, target.Y, 0.005, "(Y)" + expectedColor.Y + " != " + target.Y);
			Assert.AreEqual(expectedColor.K, target.K, 0.005, "(K)" + expectedColor.K + " != " + target.K);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IHsl expectedColor)
		{
			var target = knownColor.To<Hsl>();
			Assert.AreEqual(expectedColor.H, target.H, 1.8, "(H)" + expectedColor.H + " != " + target.H);
			Assert.AreEqual(expectedColor.S, target.S, 0.5, "(S)" + expectedColor.S + " != " + target.S);
			Assert.AreEqual(expectedColor.L, target.L, 0.5, "(L)" + expectedColor.L + " != " + target.L);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, ILab expectedColor)
		{
			var target = knownColor.To<Lab>();
			Assert.AreEqual(expectedColor.L, target.L, 0.5, "(L)" + expectedColor.L + " != " + target.L);
			Assert.AreEqual(expectedColor.A, target.A, 0.64, "(A)" + expectedColor.A + " != " + target.A);
			Assert.AreEqual(expectedColor.B, target.B, 0.64, "(B)" + expectedColor.B + " != " + target.B);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, ILch expectedColor)
		{
			var target = knownColor.To<Lch>();
			Assert.AreEqual(expectedColor.L, target.L, 0.5, "(L)" + expectedColor.L + " != " + target.L);
			Assert.AreEqual(expectedColor.C, target.C, 0.5, "(C)" + expectedColor.C + " != " + target.C);
			Assert.AreEqual(expectedColor.H, target.H, 1.8, "(H)" + expectedColor.H + " != " + target.H);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IRgb expectedColor)
		{
			var target = knownColor.To<Rgb>();
			Assert.AreEqual(expectedColor.R, target.R, 1.275, "(R)" + expectedColor.R + " != " + target.R);
			Assert.AreEqual(expectedColor.G, target.G, 1.275, "(G)" + expectedColor.G + " != " + target.G);
			Assert.AreEqual(expectedColor.B, target.B, 1.275, "(B)" + expectedColor.B + " != " + target.B);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IXyz expectedColor)
		{
			var target = knownColor.To<Xyz>();
			Assert.AreEqual(expectedColor.X, target.X, 0.5, "(X)" + expectedColor.X + " != " + target.X);
			Assert.AreEqual(expectedColor.Y, target.Y, 0.5, "(Y)" + expectedColor.Y + " != " + target.Y);
			Assert.AreEqual(expectedColor.Z, target.Z, 0.5, "(Z)" + expectedColor.Z + " != " + target.Z);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IYxy expectedColor)
		{
			var target = knownColor.To<Yxy>();
			Assert.AreEqual(expectedColor.Y1, target.Y1, 0.5, "(Y1)" + expectedColor.Y1 + " != " + target.Y1);
			Assert.AreEqual(expectedColor.X, target.X, 0.005, "(X)" + expectedColor.X + " != " + target.X);
			Assert.AreEqual(expectedColor.Y2, target.Y2, 0.005, "(Y2)" + expectedColor.Y2 + " != " + target.Y2);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, ILuv expectedColor)
		{
			var target = knownColor.To<Luv>();
			Assert.AreEqual(expectedColor.L, target.L, 0.5, "(L)" + expectedColor.L + " != " + target.L);
			Assert.AreEqual(expectedColor.U, target.U, 1.12, "(U)" + expectedColor.U + " != " + target.U);
			Assert.AreEqual(expectedColor.V, target.V, 0.61, "(V)" + expectedColor.V + " != " + target.V);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IHsv expectedColor)
		{
			var target = knownColor.To<Hsv>();
			Assert.AreEqual(expectedColor.H, target.H, 1.8, "(H)" + expectedColor.H + " != " + target.H);
			Assert.AreEqual(expectedColor.S, target.S, 0.005, "(S)" + expectedColor.S + " != " + target.S);
			Assert.AreEqual(expectedColor.V, target.V, 0.005, "(V)" + expectedColor.V + " != " + target.V);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IHsb expectedColor)
		{
			var target = knownColor.To<Hsb>();
			Assert.AreEqual(expectedColor.H, target.H, 1.8, "(H)" + expectedColor.H + " != " + target.H);
			Assert.AreEqual(expectedColor.S, target.S, 0.005, "(S)" + expectedColor.S + " != " + target.S);
			Assert.AreEqual(expectedColor.B, target.B, 0.005, "(B)" + expectedColor.B + " != " + target.B);
		}
		protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, IHunterLab expectedColor)
		{
			var target = knownColor.To<HunterLab>();
			Assert.AreEqual(expectedColor.L, target.L, 0.5, "(L)" + expectedColor.L + " != " + target.L);
			Assert.AreEqual(expectedColor.A, target.A, 0.64, "(A)" + expectedColor.A + " != " + target.A);
			Assert.AreEqual(expectedColor.B, target.B, 0.64, "(B)" + expectedColor.B + " != " + target.B);
		}
	}
}