//---------------------------------------------------------------------
// Author: jachymko (idea by Oisin Grehan)
//
// Description: PscxSettings provider for global settings
//
// Creation Date: Feb 14, 2008
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshCode.Pansies
{
    [CmdletProvider("Pansies", ProviderCapabilities.None)]
    public class PansiesProvider : DictionaryProviderBase, IPropertyCmdletProvider
    {
        private DictionaryDriveInfo SettingsDriveInfo
        {
            get { return (PSDriveInfo as DictionaryDriveInfo); }
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            return base.NewDrive(new DictionaryDriveInfo(drive));
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {

            return new Collection<PSDriveInfo>()
            {
                new DictionaryDriveInfo("Style", ProviderInfo, "Styles")
            };
        }

        protected override void ClearItem(string path)
        {
            base.ClearItem(path);
            //this.ClearProperty(path, null);
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            base.RemoveItem(path, recurse);
            //this.ClearProperty(path, null);
        }

        public void ClearProperty(String path, Collection<String> properties)
        {
            if (SettingsDriveInfo != null && !string.IsNullOrEmpty(path))
            {
                SettingsDriveInfo.ClearProperty(path, properties);
            }
        }

        public void GetProperty(String path, Collection<String> properties)
        {
            if (SettingsDriveInfo != null && !string.IsNullOrEmpty(path))
            {
                PSObject values = SettingsDriveInfo.GetProperty(path, properties);

                if (values != null)
                {
                    WritePropertyObject(values, path);
                }
            }
        }

        public void SetProperty(String path, PSObject propertyValue)
        {
            if (SettingsDriveInfo != null && !string.IsNullOrEmpty(path))
            {
                SettingsDriveInfo.SetProperty(path, propertyValue);
            }
        }

        public object ClearPropertyDynamicParameters(String path, Collection<String> propertyToClear)
        {
            return null;
        }

        public object GetPropertyDynamicParameters(String path, Collection<String> providerSpecificPickList)
        {
            return null;
        }

        public object SetPropertyDynamicParameters(String path, PSObject propertyValue)
        {
            return null;
        }
    }
}
