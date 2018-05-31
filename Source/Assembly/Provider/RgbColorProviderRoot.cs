using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeOwls.PowerShell.Provider.PathNodes;
using PoshCode.Pansies.Palettes;

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
            var color = providerContext.Path.Split(new[] { '\\' }, 2).Last();
            var mode = providerContext.Path.StartsWith("RgbColor::Background:\\") ? RgbColorMode.Background : RgbColorMode.Foreground;

            if (string.IsNullOrEmpty(color) || color.Contains("*"))
            {
                return new XTermPalette().Select(xColor => new RgbColorItem(xColor, mode));
            }
            else if(StringComparer.OrdinalIgnoreCase.Equals(color, "clear"))
            {
                return new[] {
                    new RgbColorItem(null, mode, color)
                };
            }
            else
            {
                return new[] {
                    new RgbColorItem(new RgbColor(color), mode, color)
                };
            }
        }
        #endregion

    }
}
