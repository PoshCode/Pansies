using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace PoshCode.Pansies.Provider
{
    public class DictionaryContainer : PathNodeBase
    {
        private readonly string name;
        private readonly IDictionary<string, string> dictionary;

        public DictionaryContainer(string name, IDictionary<string, string> dictionary)
        {
            this.name = name;
            this.dictionary = dictionary;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(this, name);
        }

        public override string Name
        {
            get { return name; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            // Since this provider is flat, we're ignoring any /folders/ in the path...
            var name = providerContext.Path.Split([System.IO.Path.DirectorySeparatorChar], 2).LastOrDefault();

            if (string.IsNullOrEmpty(name))
            {
                return dictionary.Select(i => new NamedGrapheme(i));
            }
            else if (dictionary.ContainsKey(name))
            {
                return [new NamedGrapheme(name, dictionary[name])];
            }
            else
            {
                return null;
            }
        }
    }

    public static class PansiesDictionaryExtensions
    {
        public static DictionaryContainer ToDriveRoot(this IDictionary<string, string> dictionary, string name) {
            return new DictionaryContainer(name, dictionary);
        }
    }
}
