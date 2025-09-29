using PoshCode.Pansies.ColorSpaces;
using PoshCode.Pansies.Palettes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PoshCode.Pansies
{
    public partial class RgbColor : Rgb, IEquatable<RgbColor>
    {
        private int index = -1;
        private static ConsolePalette _consolePalette;
        private static XTermPalette _xTermPalette;
        private static X11Palette _x11Palette;
        private static readonly ColorMode DefaultColorMode = GetRecommendedColorMode();

        public static ConsolePalette ConsolePalette
        {
            get
            {
                if (null == _consolePalette)
                {
                    _consolePalette = new ConsolePalette();
                }
                return _consolePalette;
            }
            set
            {
                _consolePalette = value;
            }
        }

        public static void ResetConsolePalette()
        {
            _consolePalette = new ConsolePalette();
        }

        public static XTermPalette XTermPalette
        {
            get
            {
                if (null == _xTermPalette)
                {
                    _xTermPalette = new XTermPalette();
                }
                return _xTermPalette;
            }
            set
            {
                _xTermPalette = value;
            }
        }
        public static X11Palette X11Palette
        {
            get
            {
                if (null == _x11Palette)
                {
                    _x11Palette = new X11Palette();
                }
                return _x11Palette;
            }
            set
            {
                _x11Palette = value;
            }
        }
        #region private ctors (to be removed?)
        private RgbColor(byte xTerm256Index)
        {
            SetXTermColor(xTerm256Index);
        }

        private RgbColor(int rgb)
        {
            if (rgb < 0 || rgb > 0xFFFFFF)
            {
                throw new ArgumentOutOfRangeException("rgb", "RGB color value must be between 0x000000 and 0xFFFFFF");
            }
            _mode = ColorMode.Rgb24Bit;
            RGB = rgb;
        }

        private RgbColor(int[] rgb)
        {
            if (rgb.Length != 3)
            {
                throw new ArgumentOutOfRangeException("rgb", "byte array must contain exactly three values: Red, Green, Blue");
            }
            _mode = ColorMode.Rgb24Bit;
            R = rgb[0];
            G = rgb[1];
            B = rgb[2];
        }

        public RgbColor(int red, int green, int blue)
        {
            _mode = ColorMode.Rgb24Bit;
            R = red;
            G = green;
            B = blue;
        }
        #endregion

        public RgbColor(X11ColorName colorName)
        {
            SetX11Color(colorName);
        }

        //public RgbColor(RgbColor color)
        //{
        //    _mode = color._mode;
        //    RGB = color.RGB;
        //}

        public RgbColor(ConsoleColor consoleColor)
        {
            SetConsoleColor(consoleColor);
        }

        public RgbColor(byte red, byte green, byte blue)
        {
            _mode = ColorMode.Rgb24Bit;
            RGB = (red << 16) + (green << 8) + blue;
        }

        public RgbColor(string color)
        {
            FromPsMetadata(color);
        }

        public string ToString(bool AsOrdinal = false)
        {
            if (AsOrdinal)
            {
                return base.ToString();
            }

            switch (_mode)
            {
                case ColorMode.ConsoleColor:
                    return Enum.GetName(typeof(ConsoleColor), this.ConsoleColor);

                case ColorMode.XTerm256:
                    return String.Format("xt{0:0}", this.XTerm256Index);

                case ColorMode.Rgb24Bit:
                default:
                    return String.Format("#{0:X6}", RGB);

            }
        }

        public string ToPsMetadata()
        {
            return ToString(false);
        }

        public void FromPsMetadata(string metadata)
        {
            string color = metadata;
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
                catch (Exception ex)
                {
                    nested = ex;
                }

            }
            else if (color[0] == 'x' && color[1] == 't')
            {
                try
                {
                    index = ParseXtermIndex(color.Substring(2));
                    SetXTermColor(index);
                    return;
                }
                catch (Exception ex)
                {
                    nested = ex;
                }
            }

            // three (or less) digit integers are an xterm index
            if (color.Length <= 3)
            {
                try
                {
                    index = ParseXtermIndex(color);
                    SetXTermColor(index);
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

            // It could be a named x11 COLOR
            // NOTE: the IsDefined is necessary to prevent numerical strings from being parsed as ConsoleColor...
            if (Enum.TryParse(color, true, out X11ColorName x11Color) && string.Equals(color, x11Color.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                SetX11Color(x11Color);
            }
            else
            {
                throw new ArgumentException("Unrecognized color: '" + color + "' if you're not using an x11 color name, consider using #RRGGBB css-style colors");
            }

            // X11 Colors are also ConsoleColors, but we want ConsoleColor to win
            if (Enum.TryParse(color, true, out ConsoleColor consoleColor) && string.Equals(color, consoleColor.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                SetConsoleColor(consoleColor);
            }
        }


        public override string ToString()
        {
            return ToString(false);
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

        public static RgbColor FromXTermIndex(string xTermIndex)
        {
            if (string.IsNullOrEmpty(xTermIndex))
            {
                throw new ArgumentException("xTermIndex cannot be null or empty", nameof(xTermIndex));
            }

            if (xTermIndex[0] == 'x' || xTermIndex[0] == 'X')
            {
                if (s.StartsWith("xt", StringComparison.OrdinalIgnoreCase))
                {
                    xTermIndex = xTermIndex.Substring(2);
                }
                else
                {
                    xTermIndex = xTermIndex.Substring(1);
                }
            }

            var result = ParseXtermIndex(xTermIndex);
            return CreateFromXTermIndex(result);
        }



        public static RgbColor FromRgb(string rgbHex)
        {
            var result = ParseRGB(rgbHex);
            return new RgbColor
            {
                _mode = ColorMode.Rgb24Bit,
                RGB = result
            };
        }
        public static RgbColor FromRgb(int red, int green, int blue)
        {
            return new RgbColor(red, green, blue);
        }

        public static RgbColor FromRgb(int rgb)
        {
            return new RgbColor(rgb);
        }

        public static RgbColor FromRegistry(int bgr)
        {
            return new RgbColor
            {
                BGR = bgr
            };
        }

        public static RgbColor ConvertFrom(object inputData)
        {
            if (inputData == null)
            {
                return new RgbColor();
            }

            if (inputData is RgbColor)
            {
                return (RgbColor)inputData;
            }

            if (inputData is ConsoleColor)
            {
                return new RgbColor((ConsoleColor)inputData);
            }

            if (inputData is X11ColorName)
            {
                return new RgbColor((X11ColorName)inputData);
            }

            if (inputData is string)
            {
                return new RgbColor(inputData.ToString());
            }

            if (inputData is byte)
            {
                return new RgbColor((byte)inputData);
            }

            if (inputData is int)
            {
                return new RgbColor((int)inputData);
            }

            if (inputData is int[])
            {
                return new RgbColor(((int[])inputData)[0], ((int[])inputData)[1], ((int[])inputData)[2]);
            }

            if (inputData is byte[])
            {
                return new RgbColor(((byte[])inputData)[0], ((byte[])inputData)[1], ((byte[])inputData)[2]);
            }

            return new RgbColor();
        }

        private void SetXTermColor(int xTermIndex)
        {
            if (xTermIndex < 0 || xTermIndex > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(xTermIndex), "xTerm index must be between 0 and 255");
            }

            _mode = ColorMode.XTerm256;
            index = xTermIndex;
            Initialize(XTermPalette[index]);
        }

        private static RgbColor CreateFromXTermIndex(int xTermIndex)
        {
            var color = new RgbColor();
            color.SetXTermColor(xTermIndex);
            return color;
        }

        public static ColorMode GetRecommendedColorMode(IDictionary<string, string> environment = null, OperatingSystem os = null)
        {
            if (environment == null)
            {
                environment = GetEnvironmentVariables();
            }

            if (os == null)
            {
                os = Environment.OSVersion;
            }

            var isWindows = os.Platform == PlatformID.Win32NT;

            if (isWindows)
            {
                if (os.Version.Major >= 10)
                {
                    return ColorMode.Rgb24Bit;
                }

                return ColorMode.ConsoleColor;
            }

            var colorTerm = GetEnvironmentValue(environment, "COLORTERM");
            if (!string.IsNullOrEmpty(colorTerm))
            {
                if (colorTerm.IndexOf("truecolor", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    colorTerm.IndexOf("24bit", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return ColorMode.Rgb24Bit;
                }
            }

            var term = GetEnvironmentValue(environment, "TERM");
            if (!string.IsNullOrEmpty(term))
            {
                if (term.IndexOf("truecolor", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    term.IndexOf("direct", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return ColorMode.Rgb24Bit;
                }

                if (term.IndexOf("256color", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return ColorMode.XTerm256;
                }

                if (term.IndexOf("color", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return ColorMode.ConsoleColor;
                }
            }

            return ColorMode.ConsoleColor;
        }

        private static IDictionary<string, string> GetEnvironmentVariables()
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                if (entry.Key is string key)
                {
                    result[key] = entry.Value?.ToString();
                }
            }

            return result;
        }

        private static string GetEnvironmentValue(IDictionary<string, string> environment, string key)
        {
            if (environment != null && environment.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        private void SetConsoleColor(ConsoleColor color)
        {
            _mode = ColorMode.ConsoleColor;
            index = (int)color;
            RGB = ConsolePalette[index].RGB;
        }

        private void SetX11Color(X11ColorName color)
        {
            _mode = ColorMode.X11;
            index = (int)color;
            RGB = X11Palette[index].RGB;
        }

        /// <summary>
        /// The default ColorMode for the console
        /// </summary>
        public static ColorMode ColorMode { get; set; } = DefaultColorMode;

        public ColorMode Mode { get => _mode; set => _mode = value; }

        /// <summary>
        /// An override mode for this color
        /// </summary>
        private ColorMode _mode = ColorMode.Automatic;

        public int RGB
        {
            get
            {
                return (((int)R) << 16) + (((int)G) << 8) + (int)B;
            }
            set
            {
                R = (value >> 16) & 0xff;
                G = (value >> 8) & 0xff;
                B = value & 0xff;
            }
        }

        public int BGR
        {
            get
            {
                return (((int)B) << 16) + (((int)G) << 8) + (int)R;
            }
            set
            {
                B = (value >> 16) & 0xff;
                G = (value >> 8) & 0xff;
                R = value & 0xff;
            }
        }

        public ConsoleColor ConsoleColor
        {
            get
            {
                if (_mode != ColorMode.ConsoleColor)
                {
                    return (ConsoleColor)ConsolePalette.FindClosestColorIndex(this);
                }
                return (ConsoleColor)index;
            }
        }

        public byte XTerm256Index
        {
            get
            {
                if (_mode != ColorMode.XTerm256)
                {
                    return (byte)XTermPalette.FindClosestColorIndex(this);
                }
                return (byte)index;
            }
        }

        public X11ColorName X11ColorName
        {
            get
            {
                if (_mode != ColorMode.X11)
                {
                    return (X11ColorName)X11Palette.FindClosestColorIndex(this);
                }
                return (X11ColorName)index;
            }
        }

        public RgbColor GetComplement(bool HighContrast = false, bool BlackAndWhite = false)
        {
            if (BlackAndWhite)
            {
                // Since the point of BlackAndWhite is to ensure contrast in the console
                // We may need to first convert to the nearest ConsoleColor
                // new RgbColor(ConsoleColor).To<HunterLab>()
                if (To<HunterLab>().L < 50)
                {
                    return new RgbColor(ConsoleColor.White);
                }
                else
                {
                    return new RgbColor(ConsoleColor.Black);
                }
            }

            var hsl = To<Hsl>();
            hsl.H = (hsl.H + 180) % 360;

            if (HighContrast)
            {
                var result = hsl.To<HunterLab>();
                result.L = (To<HunterLab>().L + 50) % 100;
                return result.To<RgbColor>();
            }
            return hsl.To<RgbColor>();
        }

        public string ToVt(bool background = false, ColorMode? mode = null)
        {
            return ToVtEscapeSequence(background, mode);
        }

        public string ToVtEscapeSequence(bool background = false, ColorMode? mode = null)
        {
            if (!mode.HasValue)
            {
                if (RgbColor.ColorMode != ColorMode.Automatic)
                {
                    mode = RgbColor.ColorMode;
                }
                else if (_mode != ColorMode.Automatic)
                {
                    mode = _mode;
                }
                else
                {
                    mode = ColorMode.Rgb24Bit;
                }
            }

            switch (mode.Value)
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
                    {
                        var format = string.Format(background ? "\u001B[48;5;{0}m" : "\u001B[38;5;{0}m", XTerm256Index);
                        return format;
                    }

                case ColorMode.Rgb24Bit:
                default:
                    {
                        if (RGB < 0)
                        {
                            return string.Empty;
                        }

                        var format = string.Format(background ? "\u001B[48;2;{0:n0};{1:n0};{2:n0}m" : "\u001B[38;2;{0:n0};{1:n0};{2:n0}m", R, G, B);
                        return format;
                    }
            }
        }

        public static bool operator ==(RgbColor left, RgbColor right)
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
                return (left.RGB == right.RGB) && (left._mode == right._mode);
            }
        }

        public static bool operator !=(RgbColor left, RgbColor right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return (obj is RgbColor) && Equals((RgbColor)obj);
        }

        public bool Equals(RgbColor other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            // For RGB and DefaultColor (-1 / 0xFFFFFFFF) _value is unique
            if (_mode == ColorMode.Rgb24Bit)
            {
                return RGB;
            }

            // For ConsoleColor / XTerm256, push the index to 0xFF000000, and combine
            // the value with the mode in the lower nibble 0xFF0000FF to get a unique number.
            return (int)(index << 24) | (int)_mode;
        }

        public static string Foreground(string hexColor)
        {
            return Foreground(ParseRGB(hexColor));
        }

        public static string Foreground(int color)
        {
            return VtEscapeSequence(color, false);
        }

        public static string Background(string hexColor)
        {
            return Background(ParseRGB(hexColor));
        }

        public static string Background(int color)
        {
            return VtEscapeSequence(color, true);
        }

        public static string VtEscapeSequence(int color, bool background)
        {
            int r = (color >> 16) & 0xff;
            int g = (color >> 8) & 0xff;
            int b = color & 0xff;

            return string.Format(background ?
                "\u001B[48;2;{0:n0};{1:n0};{2:n0}m" :
                "\u001B[38;2;{0:n0};{1:n0};{2:n0}m",
                r, g, b);
        }
    }
}

