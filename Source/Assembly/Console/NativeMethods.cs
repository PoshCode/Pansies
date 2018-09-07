using System;
using System.Runtime.InteropServices;

namespace PoshCode.Pansies.Console
{
    public class NativeMethods
    {
        public const int StandardOutputHandle = -11;
        public static readonly IntPtr InvalidHandle = new IntPtr(-1);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coordinate
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rectangle
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [Flags]
        public enum ConsoleOutputModes : uint
        {
            EnableProcessedOutput = 1,
            EnableWrapAtEOL = 2,
            EnableVirtualTerminalProcessing = 4,
            DisableNewlineAutoReturn = 8,
            EnableLvbGridWorldwide = 10,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ColorReference
        {
            internal uint ColorDWORD;

            public ColorReference(RgbColor color)
            {
                ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
            }

            public ColorReference(uint r, uint g, uint b)
            {
                ColorDWORD = r + (g << 8) + (b << 16);
            }

            public RgbColor GetRgbColor()
            {
                return new RgbColor((int)(0x000000FFU & ColorDWORD), (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
            }

            public void SetColor(RgbColor color)
            {
                ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ConsoleScreenBufferInfoEx
        {
            public int cbSize;
            public Coordinate dwSize;
            public Coordinate dwCursorPosition;
            public ushort wAttributes;
            public Rectangle srWindow;
            public Coordinate dwMaximumWindowSize;
            public ushort wPopupAttributes;
            public bool bFullscreenSupported;
            public ColorReference Black;
            public ColorReference DarkBlue;
            public ColorReference DarkGreen;
            public ColorReference DarkCyan;
            public ColorReference DarkRed;
            public ColorReference DarkMagenta;
            public ColorReference DarkYellow;
            public ColorReference Gray;
            public ColorReference DarkGray;
            public ColorReference Blue;
            public ColorReference Green;
            public ColorReference Cyan;
            public ColorReference Red;
            public ColorReference Magenta;
            public ColorReference Yellow;
            public ColorReference White;
        }

        [DllImport("kernel32.dll")]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll")]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out ConsoleOutputModes mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref ConsoleScreenBufferInfoEx csbe);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref ConsoleScreenBufferInfoEx csbe);

        [DllImport("kernel32")]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
#if NET451
        [System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.Success)]
        [System.Security.SuppressUnmanagedCodeSecurity]
#endif
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}
