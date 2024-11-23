using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;

namespace PoshCode.Pansies.Provider
{
    [CmdletProvider("Entity", ProviderCapabilities.None)]
    public class EntityProvider : CodeOwls.PowerShell.Provider.Provider
    {
        /// <summary>
        /// a required P2F override
        ///
        /// supplies P2F with the path processor for this provider
        /// </summary>
        protected override CodeOwls.PowerShell.Paths.Processors.IPathResolver PathResolver
        {
            get { return new EntityResolver(); }
        }

        /// <summary>
        /// overridden to supply a default drive when the provider is loaded
        /// </summary>
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drives = new Collection<PSDriveInfo>
            {
                new EntityDrive(
                    new PSDriveInfo( "Esc", ProviderInfo, "Entity::EscapeSequences:" + System.IO.Path.DirectorySeparatorChar, "EscapeSequences", null, "Esc:" )
                ),
                new EntityDrive(
                    new PSDriveInfo( "Extra", ProviderInfo, "Entity::Extended:" + System.IO.Path.DirectorySeparatorChar, "Named Extended Strings", null, "Extra:" )
                )
            };
            if (Entities.EnableNerdFonts) {
                drives.Add(
                    new EntityDrive(
                        new PSDriveInfo( "NF", ProviderInfo, "Entity::NerdFontSymbols:" + System.IO.Path.DirectorySeparatorChar, "NerdFont Symbols", null, "NF:" )
                    )
                );
            }
            if (Entities.EnableEmoji) {
                drives.Add(
                    new EntityDrive(
                        new PSDriveInfo( "Emoji", ProviderInfo, "Entity::Emoji:" + System.IO.Path.DirectorySeparatorChar, "Emoji 16", null, "Emoji:" )
                    )
                );
            };
            return drives;
        }
    }
}
