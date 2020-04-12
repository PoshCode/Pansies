using System;
using System.Runtime.InteropServices;

namespace PoshCode.Pansies
{
    public static class NativeMethods
    {
        private static readonly IntPtr ConsoleOutputHandle;
        public const int StandardOutputHandle = -11;
        public static readonly IntPtr InvalidHandle = new IntPtr(-1);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll")]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out ConsoleOutputModes mode);

        [Flags]
        public enum ConsoleOutputModes : uint
        {
            EnableProcessedOutput = 1,
            EnableWrapAtEOL = 2,
            EnableVirtualTerminalProcessing = 4,
            DisableNewlineAutoReturn = 8,
            EnableLvbGridWorldwide = 10,
        }

        static NativeMethods()
        {
            ConsoleOutputHandle = GetStdHandle(StandardOutputHandle); // 7
            if (ConsoleOutputHandle == InvalidHandle)
            {
                throw new System.Exception("GetStdHandle->WinError: #" + Marshal.GetLastWin32Error());
            }
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
