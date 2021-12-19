//---------------------------------------------------------------------
// Author: jachymko
// Creation Date: Feb 14, 2008
//---------------------------------------------------------------------

using System;
using System.Management.Automation;

namespace PoshCode.Pansies
{
    public class PSObjectDriveInfo : PSDriveInfo
    {
        public PSObjectDriveInfo(string name, ProviderInfo provider, string description, PSObject obj = null)
            : base(name, provider, string.Empty, description, null)
        {
            DriveObject = obj ?? new PSObject();
        }

        public PSObjectDriveInfo(PSDriveInfo driveInfo, PSObject obj = null) : base(driveInfo)
        {
            DriveObject = obj ?? new PSObject();
        }

        public PSObject DriveObject
        {
            get;
            private set;
        }
    }
}
