using ColorMine.ColorSpaces;
using PoshCode.Pansies;
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
            internal short X;
            internal short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rectangle
        {
            internal short Left;
            internal short Top;
            internal short Right;
            internal short Bottom;
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
            internal int cbSize;
            internal Coordinate dwSize;
            internal Coordinate dwCursorPosition;
            internal ushort wAttributes;
            internal Rectangle srWindow;
            internal Coordinate dwMaximumWindowSize;
            internal ushort wPopupAttributes;
            internal bool bFullscreenSupported;
            internal ColorReference Black;
            internal ColorReference DarkBlue;
            internal ColorReference DarkGreen;
            internal ColorReference DarkCyan;
            internal ColorReference DarkRed;
            internal ColorReference DarkMagenta;
            internal ColorReference DarkYellow;
            internal ColorReference Gray;
            internal ColorReference DarkGray;
            internal ColorReference Blue;
            internal ColorReference Green;
            internal ColorReference Cyan;
            internal ColorReference Red;
            internal ColorReference Magenta;
            internal ColorReference Yellow;
            internal ColorReference White;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref ConsoleScreenBufferInfoEx csbe);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref ConsoleScreenBufferInfoEx csbe);

        [DllImport("kernel32")]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
#if NOTCORE
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
#endif
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}
