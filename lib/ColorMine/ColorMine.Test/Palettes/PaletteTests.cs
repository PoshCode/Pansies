using ColorMine.ColorSpaces;
using ColorMine.Palettes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColorMine.Test.Palettes
{
    [TestClass]
    public class PaletteTests
    {
        [TestMethod]
        public void ConsolePalette()
        {
            var ConsolePalette = new Palette<Rgb>() {
                new Rgb(0x00, 0x00, 0x00),
                new Rgb(0x80, 0x00, 0x00),
                new Rgb(0x00, 0x80, 0x00),
                new Rgb(0x80, 0x80, 0x00),
                new Rgb(0x00, 0x00, 0x80),
                new Rgb(0x80, 0x00, 0x80),
                new Rgb(0x00, 0x80, 0x80),
                new Rgb(0xc0, 0xc0, 0xc0),
                new Rgb(0x80, 0x80, 0x80),
                new Rgb(0xff, 0x00, 0x00),
                new Rgb(0x00, 0xff, 0x00),
                new Rgb(0xff, 0xff, 0x00),
                new Rgb(0x00, 0x00, 0xff),
                new Rgb(0xff, 0x00, 0xff),
                new Rgb(0x00, 0xff, 0xff),
                new Rgb(0xff, 0xff, 0xff)
            };
            Rgb testColor;
            Palette<Rgb>.FindResult<Rgb> testResult;

            // Going to run a test at each end of the spectrum
            testColor = new Rgb(0x70, 0x20, 0x20);
            testResult = ConsolePalette.FindClosestColor<Rgb>(testColor);
            Assert.AreEqual(1, testResult.Index, testColor.ToString() + " != " + testResult.ToString() + " => " + testResult.Distance);

            testColor = new Rgb(0x20, 0x90, 0x20);
            testResult = ConsolePalette.FindClosestColor<Rgb>(testColor);
            Assert.AreEqual(2, testResult.Index, testColor.ToString() + " != " + testResult.ToString() + " => " + testResult.Distance);

            testColor = new Rgb(0x90, 0x70, 0x20);
            testResult = ConsolePalette.FindClosestColor<Rgb>(testColor);
            Assert.AreEqual(3, testResult.Index, testColor.ToString() + " != " + testResult.ToString() + " => " + testResult.Distance);

            testColor = new Rgb(0x20, 0x20, 0x87);
            testResult = ConsolePalette.FindClosestColor<Rgb>(testColor);
            Assert.AreEqual(4, testResult.Index, testColor.ToString() + " != " + testResult.ToString() + " => " + testResult.Distance);

            testColor = new Rgb(0x90, 0x00, 0x87);
            testResult = ConsolePalette.FindClosestColor<Rgb>(testColor);
            Assert.AreEqual(5, testResult.Index, testColor.ToString() + " != " + testResult.ToString() + " => " + testResult.Distance);

            testColor = new Rgb(0x20, 0x90, 0x87);
            testResult = ConsolePalette.FindClosestColor<Rgb>(testColor);
            Assert.AreEqual(6, testResult.Index, testColor.ToString() + " != " + testResult.ToString() + " => " + testResult.Distance);

        }
    }
}
