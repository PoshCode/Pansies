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
    enum RgbColorMode { Foreground, Background }

    class RgbColorContainer : PathNodeBase
    {
        private RgbColorMode RgbColorMode;

        public RgbColorContainer(RgbColorMode mode)
        {
            RgbColorMode = mode;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(RgbColorMode, Name);
        }

        public override string Name
        {
            get { return RgbColorMode.ToString(); }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            return new XTermPalette().Select(color => new RgbColorItem(color, RgbColorMode));
        }
    }

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
            if (Content != null)  {
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


    class RgbColorItem : PathNodeBase, IGetItemContent
    {
        private readonly String name;
        private RgbColor Color;
        private RgbColorMode RgbColorMode;

        public RgbColorItem(RgbColor color, RgbColorMode mode, string name = null)
        {
            Color = color;
            RgbColorMode = mode;
            this.name = name ?? Color.ToString();
        }

        /// <summary>
        /// supplies the item for the current path value
        ///
        /// the item it wrapped in either a PathValue instance
        /// that describes the item, its name, and whether it is
        /// a container.
        /// </summary>
        /// <seealso cref="PathValue"/>
        /// <seealso cref="LeafPathValue"/>
        /// <seealso cref="ContainerPathValue"/>
        public override IPathValue GetNodeValue()
        {
            return new LeafPathValue(Color, Name);
        }

        public IContentReader GetContentReader(IProviderContext providerContext)
        {
            return new ColorContentReader(Color, RgbColorMode);
        }

        public object GetContentReaderDynamicParameters(IProviderContext providerContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// supplies the name for the item at the current path value
        /// </summary>
        public override string Name
        {
            get { return name; }
        }
    }
}
