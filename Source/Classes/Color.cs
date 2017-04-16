using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Text;

namespace PoshCode.Pansies
{

    // Thinking about renaming this to AnsiColor to prevent collisions with System.Drawing.Color, as I would
    // like to add a ctor that takes a System.Drawing.Color.AntiqueWhite, etc.
    public class Color : IEquatable<Color>
    {
        #region private ctors (to be removed?)
            private Color(byte xTerm256Index)
            {
                // TODO: Need a SetXTermColor to set the actual RGB values
                _mode = ColorMode.XTerm256;
                rgb = xTerm256Index << 24;
            }

            private Color(int rgb)
            {
                if (rgb < 0 || rgb > 0x00FFFFFF)
                {
                    throw new ArgumentOutOfRangeException("rgb", "RGB color value must be between 0x000000 and 0xFFFFFF");
                }
                _mode = ColorMode.Rgb24Bit;
                RGB = rgb;
            }

            private Color(int[] rgb)
            {
                if(rgb.Length != 3)
                {
                    throw new ArgumentOutOfRangeException("rgb", "byte array must contain exactly three values: Red, Green, Blue");
                }
                _mode = ColorMode.Rgb24Bit;
                RGB = (rgb[0] << 16) + (rgb[1] << 8) + rgb[2];
            }

            public Color(int red, int green, int blue)
            {
                _mode = ColorMode.Rgb24Bit;
                RGB = (red << 16) + (green << 8) + blue;
            }

        #endregion

        // Use this constructor to specify the default color
        public Color()
        {
            _mode = ColorMode.XTerm256;
            rgb = -1;
        }

        public Color(Color color)
        {
            _mode = color._mode;
            rgb = color.rgb;
        }

        public Color(ConsoleColor consoleColor)
        {
            SetConsoleColor(consoleColor);
        }

        public Color(byte red, byte green, byte blue)
        {
            _mode = ColorMode.Rgb24Bit;
            RGB = (red << 16) + (green << 8) + blue;
        }

        public Color(string color)
        {
            Exception nested;
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color value can't be an empty string.");
            }
            color = color.Trim();

            // handle #rrggbb hex strings like CSS colors ....
            if (color[0] == '#' || (color[0] == '0' && (color[1] == 'x' || color[1] == 'X')))
            {
                try
                {
                    RGB = ParseRGB(color);
                    _mode = ColorMode.Rgb24Bit;
                    return;
                }
                catch(Exception ex)
                {
                    nested = ex;
                }

            }
            else if (color[0] == 'x' && color[1] == 't')
            {
                try {
                    var index = ParseXtermIndex(color.Substring(2));
                    rgb = index << 24;
                    _mode = ColorMode.XTerm256;
                    return;
                }
                catch(Exception ex)
                {
                    nested = ex;
                }
            }

            // three (or less) digit integers are an xterm index
            if (color.Length <= 3)
            {
                try
                {
                    var index = ParseXtermIndex(color);
                    rgb = index << 24;
                    _mode = ColorMode.XTerm256;
                    return;
                }
                catch { }
            }
            // six digit hex integers are CSS colors
            else if (color.Length == 6)
            {
                try
                {
                    RGB = ParseRGB(color);
                    _mode = ColorMode.Rgb24Bit;
                    return;
                }
                catch { }
            }

            // It could also be a named ConsoleColor
            // TODO: Should we support all the named colors? They're very standard:
            // https://en.wikipedia.org/wiki/X11_color_names
            // https://msdn.microsoft.com/en-us/library/system.drawing.color
            // https://en.wikipedia.org/wiki/Web_colors
            // NOTE: the IsDefined is necessary to prevent numerical strings from being parsed as ConsoleColor...
            if (Enum.TryParse(color, true, out ConsoleColor consoleColor) && Enum.IsDefined(typeof(ConsoleColor), consoleColor))
            {
                SetConsoleColor(consoleColor);
            }
            else
            {
                throw new ArgumentException("Unrecognized color: '" + color + "' if you're not using a ConsoleColor name, consider using #RRGGBB css-style colors");
            }
        }

        private static int ParseRGB(string rgbHex)
        {
            rgbHex = rgbHex.TrimStart('#');
            if (int.TryParse(rgbHex, NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out int val))
            {
                if (val < 0 || val > 0xffffff)
                {
                    throw new ArgumentOutOfRangeException("rgbHex", "RGB color value must be between 000000 and FFFFFF");
                }

                return val;
            }
            else
            {
                throw new ArgumentException("rgbHex", "RGB color value must be in hex form, with two hex digits for each component: RRGGBB");
            }
        }

        public static Color FromRGB(string rgbHex)
        {
            var result = ParseRGB(rgbHex);
            return new Color {
                _mode = ColorMode.Rgb24Bit,
                RGB = result
            };
        }

        private static int ParseXtermIndex(string xTermIndex)
        {
            if (int.TryParse(xTermIndex, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out int val))
            {
                if (val < 0 || val > 255)
                {
                    throw new ArgumentOutOfRangeException("xTermIndex", "xTerm index must be between 0 and 255");
                }

                return val;
            }
            else
            {
                throw new ArgumentException("xTermIndex", "xTerm index must be in the form xt123 where 123 is a number between 0 and 255");
            }
        }

        public static Color FromXTermIndex(string xTermIndex)
        {
            // handle #rrggbb hex strings like CSS colors ....
            if (xTermIndex[0] == 'x' || xTermIndex[0] == 'X')
            {
                if(xTermIndex[1] == 't' || xTermIndex[1] == 'T')
                {
                    xTermIndex = xTermIndex.Substring(2);
                }
                else
                {
                    xTermIndex = xTermIndex.Substring(1);
                }
            }
            var result = ParseXtermIndex(xTermIndex);
            return new Color {
                _mode = ColorMode.XTerm256,
                rgb = result << 24
        };
        }

        public static Color ConvertFrom(object inputData)
        {
            if (inputData == null)
            {
                return new Color();
            }

            if (inputData is Color)
            {
                return (Color)inputData;
            }

            if (inputData is ConsoleColor)
            {
                return new Color((ConsoleColor)inputData);
            }

            if (inputData is string)
            {
                return new Color(inputData.ToString());
            }

            if (inputData is byte)
            {
                return new Color((byte)inputData);
            }

            if (inputData is int)
            {
                return new Color((int)inputData);
            }

            if (inputData is int[])
            {
                return new Color(((int[])inputData)[0], ((int[])inputData)[1], ((int[])inputData)[2]);
            }

            if (inputData is byte[])
            {
                return new Color(((byte[])inputData)[0], ((byte[])inputData)[1], ((byte[])inputData)[2]);
            }

            return (Color)LanguagePrimitives.ConvertTo(inputData, typeof(Color));
        }



        private void SetConsoleColor(ConsoleColor color)
        {
            _mode = ColorMode.ConsoleColor;
            rgb = ((int)color) << 24;

            switch (color)
            {
                case ConsoleColor.Black:
                    // ConsoleColor 0, XTerm 0
                    rgb |=0x000000;
                    break;
                case ConsoleColor.DarkBlue:
                    // ConsoleColor 1, XTerm 4
                    rgb |=0x000080;
                    break;
                case ConsoleColor.DarkGreen:
                    // ConsoleColor 2, XTerm 2
                    rgb |=0x008000;
                    break;
                case ConsoleColor.DarkCyan:
                    // ConsoleColor 3, XTerm 6
                    rgb |=0x008080;
                    break;
                case ConsoleColor.DarkRed:
                    // ConsoleColor 4, XTerm 1
                    rgb |=0x800000;
                    break;
                case ConsoleColor.DarkMagenta:
                    // ConsoleColor 5, XTerm 5
                    rgb |=0x800080;
                    break;
                case ConsoleColor.DarkYellow:
                    // ConsoleColor 6, XTerm 3
                    rgb |=0x808000;
                    break;
                case ConsoleColor.Gray:
                    // ConsoleColor 7, XTerm 6
                    rgb |=0xc0c0c0;
                    break;
                case ConsoleColor.DarkGray:
                    // ConsoleColor 8, XTerm 8
                    rgb |=0x808080;
                    break;
                case ConsoleColor.Blue:
                    // ConsoleColor 9, XTerm 12
                    rgb |=0x0000ff;
                    break;
                case ConsoleColor.Green:
                    // ConsoleColor 10, XTerm 10
                    rgb |=0x00ff00;
                    break;
                case ConsoleColor.Cyan:
                    // ConsoleColor 11, XTerm 14
                    rgb |=0x00ffff;
                    break;
                case ConsoleColor.Red:
                    // ConsoleColor 12, XTerm 9
                    rgb |=0xff0000;
                    break;
                case ConsoleColor.Magenta:
                    // ConsoleColor 13, XTerm 12
                    rgb |=0xff00ff;
                    break;
                case ConsoleColor.Yellow:
                    // ConsoleColor 14, XTerm 11
                    rgb |=0xffff00;
                    break;
                case ConsoleColor.White:
                    // ConsoleColor 15, XTerm 15
                    rgb |=0xffffff;
                    break;
            }
        }

        /// <summary>
        /// The default ColorMode for the console
        /// </summary>
        // TODO: Detect platform. Should default to RGB on Windows 10
        public static ColorMode ColorMode { get; set; } = ColorMode.XTerm256;

        /// <summary>
        /// An override mode for this color
        /// </summary>
        private ColorMode _mode = ColorMode;

        /// <summary>
        /// The actual value of the color
        /// </summary>
        private int rgb;

        public int R => (rgb >> 16) & 0xff;

        public int G => (rgb >> 8) & 0xff;

        public int B => rgb & 0xff;

        // TODO: Downsample to the nearest ConsoleColor
        public ConsoleColor ConsoleColor
        {
            get
            {
                if(_mode != ColorMode.ConsoleColor)
                {
                    throw new NotImplementedException("Downsampling to ConsoleColor not implemented yet");
                }
                return (ConsoleColor)((rgb >> 24) & 0xFF);
            }
        }

        // TODO: Downsample to the nearest XTerm256Color
        public byte XTerm256Index
        {
            get
            {
                if (_mode != ColorMode.XTerm256)
                {
                    throw new NotImplementedException("Downsampling to XTerm256 not implemented yet");
                }
                return (byte)(rgb >> 24);
            }
        }

        public int RGB { get => rgb & 0xffffff; set => rgb = value; }

        public override string ToString()
        {
            switch (_mode)
            {
                case ColorMode.ConsoleColor:
                    return Enum.GetName(typeof(ConsoleColor), this.ConsoleColor);

                case ColorMode.XTerm256:
                    return "XTerm256: " + this.XTerm256Index.ToString();

                case ColorMode.Rgb24Bit:
                    return String.Format("RGB: 0x{0:X6}", RGB);
            }

            return base.ToString();
        }

        public string ToString(bool background = false)
        {
            if(rgb == -1) { return string.Empty; }

            switch (_mode)
            {
                case ColorMode.ConsoleColor:
                {
                    switch (this.ConsoleColor)
                    {
                        case ConsoleColor.Black:
                            return background ? "\u001B[40m" : "\u001B[30m";
                        case ConsoleColor.Blue:
                            return background ? "\u001B[104m" : "\u001B[94m";
                        case ConsoleColor.Cyan:
                            return background ? "\u001B[106m" : "\u001B[96m";
                        case ConsoleColor.DarkBlue:
                            return background ? "\u001B[44m" : "\u001B[34m";
                        case ConsoleColor.DarkCyan:
                            return background ? "\u001B[46m" : "\u001B[36m";
                        case ConsoleColor.DarkGray:
                            return background ? "\u001B[100m" : "\u001B[90m";
                        case ConsoleColor.DarkGreen:
                            return background ? "\u001B[42m" : "\u001B[32m";
                        case ConsoleColor.DarkMagenta:
                            return background ? "\u001B[45m" : "\u001B[35m";
                        case ConsoleColor.DarkRed:
                            return background ? "\u001B[41m" : "\u001B[31m";
                        case ConsoleColor.DarkYellow:
                            return background ? "\u001B[43m" : "\u001B[33m";
                        case ConsoleColor.Gray:
                            return background ? "\u001B[47m" : "\u001B[37m";
                        case ConsoleColor.Green:
                            return background ? "\u001B[102m" : "\u001B[92m";
                        case ConsoleColor.Magenta:
                            return background ? "\u001B[105m" : "\u001B[95m";
                        case ConsoleColor.Red:
                            return background ? "\u001B[101m" : "\u001B[91m";
                        case ConsoleColor.White:
                            return background ? "\u001B[107m" : "\u001B[97m";
                        case ConsoleColor.Yellow:
                            return background ? "\u001B[103m" : "\u001B[93m";
                        default:
                            return background ? "\u001B[49m" : "\u001B[39m";
                    }
                }

                case ColorMode.XTerm256:
                    return string.Format(background ? "\u001B[48;5;{0}m" : "\u001B[38;5;{0}m", XTerm256Index);

                case ColorMode.Rgb24Bit:
                default:
                    return string.Format(background ? "\u001B[48;2;{0};{1};{2}m" : "\u001B[38;2;{0};{1};{2}m", R, G, B);
            }
        }

        public static bool operator ==(Color left, Color right)
        {
            if (left is null && right is null)
            {
                return true;
            }
            else if (left is null || right is null)
            {
                return false;
            }
            else
            {
                return (left.rgb == right.rgb) && (left._mode == right._mode);
            }
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return (obj is Color) && Equals((Color)obj);
        }

        public bool Equals(Color other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            // For RGB and DefaultColor (-1 / 0xFFFFFFFF) _value is unique
            if (_mode == ColorMode.Rgb24Bit)
            {
                return rgb;
            }

            // For ConsoleColor / XTerm256, where value is pushed up to 0xFF000000, we just combine
            // the value with the mode in the lower nibble 0xFF0000FF to get a unique number.
            return (int)(rgb & 0xFF000000) | (int)_mode;
        }
    }
}
