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
            var color = providerContext.Path.Split(new[] { System.IO.Path.DirectorySeparatorChar }, 2).Last();
            var mode = providerContext.Path.StartsWith("RgbColor::Background:" + System.IO.Path.DirectorySeparatorChar ) ? RgbColorMode.Background : RgbColorMode.Foreground;

            if (string.IsNullOrEmpty(color) || color.Contains("*"))
            {
                //if (Enum.TryParse(color, true, out X11ColorName x11Color) && string.Equals(color, x11Color.ToString(), StringComparison.OrdinalIgnoreCase))
                return Enum.GetValues(typeof(X11ColorName)).Cast<X11ColorName>().Select(name => new RgbColorItem(RgbColor.X11Palette[(int)name], mode, name.ToString()));
                //return new X11Palette().Distinct().Select(xColor => new RgbColorItem(xColor, mode));
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
