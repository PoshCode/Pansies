using PoshCode.Pansies;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PoshCode.Pansies.Console
{
    using static NativeMethods;
    public class WindowHelper
    {
        private readonly IntPtr _consoleOutputHandle;


        /// <summary>
        /// Initializes new instance of ConsoleColorsHelper class
        /// </summary>
        public WindowHelper()
        {
            // TODO: second instance created is crashing. Find out why and how to fix it / prevent. In the worst case - hidden control instance singleton
            // Not very important, can wait
            _consoleOutputHandle = GetStdHandle(StandardOutputHandle); // 7
            if (_consoleOutputHandle == InvalidHandle)
            {
                throw new System.Exception("GetStdHandle->WinError: #" + Marshal.GetLastWin32Error());
            }
        }

        #region IDsiposable implementation

            /// <inheritdoc />
            public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ConsoleController"/> class. 
        /// </summary>
        ~WindowHelper()
        {
            // NOTE: Leave out the finalizer altogether if this class doesn't 
            // own unmanaged resources itself, but leave the other methods
            // exactly as they are. 
            this.Dispose(false);
        }

        /// <summary>
        /// Actual disposing method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }

            // free native resources
            if (_consoleOutputHandle != InvalidHandle)
            {
                CloseHandle(_consoleOutputHandle);
            }
        }

        #endregion IDsiposable implementation

        internal ConsoleScreenBufferInfoEx GetConsoleScreenBufferInfoEx()
        {
            ConsoleScreenBufferInfoEx csbe = new ConsoleScreenBufferInfoEx();
            csbe.cbSize = Marshal.SizeOf(csbe); // 96 = 0x60

            bool brc = NativeMethods.GetConsoleScreenBufferInfoEx(_consoleOutputHandle, ref csbe);
            if (!brc)
            {
                throw new System.Exception("GetConsoleScreenBufferInfoEx->WinError: #" + Marshal.GetLastWin32Error());
            }
            return csbe;
        }

        public IDictionary<ConsoleColor, RgbColor> GetCurrentColorset()
        {
            var csbe = GetConsoleScreenBufferInfoEx();
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
    }
}
