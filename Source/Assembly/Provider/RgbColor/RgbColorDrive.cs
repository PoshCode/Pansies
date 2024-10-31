using System.Linq;
using System.Management.Automation;

namespace PoshCode.Pansies.Provider
{
    public class RgbColorDrive : CodeOwls.PowerShell.Provider.Drive
    {
        // PSDriveRoot constructor
        public RgbColorDrive(PSDriveInfo name): base(name)
        {

        }
    }
}
