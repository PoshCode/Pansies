using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PoshCode.Pansies.Console
{
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
    }
}
