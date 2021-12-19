using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshCode.Pansies
{

    // [CmdletProvider("Pansies", ProviderCapabilities.None)]
    public class StyleProvider : NavigationCmdletProvider, IContentCmdletProvider //, IPropertyCmdletProvider
    {
        internal static Dictionary<string, string> Styles = new Dictionary<string, string>();
        private const string PathBase = @"Pansies::Style:\";

        private static string NormalizePath(string path)
        {
            return path.StartsWith(PathBase, StringComparison.OrdinalIgnoreCase) ? path.Substring(PathBase.Length) : path;
        }

        protected override bool HasChildItems(string path)
        {
            WriteDebug("HasChildItems: " + path + "==" + PathBase);
            return path == PathBase;
        }

        protected override bool IsItemContainer(string path)
        {
            WriteDebug("IsItemContainer: " + path + "==" + PathBase);
            return path == PathBase;
        }

        protected override void NewItem(string path, string itemTypeName, object value)
        {
            path = NormalizePath(path);
            WriteDebug("NewItem: " + path + " (" + itemTypeName + ") " + value.ToString().Replace("\x27", "\\e"));
            if (value is string)
            {
                Styles[path] = (string)value;
            }
            else
            {
                throw new NotSupportedException("The Styles provider only accepts string values");
            }
        }


        protected override bool ItemExists(string path)
        {
            path = NormalizePath(path);
            var exists = Styles.ContainsKey(path);
            WriteDebug("Item " + (exists ? "Exists" : "Does Not Exist") + ": " + path);
            return exists;
        }

        protected override void SetItem(string path, object value)
        {
            path = NormalizePath(path);
            WriteDebug("SetItem: " + path + " = " + value.ToString().Replace("\x27", "\\e"));
            if (value is string)
            {
                Styles[path] = (string)value;
            } 
            else
            {
                throw new NotSupportedException("The Styles provider only accepts string values");
            }
        }

        protected override void GetItem(string path)
        {
            path = NormalizePath(path);
            WriteDebug("GetItem: " + path);
            if (Styles.ContainsKey(path))
            {
                WriteItemObject(Styles[path], path, false);
            }
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            path = NormalizePath(path);
            var exists = Styles.ContainsKey(path);
            WriteDebug("RemoveItem: " + path + " (exists? " + exists + ")");
            if (exists) Styles.Remove(path);
        }

        public void ClearContent(string path)
        {
            path = NormalizePath(path);
            WriteDebug("ClearContent: " + path);
            if (Styles.ContainsKey(path)) Styles.Remove(path);
        }

        public IContentReader GetContentReader(string path)
        {
            path = NormalizePath(path);
            WriteDebug("GetContentReader: " + path);
            return new StyleReader(path);
        }

        public IContentWriter GetContentWriter(string path)
        {
            path = NormalizePath(path);
            WriteDebug("GetContentWriter: " + path);
            return new StyleWriter(path);
        }

        //public void GetProperty(string path, Collection<string> providerSpecificPickList)
        //{
        //    path = NormalizePath(path);
        //}

        //public void SetProperty(string path, PSObject propertyValue)
        //{
        //    path = NormalizePath(path);
        //}

        //public void ClearProperty(string path, Collection<string> propertyToClear)
        //{
        //    path = NormalizePath(path);
        //}

        protected override bool IsValidPath(string path)
        {
            WriteDebug("IsValidPath: " + path);
            path = NormalizePath(path);
            return true;
        }
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
            {
                new StyleDrive(
                    new PSDriveInfo( "Style", ProviderInfo, PathBase, "PANSIES Styles", null )
                )
            };
        }

        #region we don't have dynamic parameters

        public object ClearContentDynamicParameters(string path) => null;
        public object ClearPropertyDynamicParameters(string path, Collection<string> propertyToClear) => null;
        public object GetContentReaderDynamicParameters(string path) => null;
        public object GetContentWriterDynamicParameters(string path) => null;
        public object GetPropertyDynamicParameters(string path, Collection<string> providerSpecificPickList) => null;
        public object SetPropertyDynamicParameters(string path, PSObject propertyValue) => null;

        #endregion
    }
}