using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation.Provider;
using System.Collections;
using System.IO;
using System.Management.Automation;

namespace PoshCode.Pansies
{
    internal class DictionaryProviderContent : IContentReader, IContentWriter
    {
        internal readonly Dictionary<string, object> _object;
        internal readonly Stack<string> _stack;
        internal readonly String _path;
        internal bool _contentRead;

        public DictionaryProviderContent(Dictionary<string,object> obj, Stack<string> stack, String path)
        {
            _object = obj;
            _stack = stack;
            _path = path;
        }

        public virtual void Close()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public virtual IList Read(long readCount)
        {
            IList result = null;

            if (!_contentRead && _object.ContainsKey(_path))
            {
                object value = _object[_path];
                result = value as IList;

                if (result == null)
                {
                    result = new object[] { value };
                }

                _contentRead = true;
            }

            return result;
        }

        public virtual IList Write(IList content)
        {
            object value = content;

            if (content.Count == 1)
            {
                value = content[0];
            }

            _object[_path] = value;
            
            return content;
        }
    }
}
