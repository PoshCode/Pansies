using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;

namespace PoshCode.Pansies
{
    public class StyleDrive : PSDriveInfo
    {
        public StyleDrive(PSDriveInfo driveInfo) : base(driveInfo)
        {
        }

        internal static string GetDriveName(string path)
        {
            Regex re = new Regex(@"^([^:]+):");
            var match = re.Match(path);
            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }
    }
}