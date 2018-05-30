using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider.PathNodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoshCode.Pansies.Provider
{
    class RgbColorResolver : PathResolverBase
    {
        /// <summary>
        /// returns the first node factory object in the path graph
        /// </summary>
        protected override IPathNode Root
        {
            get
            {
                return new RgbColorProviderRoot();
            }
        }
    }
}
