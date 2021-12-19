//---------------------------------------------------------------------
// Author: jachymko
// Creation Date: Feb 14, 2008
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PoshCode.Pansies
{
    public class DictionaryDriveInfo : PSDriveInfo
    {
        public DictionaryDriveInfo(string name, ProviderInfo provider, string description, Dictionary<string,object> obj = null)
            : base(name, provider, string.Empty, description, null)
        {
            Data = obj ?? new Dictionary<string, object>();
            Stack = new Stack<string>();
        }

        public DictionaryDriveInfo(PSDriveInfo driveInfo, Dictionary<string, object> obj = null) : base(driveInfo)
        {
            Data = obj ?? new Dictionary<string, object>();
            Stack = new Stack<string>();
        }

        public Dictionary<string, object> Data
        {
            get;
            private set;
        }

        public Stack<string> Stack
        {
            get;
            private set;
        }

        private readonly Dictionary<String, DictionaryProperty> _properties = new Dictionary<String, DictionaryProperty>(StringComparer.OrdinalIgnoreCase);

        private sealed class DictionaryProperty : Dictionary<String, Object>
        {
            public DictionaryProperty() : base(StringComparer.OrdinalIgnoreCase)
            {
            }

            public void RemoveRange(IEnumerable<String> keys)
            {
                foreach (string key in keys)
                {
                    Remove(key);
                }
            }
        }

        public void ClearProperty(String path, Collection<String> propertiesToClear)
            {
                DictionaryProperty itemProperties;

                if (_properties.TryGetValue(path, out itemProperties))
                {
                    if (propertiesToClear == null || propertiesToClear.Count == 0)
                    {
                        _properties.Remove(path);
                    }
                    else
                    {
                        itemProperties.RemoveRange(propertiesToClear);
                    }
                }
            }

            public PSObject GetProperty(String path, Collection<String> propertiesToGet)
            {
                PSObject result = null;
                DictionaryProperty itemProperties;

                if (_properties.TryGetValue(path, out itemProperties))
                {
                    result = new PSObject();
                    Boolean getAll = (propertiesToGet.Count == 0);

                    foreach (KeyValuePair<String, Object> entry in itemProperties)
                    {
                        if (getAll || ContainsOrdinalCI(propertiesToGet, entry.Key))
                        {
                            result.Properties.Add(new PSNoteProperty(entry.Key, entry.Value));
                        }
                    }
                }

                return result;
            }

            public void SetProperty(String path, PSObject value)
            {
                DictionaryProperty itemProperties;

                if (!_properties.TryGetValue(path, out itemProperties))
                {
                    itemProperties = new DictionaryProperty();
                    _properties[path] = itemProperties;
                }

                foreach (PSPropertyInfo pspi in value.Properties)
                {
                    itemProperties[pspi.Name] = pspi.Value;
                }
            }

            private static bool ContainsOrdinalCI(IEnumerable<String> strings, String value)
            {
                foreach (string s in strings)
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals(s, value))
                    {
                        return true;
                    }
                }

                return false;
            }
    }
}
