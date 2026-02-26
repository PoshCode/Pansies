using CodeOwls.PowerShell.Paths;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using PoshCode.Pansies.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation.Provider;
using System.Text;

namespace PoshCode.Pansies
{
    public class NamedGrapheme : PathNodeBase, IGetItemContent //, ISetItemContent
    {
        private string name;
        public string Value { get; set; }

        /// <summary>
        /// supplies the name for the item at the current path value
        /// </summary>
        public override string Name
        {
            get { return name; }
        }

        public NamedGrapheme(KeyValuePair<string, string> item)
        {
            name = item.Key;
            Value = item.Value;
        }

        public NamedGrapheme(string name, string value)
        {
            this.name = name;
            Value = value;
        }

        /// <summary>
        /// supplies the item for the current path value
        ///
        /// the item it wrapped in either a PathValue instance
        /// that describes the item, its name, and whether it is
        /// a container.
        /// </summary>
        /// <seealso cref="PathValue"/>
        /// <seealso cref="LeafPathValue"/>
        /// <seealso cref="ContainerPathValue"/>
        public override IPathValue GetNodeValue()
        {
            return new LeafPathValue(this, name);
        }

        public IContentReader GetContentReader(IProviderContext providerContext)
        {
            return new GraphemeReader(this);
        }

        public object GetContentReaderDynamicParameters(IProviderContext providerContext)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<int> ToUTF32()
        {
            return Value.ToUTF32();
        }
        public string ToPsEscapedString()
        {
            return Value.ToPsEscapedString();
        }
    }
}
