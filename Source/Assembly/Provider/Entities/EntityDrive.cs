using System.Linq;
using System.Management.Automation;

namespace PoshCode.Pansies.Provider
{
    public class EntityDrive : CodeOwls.PowerShell.Provider.Drive
    {
        // PSDriveRoot constructor
        public EntityDrive(PSDriveInfo name): base(name)
        {

        }
    }
}
