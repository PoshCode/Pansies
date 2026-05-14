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
        private static TerminalPalette _terminalPalette;
        private static XTermPalette _xTermPalette;
        private static X11Palette _x11Palette;
        private static ConsolePalette _consolePalette;
        public static TerminalPalette TerminalPalette
        {
            get
            {
                if(null == _terminalPalette)
                {
                    _terminalPalette = new TerminalPalette();
                }
                return _terminalPalette;
            }
            set
            {
                _terminalPalette = value;
            }
        }

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

        public static void ResetTerminalPalette()
        {
            _terminalPalette = new TerminalPalette();
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
            // TODO: Need a SetXTermColor to set the actual RGB values
            _mode = ColorMode.XTerm256;
            index = xTerm256Index;
            Initialize(XTermPalette[index]);
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
        #endregion

        public RgbColor(int red, int green, int blue, ColorMode mode = ColorMode.Rgb24Bit)
        {
            _mode = mode;
            R = red;
            G = green;
            B = blue;
        }

        public RgbColor(int red, int green, int blue) : this(red, green, blue, ColorMode.Rgb24Bit) {}

        public RgbColor(ConsoleColor consoleColor)
        {
            // The ConsoleColor and TerminalPalette are the same 16-color palette
            // But ConsoleColor is in an order that doesn't match the escape sequence order
            // So we just don't use it anymore directly, we convert to the TerminalPalette
            RgbColor rgb = ConsolePalette[(int)consoleColor];
            int found;
            if ((found = TerminalPalette.IndexOf(rgb)) >= 0)
            {
                SetTerminalColor(found);
            }
            else
            {
                index = TerminalPalette.FindClosestColorIndex(rgb);
                _mode = ColorMode.TerminalColor;
                RGB = TerminalPalette[index].Value.RGB;
            }
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

        public RgbColor(IColorSpace colorSpace)
        {
            if (colorSpace == null)
            {
                throw new ArgumentNullException(nameof(colorSpace), "Color space cannot be null.");
            }

            if (colorSpace is RgbColor rgbColor)
            {
                _mode = rgbColor._mode;
                index = rgbColor.index;
                RGB = rgbColor.RGB;
            }
            else
            {
                Initialize(colorSpace.ToRgb());
            }
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

            // handle #rrggbb or 0xrrggbb hex strings like CSS colors ....
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
                    _mode = ColorMode.XTerm256;
                    RGB = XTermPalette[index].RGB;
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
                    _mode = ColorMode.XTerm256;
                    RGB = XTermPalette[index].RGB;
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

            // It could be a named Terminal Color
            // What about named Console Colors
            // Basically, what about DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow?
            int found;
            if ((found = TerminalPalette.IndexOf(color)) >= 0)
            {
                SetTerminalColor(found);
                return;
            }

            // Or a named X11 Color
            if ((found = X11Palette.IndexOf(color)) >= 0)
            {
                SetX11Color(found);
                return;
            }

            throw new ArgumentException("Unrecognized color: '" + color + "' if you're not using an x11 color name, consider using #RRGGBB css-style colors");
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
            // handle #rrggbb hex strings like CSS colors ....
            if (xTermIndex[0] == 'x' || xTermIndex[0] == 'X')
            {
                if (xTermIndex[1] == 't' || xTermIndex[1] == 'T')
                {
                    xTermIndex = xTermIndex.Substring(2);
                }
                else
                {
                    xTermIndex = xTermIndex.Substring(1);
                }
            }
            var result = ParseXtermIndex(xTermIndex);
            return new RgbColor
            {
                _mode = ColorMode.XTerm256,
                index = result
            };
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

            if (inputData is IColorSpace) {
                return new RgbColor((IColorSpace)inputData);
            }

            if (inputData is string)
            {
                return new RgbColor(inputData.ToString());
            }

            if (TerminalPalette.TryGetValue(inputData.ToString(), out RgbColor terminalColor))
            {
                return new RgbColor(terminalColor);
            }

            if (X11Palette.TryGetValue(inputData.ToString(), out RgbColor x11Color))
            {
                return new RgbColor(x11Color);
            }

            return new RgbColor();
        }

        private void SetTerminalColor(int index)
        {
            _mode = ColorMode.TerminalColor;
            this.index = index;
            RGB = TerminalPalette[index].Value.RGB;
        }

        private void SetX11Color(int index)
        {
            _mode = ColorMode.X11;
            this.index = index;
            RGB = X11Palette[index].Value.RGB;
        }

        /// <summary>
        /// The default ColorMode for the console
        /// </summary>
        // TODO: Detect from platform. Should default to RGB on Windows 10, use TERM on others
        public static ColorMode ColorMode { get => colorMode; set => colorMode = value; }
        public ColorMode Mode
        {
            get
            {
                return _mode;
            }
            set => _mode = value;
        }

        /// <summary>
        /// An override mode for this color
        /// </summary>
        private ColorMode _mode = ColorMode.Automatic;
        [ThreadStatic] private static ColorMode colorMode = ColorMode.Automatic;

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
                return (ConsoleColor)ConsolePalette.FindClosestColorIndex(this);
            }
        }
        public string TerminalColor
        {
            get
            {
                return TerminalPalette.FindClosestColorName(this);
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

        public string X11ColorName
        {
            get
            {
                if (_mode != ColorMode.X11)
                {
                    return X11Palette.FindClosestColorName(this);
                }
                return X11Palette[index].Key;
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

        public override string ToVTEscapeSequence(bool background = false)
        {
            return ToVtEscapeSequence(background, _mode);
        }
        public string ToVtEscapeSequence(bool background = false, ColorMode? mode = null)
        {
            if (RGB < 0) // all negative values output the "default" escape sequence, regardless of color mode
            {
                return background ? "\e[49m" : "\e[39m";
            }

            if (!mode.HasValue)
            {
                if(_mode != ColorMode.Automatic)
                {
                    mode = _mode;
                }
                else if(ColorMode != ColorMode.Automatic)
                {
                    mode = ColorMode;
                }
                else
                {
                    mode = ColorMode.Rgb24Bit;
                }
            }

            switch (mode.Value)
            {
                case ColorMode.TerminalColor:
                {
                    switch (TerminalColor)
                    {
                        case "Black":
                            return background ? "\e[40m" : "\e[30m";
                        case "Red":
                            return background ? "\e[41m" : "\e[31m";
                        case "Green":
                            return background ? "\e[42m" : "\e[32m";
                        case "Yellow":
                            return background ? "\e[43m" : "\e[33m";
                        case "Blue":
                            return background ? "\e[44m" : "\e[34m";
                        case "Magenta":
                            return background ? "\e[45m" : "\e[35m";
                        case "Cyan":
                            return background ? "\e[46m" : "\e[36m";
                        case "White":
                            return background ? "\e[47m" : "\e[37m";
                        case "BrightBlack":
                            return background ? "\e[100m" : "\e[90m";
                        case "BrightRed":
                            return background ? "\e[101m" : "\e[91m";
                        case "BrightGreen":
                            return background ? "\e[102m" : "\e[92m";
                        case "BrightYellow":
                            return background ? "\e[103m" : "\e[93m";
                        case "BrightBlue":
                            return background ? "\e[104m" : "\e[94m";
                        case "BrightMagenta":
                            return background ? "\e[105m" : "\e[95m";
                        case "BrightCyan":
                            return background ? "\e[106m" : "\e[96m";
                        case "BrightWhite":
                            return background ? "\e[107m" : "\e[97m";
                        default:
                            return background ? "\e[49m" : "\e[39m";
                    }
                }

                case ColorMode.XTerm256:
                {
                    var format = string.Format(background ? "\e[48;5;{0}m" : "\e[38;5;{0}m", XTerm256Index);
                    return format;
                }

                case ColorMode.Rgb24Bit:
                default:
                {
                    return string.Format(background ? "\e[48;2;{0:n0};{1:n0};{2:n0}m" : "\e[38;2;{0:n0};{1:n0};{2:n0}m", R, G, B);
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
                "\e[48;2;{0:n0};{1:n0};{2:n0}m" :
                "\e[38;2;{0:n0};{1:n0};{2:n0}m",
                r, g, b);
        }
    }
}
