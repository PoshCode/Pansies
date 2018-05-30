using System;
using System.Collections.Generic;
using System.Text;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace PoshCode.Pansies.Provider
{
    class RgbColorProviderRoot : PathNodeBase
    {
        #region unchanged code from previous version
        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(this, Name);
        }

        public override string Name
        {
            get { return "RgbColor"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            return new[] { new RgbColorContainer(false), new RgbColorContainer(true) };
        }
        #endregion

    }
}
