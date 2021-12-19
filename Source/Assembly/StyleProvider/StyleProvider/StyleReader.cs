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
    class StyleReader : IContentReader
    {
        private const char  zwnbsp = (char)0xfeff;
        private readonly string[] _result;
        private int _offset = 0;

        public StyleReader(string name)
        {
            _offset = 0;
            _result = StyleProvider.Styles[name].Split(zwnbsp);
        }

        void IContentReader.Close() {
            _offset = 0;
        }

        void IDisposable.Dispose() { }

        IList IContentReader.Read(long readCount)
        {
            var output = _result.Skip(_offset).Take((int)readCount).ToList();
            _offset += (int)readCount;
            return output;
        }

        void IContentReader.Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
    }
}
