using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PoshCode.Pansies.Console
{
    using ColorMine.Palettes;
    using Microsoft.Win32;
    using PoshCode.Pansies.Palettes;
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

        internal static ConsoleScreenBufferInfoEx GetConsoleScreenBuffer()
        {
            ConsoleScreenBufferInfoEx csbe = new ConsoleScreenBufferInfoEx();
            csbe.cbSize = Marshal.SizeOf(csbe); // 96 = 0x60

            bool brc = GetConsoleScreenBufferInfoEx(ConsoleOutputHandle, ref csbe);
            if (!brc)
            {
                throw new System.Exception("GetConsoleScreenBufferInfoEx->WinError: #" + Marshal.GetLastWin32Error());
            }
            return csbe;
        }

        internal static void SetConsoleScreenBuffer(ConsoleScreenBufferInfoEx screenBufferInfo)
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

        public static void LoadCurrentColorset(this IList<RgbColor> colors)
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
        }

        public static ConsolePalette GetCurrentConsolePalette()
        {
            return new ConsolePalette();
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

            SetConsoleScreenBuffer(csbe);
        }

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
                    consoleKey.SetValue(valueName, colors[i].RGB, RegistryValueKind.DWord);
                }
            }
        }
        public static ConsolePalette GetDefaultConsolePalette()
        {
            ConsolePalette colors = new ConsolePalette();
            using (RegistryKey consoleKey = Registry.CurrentUser.OpenSubKey("Console", true))
            {
                for (int i = 0; i < colors.Count; i++)
                {
                    string valueName = "ColorTable" + (i < 10 ? "0" : "") + i;
                    colors[i].RGB = (int)consoleKey.GetValue(valueName, colors[i].RGB);
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
