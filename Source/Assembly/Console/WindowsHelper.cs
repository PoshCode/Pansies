using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PoshCode.Pansies.Console
{
    using ColorMine.Palettes;
    using Microsoft.Win32;
    using PoshCode.Pansies.Palettes;
    using System.Linq;
    using static NativeMethods;
    public static class WindowsHelper
    {
        private static readonly IntPtr ConsoleOutputHandle;

        /// <summary>
        /// Initializes new instance of ConsoleColorsHelper class
        /// </summary>
        static WindowsHelper()
        {
            // TODO: second instance created is crashing. Find out why and how to fix it / prevent. In the worst case - hidden control instance singleton
            // Not very important, can wait
            ConsoleOutputHandle = GetStdHandle(StandardOutputHandle); // 7
            if (ConsoleOutputHandle == InvalidHandle)
            {
                throw new System.Exception("GetStdHandle->WinError: #" + Marshal.GetLastWin32Error());
            }
        }

        public static ConsoleScreenBufferInfoEx GetConsoleScreenBuffer()
        {
            ConsoleScreenBufferInfoEx csbe = new ConsoleScreenBufferInfoEx();
            csbe.cbSize = Marshal.SizeOf(csbe); // 96 = 0x60
            bool brc = GetConsoleScreenBufferInfoEx(ConsoleOutputHandle, ref csbe);
            if (!brc)
            {
                throw new System.Exception("GetConsoleScreenBufferInfoEx->WinError: #" + Marshal.GetLastWin32Error());
            }
            // work around a weird bug in windows ...
            // https://stackoverflow.com/questions/25274019/strange-setconsolescreenbufferinfoex-behavior
            ++csbe.srWindow.Bottom;
            ++csbe.srWindow.Right;
            return csbe;
        }

        public static void SetConsoleScreenBuffer(ConsoleScreenBufferInfoEx screenBufferInfo)
        {
            bool brc = SetConsoleScreenBufferInfoEx(ConsoleOutputHandle, ref screenBufferInfo);
            if (!brc)
            {
                throw new System.Exception("SetConsoleScreenBufferInfoEx->WinError: #" + Marshal.GetLastWin32Error());
            }
        }

        public static IDictionary<ConsoleColor, RgbColor> GetCurrentColorset()
        {
            var csbe = GetConsoleScreenBuffer();
            var result = new Dictionary<ConsoleColor, RgbColor>(16)
            {
                { ConsoleColor.Black, csbe.Black.GetRgbColor() },
                { ConsoleColor.DarkBlue, csbe.DarkBlue.GetRgbColor() },
                { ConsoleColor.DarkGreen, csbe.DarkGreen.GetRgbColor() },
                { ConsoleColor.DarkCyan, csbe.DarkCyan.GetRgbColor() },
                { ConsoleColor.DarkRed, csbe.DarkRed.GetRgbColor() },
                { ConsoleColor.DarkMagenta, csbe.DarkMagenta.GetRgbColor() },
                { ConsoleColor.DarkYellow, csbe.DarkYellow.GetRgbColor() },
                { ConsoleColor.Gray, csbe.Gray.GetRgbColor() },
                { ConsoleColor.DarkGray, csbe.DarkGray.GetRgbColor() },
                { ConsoleColor.Blue, csbe.Blue.GetRgbColor() },
                { ConsoleColor.Green, csbe.Green.GetRgbColor() },
                { ConsoleColor.Cyan, csbe.Cyan.GetRgbColor() },
                { ConsoleColor.Red, csbe.Red.GetRgbColor() },
                { ConsoleColor.Magenta, csbe.Magenta.GetRgbColor() },
                { ConsoleColor.Yellow, csbe.Yellow.GetRgbColor() },
                { ConsoleColor.White, csbe.White.GetRgbColor() }
            };

            return result;
        }

        public static void LoadCurrentColorset(this IList<RgbColor> colors, bool addScreenAndPopup = false)
        {
            var csbe = GetConsoleScreenBuffer();
            colors.Clear();

            colors.Add(csbe.Black.GetRgbColor());
            colors.Add(csbe.DarkBlue.GetRgbColor());
            colors.Add(csbe.DarkGreen.GetRgbColor());
            colors.Add(csbe.DarkCyan.GetRgbColor());
            colors.Add(csbe.DarkRed.GetRgbColor());
            colors.Add(csbe.DarkMagenta.GetRgbColor());
            colors.Add(csbe.DarkYellow.GetRgbColor());
            colors.Add(csbe.Gray.GetRgbColor());
            colors.Add(csbe.DarkGray.GetRgbColor());
            colors.Add(csbe.Blue.GetRgbColor());
            colors.Add(csbe.Green.GetRgbColor());
            colors.Add(csbe.Cyan.GetRgbColor());
            colors.Add(csbe.Red.GetRgbColor());
            colors.Add(csbe.Magenta.GetRgbColor());
            colors.Add(csbe.Yellow.GetRgbColor());
            colors.Add(csbe.White.GetRgbColor());

            if (addScreenAndPopup)
            {
                // the default colors, foreground first
                colors.Add(colors[csbe.wAttributes ^ ((csbe.wAttributes >> 4) << 4)]);
                colors.Add(colors[csbe.wAttributes >> 4]);

                // the popup colors, foreground first
                colors.Add(colors[csbe.wPopupAttributes ^ ((csbe.wPopupAttributes >> 4) << 4)]);
                colors.Add(colors[csbe.wPopupAttributes >> 4]);
            }
        }

        public static ConsolePalette GetCurrentConsolePalette(bool addScreenAndPopup = false)
        {
            return new ConsolePalette(false, addScreenAndPopup);
        }

        public static void SetCurrentConsolePalette(IList<RgbColor> colors)
        {
            if(colors.Count < 16)
            {
                throw new InvalidOperationException("Palette has insufficient colors (need at least 16 colors to set the Console Palette)");
            }

            var csbe = GetConsoleScreenBuffer();
            csbe.Black.SetColor(colors[(int)ConsoleColor.Black]);
            csbe.DarkBlue.SetColor(colors[(int)ConsoleColor.DarkBlue]);
            csbe.DarkGreen.SetColor(colors[(int)ConsoleColor.DarkGreen]);
            csbe.DarkCyan.SetColor(colors[(int)ConsoleColor.DarkCyan]);
            csbe.DarkRed.SetColor(colors[(int)ConsoleColor.DarkRed]);
            csbe.DarkMagenta.SetColor(colors[(int)ConsoleColor.DarkMagenta]);
            csbe.DarkYellow.SetColor(colors[(int)ConsoleColor.DarkYellow]);
            csbe.Gray.SetColor(colors[(int)ConsoleColor.Gray]);
            csbe.DarkGray.SetColor(colors[(int)ConsoleColor.DarkGray]);
            csbe.Blue.SetColor(colors[(int)ConsoleColor.Blue]);
            csbe.Green.SetColor(colors[(int)ConsoleColor.Green]);
            csbe.Cyan.SetColor(colors[(int)ConsoleColor.Cyan]);
            csbe.Red.SetColor(colors[(int)ConsoleColor.Red]);
            csbe.Magenta.SetColor(colors[(int)ConsoleColor.Magenta]);
            csbe.Yellow.SetColor(colors[(int)ConsoleColor.Yellow]);
            csbe.White.SetColor(colors[(int)ConsoleColor.White]);


            // Index 16 & 17 are the default FG and BG (which can only be one of the 16 colors)
            if (colors.Count >= 18)
            {
                var palette = new Palette<RgbColor>(colors.Take(16));
                var fg = palette.FindClosestColorIndex(colors[16]);
                var bg = palette.FindClosestColorIndex(colors[17]);

                csbe.wAttributes = (ushort)(fg | bg << 4);
                // Index 18 and 19 are the "popup" FG and BG (which can only be one of the 16 colors)
                if (colors.Count >= 20)
                {
                    fg = palette.FindClosestColorIndex(colors[18]);
                    bg = palette.FindClosestColorIndex(colors[19]);
                    csbe.wPopupAttributes = (ushort)(fg | bg << 4);
                }
            }

            SetConsoleScreenBuffer(csbe);
        }

        /// <summary>
        /// Set the default console palette in the registery
        /// </summary>
        /// <param name="colors"></param>
        /// <remarks>Note that Windows stores colors in ConsoleColor enum order and in BGR byte order <see cref="ColorReference"/></remarks>
        public static void SetDefaultConsolePalette(IList<RgbColor> colors)
        {
            if (colors.Count < 16)
            {
                throw new InvalidOperationException("Palette has insufficient colors (need at least 16 colors to set the Console Palette)");
            }

            using (RegistryKey consoleKey = Registry.CurrentUser.OpenSubKey("Console", true))
            {
                for (int i = 0; i < 16; i++)
                {
                    string valueName = "ColorTable" + (i < 10 ? "0" : "") + i;
                    consoleKey.SetValue(valueName, colors[i].BGR, RegistryValueKind.DWord);
                }

                // Index 16 & 17 are the default FG and BG (which can only be one of the 16 colors)
                if (colors.Count >= 18)
                {
                    var palette = new Palette<RgbColor>(colors.Take(16));
                    var fg = palette.FindClosestColorIndex(colors[16]);
                    var bg = palette.FindClosestColorIndex(colors[17]);

                    consoleKey.SetValue("ScreenColors", (fg | bg << 4), RegistryValueKind.DWord);
                    // Index 18 and 19 are the "popup" FG and BG (which can only be one of the 16 colors)
                    if (colors.Count >= 20)
                    {
                        fg = palette.FindClosestColorIndex(colors[18]);
                        bg = palette.FindClosestColorIndex(colors[19]);
                        consoleKey.SetValue("PopupColors", (fg | bg << 4), RegistryValueKind.DWord);
                    }
                }
            }
        }
        /// <summary>
        /// Get the default console palette from the registery
        /// </summary>
        /// <param name="colors"></param>
        /// <remarks>Note that Windows stores colors in ConsoleColor enum order and in BGR byte order <see cref="ColorReference"/></remarks>
        public static ConsolePalette GetDefaultConsolePalette(bool addScreenAndPopup = false)
        {
            ConsolePalette colors = new ConsolePalette(false);
            using (RegistryKey consoleKey = Registry.CurrentUser.OpenSubKey("Console", true))
            {
                for (int i = 0; i < colors.Count; i++)
                {
                    string valueName = "ColorTable" + (i < 10 ? "0" : "") + i;
                    colors[i].BGR = (int)consoleKey.GetValue(valueName, colors[i].BGR);
                }
                if (addScreenAndPopup)
                {
                    var color = (int)consoleKey.GetValue("ScreenColors", colors[0].BGR);
                    // the default colors, foreground first
                    colors.Add(colors[color >> 4 & color]);
                    colors.Add(colors[color >> 4]);

                    color = (int)consoleKey.GetValue("PopupColors", colors[7].BGR);
                    // the popup colors, foreground first
                    colors.Add(colors[color >> 4 & color]);
                    colors.Add(colors[color >> 4]);
                }
            }

            return colors;
        }

        public static void EnableVirtualTerminalProcessing()
        {
            ConsoleOutputModes mode;
            if (!GetConsoleMode(ConsoleOutputHandle, out mode))
            {
                mode = ConsoleOutputModes.EnableProcessedOutput | ConsoleOutputModes.EnableWrapAtEOL;
            }
            mode |= ConsoleOutputModes.EnableVirtualTerminalProcessing;

            SetConsoleMode(ConsoleOutputHandle, (uint)mode);
        }

        public static void DisableVirtualTerminalProcessing()
        {
            ConsoleOutputModes mode;
            if (!GetConsoleMode(ConsoleOutputHandle, out mode))
            {
                mode = ConsoleOutputModes.EnableProcessedOutput | ConsoleOutputModes.EnableWrapAtEOL;
            }
            mode &= ~ConsoleOutputModes.EnableVirtualTerminalProcessing;

            SetConsoleMode(ConsoleOutputHandle, (uint)mode);
        }
    }
}
