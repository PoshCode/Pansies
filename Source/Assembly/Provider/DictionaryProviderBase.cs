//---------------------------------------------------------------------
// Author: jachymko
//
// Description: Base class for creating simple providers from PSObjects.
//
// Creation Date: Feb 14, 2008
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshCode.Pansies
{

    public abstract class DictionaryProviderBase : ContainerCmdletProvider, IContentCmdletProvider
    {

        private DictionaryDriveInfo DictionaryDriveInfo
        {
            get { return PSDriveInfo as DictionaryDriveInfo; }
        }

        public Dictionary<string, object> CurrentObject
        {
            get
            {
                if (DictionaryDriveInfo != null)
                {
                    return DictionaryDriveInfo.Data;
                }

                return null;
            }
        }

        public Stack<string> HistoryStack
        {
            get
            {
                if (DictionaryDriveInfo != null)
                {
                    return DictionaryDriveInfo.Stack;
                }

                return null;
            }
        }

        protected override void ClearItem(string path)
        {
            if (CurrentObject != null)
            {
                CurrentObject.Remove(path);
            }            
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            foreach (var entry in CurrentObject)
            {
                WriteItemObject(entry, entry.Key, false);
            }
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            foreach (var name in CurrentObject.Keys)
            {
                WriteItemObject(name, name, false);
            }
        }

        protected override void GetItem(string path)
        {
            if (CurrentObject != null)
            {
                WriteItemObject(new KeyValuePair<string,object>(path, CurrentObject[path]), path, string.IsNullOrEmpty(path));
            }
        }

        protected override bool HasChildItems(string path)
        {
            if (CurrentObject == null)
            {
                return false;
            }

            return string.IsNullOrEmpty(path);
        }

        protected override bool IsValidPath(string path)
        {
            // Altough this may seem strange, it's stolen right from
            // the MS.PS.C.SessionStateProviderBase class.

            if (CurrentObject == null)
            {
                return false;
            }

            return !string.IsNullOrEmpty(path);
        }

        protected override bool ItemExists(string path)
        {
            return string.IsNullOrEmpty(path) || CurrentObject.ContainsKey(path);
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            ClearItem(path);
        }

        protected override void SetItem(string path, object value)
        {
            if (CurrentObject != null)
            {
                CurrentObject[path] = value;
            }
        }

        #region IContentCmdletProvider Members

        private StringDictionaryProviderContent GetContent(string path)
        {
            if (CurrentObject != null)
            {
                return new StringDictionaryProviderContent(CurrentObject, HistoryStack, path);                
            }

            return null;
        }

        public void ClearContent(string path)
        {
            if (CurrentObject.ContainsKey(path))
            {
                CurrentObject[path] = null;
            }
        }

        public IContentReader GetContentReader(string path)
        {
            WriteDebug("GetContentReader: " + path);
            return GetContent(path);
        }

        public IContentWriter GetContentWriter(string path)
        {
            return GetContent(path);
        }

        public object ClearContentDynamicParameters(string path)
        {
            return null;
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            return null;
        }

        public object GetContentWriterDynamicParameters(string path)
        {
            return null;
        }

        #endregion
    }
}
