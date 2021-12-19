using System.Linq;
using System.Management.Automation;

namespace PoshCode.Pansies.Provider
{

    // Thinking about renaming this to AnsiColor to prevent collisions with System.Drawing.Color, as I would
    // like to add a ctor that takes a System.Drawing.Color.AntiqueWhite, etc.
    public class RgbColorDrive : CodeOwls.PowerShell.Provider.Drive
    {
        // PSDriveRoot constructor
        public RgbColorDrive(PSDriveInfo name): base(name)
        {

        }
    }
}