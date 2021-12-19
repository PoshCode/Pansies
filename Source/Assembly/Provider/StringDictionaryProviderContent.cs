using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation.Provider;
using System.Collections;
using System.IO;
using System.Management.Automation;
using System.Linq;

namespace PoshCode.Pansies
{
    internal sealed class StringDictionaryProviderContent : DictionaryProviderContent
    {
        public StringDictionaryProviderContent(Dictionary<string, object> obj, Stack<string> stack, string path) : base(obj, stack, path)
        {
        }

        public override IList Read(long readCount)
        {
            StringBuilder result = new StringBuilder();
            if (_path.EndsWith("end", StringComparison.OrdinalIgnoreCase))
            {
                // we need to "end" the previous style
                if (_stack.Count > 0) _stack.Pop();
                if (!_contentRead && _object.ContainsKey(_path)) {

                    var styles = _stack.ToArray();
                    Array.Reverse(styles);
                    var list = styles.ToList();
                    list.Insert(0, _path);

                    foreach (var item in list)
                    {
                        if (_object.ContainsKey(item))
                        {
                            object value = _object[item];
                            var items = value as IList;
                            if (items == null)
                            {
                                result.Append(LanguagePrimitives.ConvertTo(value, typeof(string)));
                            }
                            else
                            {
                                foreach (var i in items)
                                    result.Append(LanguagePrimitives.ConvertTo(i, typeof(string)));
                            }

                            _contentRead = true;
                        }
                    }
                }
            }
            else
            {
                if (!_contentRead && _object.ContainsKey(_path))
                {
                    _stack.Push(_path);
                    if (_path.EndsWith("reset", StringComparison.OrdinalIgnoreCase))
                    {
                        _stack.Clear();
                    }

                    object value = _object[_path];
                    var items = value as IList;

                    if (items == null)
                    {
                        result.Append(LanguagePrimitives.ConvertTo(value, typeof(string)));
                    }
                    else
                    {
                        foreach (var i in items)
                            result.Append(LanguagePrimitives.ConvertTo(i, typeof(string)));
                    }

                    _contentRead = true;
                }
            }

            return new[] { result.ToString() };
        }
    }
}
