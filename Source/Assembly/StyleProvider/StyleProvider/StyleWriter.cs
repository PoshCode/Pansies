using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace PoshCode.Pansies
{
    class StyleWriter : IContentWriter
    {
        private const char  zwnbsp = (char)0xfeff;
        private string _name;
        private StringBuilder _sb;

        public StyleWriter(string name)
        {
            _sb = new StringBuilder();
            _name = name;
        }

        void IContentWriter.Close() {
            if (_name != null)
            {
                StyleProvider.Styles[_name] = _sb.ToString();
            }
            _name = null;
        }

        void IDisposable.Dispose() { }

        IList IContentWriter.Write(IList content)
        {
            foreach(var item in content.Cast<string>())
            {
                _sb.Append(item);
                _sb.Append(zwnbsp);
            }
            return content;
        }

        void IContentWriter.Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

    }
}
