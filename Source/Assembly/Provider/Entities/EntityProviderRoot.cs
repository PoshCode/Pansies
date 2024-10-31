using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeOwls.PowerShell.Provider.PathNodes;
using PoshCode.Pansies.Palettes;

namespace PoshCode.Pansies.Provider
{
    class EntityProviderRoot : PathNodeBase
    {
        #region unchanged code from previous version
        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(this, Name);
        }

        public override string Name
        {
            get { return "Entity"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            EntityType type = (EntityType)Enum.Parse(typeof(EntityType), providerContext.Drive.Name);
            var drive = new EntityContainer(type);

            return drive.GetNodeChildren(providerContext);
        }
        #endregion

    }
}
