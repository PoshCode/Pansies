using System;
using System.Collections;
using System.IO;
using System.Management.Automation.Provider;
namespace PoshCode.Pansies.Provider
{
    class GraphemeReader : IContentReader
    {
        private Grapheme Item;

        public GraphemeReader(Grapheme item)
        {
            Item = item;
        }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public IList Read(long readCount)
        {
            if (Item != null) {
                var result = new[] { Item.Value };
                Item = null;
                return result;
            }
            else return null;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
    }
}
