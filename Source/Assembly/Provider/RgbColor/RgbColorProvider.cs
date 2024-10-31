using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;

namespace PoshCode.Pansies.Provider
{
    [CmdletProvider("RgbColor", ProviderCapabilities.None)]
    public class RgbColorProvider : CodeOwls.PowerShell.Provider.Provider
    {
             /// <summary>
             /// a required P2F override
             ///
             /// supplies P2F with the path processor for this provider
             /// </summary>
        protected override CodeOwls.PowerShell.Paths.Processors.IPathResolver PathResolver
        {
            get { return new RgbColorResolver(); }
        }

        /// <summary>
        /// overridden to supply a default drive when the provider is loaded
        /// </summary>
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
            {
                new RgbColorDrive(
                    new PSDriveInfo( "Fg", ProviderInfo, "RgbColor::Foreground:" + System.IO.Path.DirectorySeparatorChar, "Foreground Colors", null )
                ),
                new RgbColorDrive(
                    new PSDriveInfo( "Bg", ProviderInfo, "RgbColor::Background:" + System.IO.Path.DirectorySeparatorChar, "Background Colors", null )
                )
            };
        }
    }
}
