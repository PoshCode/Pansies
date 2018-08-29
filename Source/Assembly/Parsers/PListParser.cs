using ColorMine.Palettes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace PoshCode.Pansies.Parsers
{
    /// <summary>
    ///   An XML PList Parser for .itermcolors
    /// </summary>
    /// <permission>
    ///   Original Copyright (C) Microsoft.  All rights reserved.
    ///   Licensed under MIT license, as described in the LICENSE file in the root of this project.
    /// </permission>
    public static class PListParser
    {
        // In Windows Color Table order
        static string[] PLIST_COLOR_NAMES = {
            "Ansi 0 Color",  // DARK_BLACK
            "Ansi 4 Color",  // DARK_BLUE
            "Ansi 2 Color",  // DARK_GREEN
            "Ansi 6 Color",  // DARK_CYAN
            "Ansi 1 Color",  // DARK_RED
            "Ansi 5 Color",  // DARK_MAGENTA
            "Ansi 3 Color",  // DARK_YELLOW
            "Ansi 7 Color",  // DARK_WHITE
            "Ansi 8 Color",  // BRIGHT_BLACK
            "Ansi 12 Color", // BRIGHT_BLUE
            "Ansi 10 Color", // BRIGHT_GREEN
            "Ansi 14 Color", // BRIGHT_CYAN
            "Ansi 9 Color",  // BRIGHT_RED
            "Ansi 13 Color", // BRIGHT_MAGENTA
            "Ansi 11 Color", // BRIGHT_YELLOW
            "Ansi 15 Color" // BRIGHT_WHITE
        };
        static string FG_KEY = "Foreground Color";
        static string BG_KEY = "Background Color";
        static string RED_KEY = "Red Component";
        static string GREEN_KEY = "Green Component";
        static string BLUE_KEY = "Blue Component";

        static bool ParseRgbFromXml(XmlNode components, ref RgbColor color)
        {
            int r = -1;
            int g = -1;
            int b = -1;

            foreach (XmlNode c in components.ChildNodes)
            {
                if (c.Name == "key")
                {
                    if (c.InnerText == RED_KEY)
                    {
                        r = (int)(255 * Convert.ToDouble(c.NextSibling.InnerText, CultureInfo.InvariantCulture));
                    }
                    else if (c.InnerText == GREEN_KEY)
                    {
                        g = (int)(255 * Convert.ToDouble(c.NextSibling.InnerText, CultureInfo.InvariantCulture));
                    }
                    else if (c.InnerText == BLUE_KEY)
                    {
                        b = (int)(255 * Convert.ToDouble(c.NextSibling.InnerText, CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (r < 0 || g < 0 || b < 0)
            {
                // Console.WriteLine(Resources.InvalidColor);
                return false;
            }
            color = new RgbColor(r, g, b);
            return true;
        }


        static XmlDocument LoadItermColors(string palettePath)
        {
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object

            try
            {
                xmlDoc.Load(palettePath);
                return xmlDoc;
            }
            catch (XmlException /*e*/) { /* failed to parse */ }
            catch (System.IO.IOException /*e*/) { /* failed to find */ }
            catch (UnauthorizedAccessException /*e*/) { /* unauthorized */ }

            return null;
        }


        public static Palette<RgbColor> Parse(string palettePath)
        {
            if (!File.Exists(palettePath))
            {
                throw new FileNotFoundException("Palette file not found", palettePath);
            }


            XmlDocument xmlDoc = LoadItermColors(palettePath); // Create an XML document object
            if (xmlDoc == null) return null;

            XmlNode root = xmlDoc.GetElementsByTagName("dict")[0];
            if (root == null) return null;

            XmlNodeList children = root.ChildNodes;

            if (children == null || children.Count == 0) return null;

            var palette = new Palette<RgbColor>(PLIST_COLOR_NAMES.Length);

            RgbColor fgColor = null, bgColor = null;
            int colorsFound = 0;
            bool success = false;
            foreach (var tableEntry in children.OfType<XmlNode>().Where(_ => _.Name == "key"))
            {
                var rgb = new RgbColor();
                int index = -1;
                XmlNode components = tableEntry.NextSibling;

                success = ParseRgbFromXml(components, ref rgb);
                if (!success) {
                    break;
                }

                if (tableEntry.InnerText == FG_KEY)
                {
                    fgColor = rgb;
                }
                else if (tableEntry.InnerText == BG_KEY)
                {
                    bgColor = rgb;
                }
                else if (-1 != (index = Array.IndexOf(PLIST_COLOR_NAMES, tableEntry.InnerText)))
                {
                    palette[index] = rgb;
                    colorsFound++;
                }
            }
            if (colorsFound < PLIST_COLOR_NAMES.Length)
            {
                //if (reportErrors)
                //{
                //    Console.WriteLine(Resources.InvalidNumberOfColors);
                //}
                success = false;
            }
            if (!success)
            {
                return null;
            }

            // It's possible nothing will use these, but it can't hurt, right?
            if (fgColor != null && bgColor != null)
            {
                palette.Add(fgColor);
                palette.Add(bgColor);
            }

            return palette;
        }
    }
}
