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
    class RgbColorContainer : PathNodeBase
    {
        private bool Background;

        public RgbColorContainer(bool background)
        {
            Background = background;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(Background, Name);
        }

        public override string Name
        {
            get { return Background ? "BackgroundColor" : "ForegroundColor"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            return new XTermPalette().Select(color => new RgbColorItem(color, Background));
        }
    }

    class ColorContentReader : IContentReader
    {
        private RgbColor Color;
        private bool Background;

        public ColorContentReader(RgbColor color, bool background)
        {
            Color = color;
            Background = background;
        }
        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public IList Read(long readCount)
        {
            return new[] { Color.ToVtEscapeSequence(Background) };
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
    }


    class RgbColorItem : PathNodeBase, IGetItemContent
    {
        private RgbColor Color;
        private bool Background;

        public RgbColorItem(RgbColor color, bool background)
        {
            Color = color;
            Background = background;
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
            return new ColorContentReader(Color, Background);
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
            get { return Color.ToString(); }
        }
    }
}
