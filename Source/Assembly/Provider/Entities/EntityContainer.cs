using CodeOwls.PowerShell.Paths;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using PoshCode.Pansies.Palettes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation.Provider;
using System.Text;
namespace PoshCode.Pansies.Provider
{

    class EntityContainer : PathNodeBase, INewItem
    {
        private SortedList<string, string> items;
        private string name;

        public EntityContainer(string name, SortedList<string, string> entities)
        {
            this.name = name;
            items = entities;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(name, name);
        }

        public override string Name
        {
            get { return name; }
        }

        public IEnumerable<string> NewItemTypeNames => ["Grapheme"];

        public object NewItemParameters => null;

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            //return new XTermPalette().Select(color => new EntityItem(color, Type));
            var name = providerContext.Path.Split([Path.DirectorySeparatorChar], 2).LastOrDefault();
            // Console.WriteLine("EntityContainer.GetNodeChildren: " + name);

            if (string.IsNullOrEmpty(name)) {
                return items.Select(i => new NamedGrapheme(i));
            } /* else if (System.Management.Automation.WildCardPattern.ContainsWildcardCharacters(name)) {
                var pattern = new System.Management.Automation.WildCardPattern(name);
                return items.Where(i => pattern.IsMatch(i.Key)).Select(i => new EntityItem(i));
            } */ else {
                return items.Where(i => i.Key == name).Select(i => new NamedGrapheme(i));
            }
        }

        public IPathValue NewItem(IProviderContext providerContext, string path, string itemTypeName, object newItemValue)
        {
            var name = path.Split([Path.DirectorySeparatorChar], 2).LastOrDefault();
            items.Add(name, newItemValue.ToString());

            return new LeafPathValue(new NamedGrapheme(name, newItemValue.ToString()), name);
        }
    }
}
