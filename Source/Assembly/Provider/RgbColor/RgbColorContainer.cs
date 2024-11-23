using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using PoshCode.Pansies.Palettes;
using System.Collections.Generic;
using System.Linq;
namespace PoshCode.Pansies.Provider
{
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

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            return new XTermPalette().Select(color => new RgbColorItem(color, RgbColorMode));
        }
    }
}
