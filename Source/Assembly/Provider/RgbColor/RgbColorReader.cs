using CodeOwls.PowerShell.Paths;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using PoshCode.Pansies.Palettes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation.Provider;
using System.Text;

namespace PoshCode.Pansies.Provider
{
    class ColorContentReader : IContentReader
    {
        private string Content;

        public ColorContentReader(RgbColor color, RgbColorMode mode)
        {
            if (color == null)
            {
                if (mode == RgbColorMode.Background)
                {
                    Content = "\u001B[49m";
                }
                else
                {
                    Content = "\u001B[39m";
                }
            }
            else
            {
                Content = color.ToVtEscapeSequence(mode == RgbColorMode.Background);
            }
        }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public IList Read(long readCount)
        {
            if (Content != null)
            {
                var result = new[] { Content };
                Content = null;
                return result;
            }
            else return null;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
    }
}
