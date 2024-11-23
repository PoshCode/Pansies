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
    enum EntityType {
        NerdFont,
        Emoji,
        EscapeSequences,
        ExtendedCharacters
    }

    class EntityContainer : PathNodeBase
    {
        private SortedList<string, string> items;
        private EntityType Type;

        public EntityContainer(EntityType type)
        {
            Type = type;
            switch (type)
            {
                case EntityType.NerdFont:
                    items = Entities.NerdFonts;
                    break;
                case EntityType.Emoji:
                    items = Entities.Emoji;
                    break;
                case EntityType.EscapeSequences:
                    items = Entities.EscapeSequences;
                    break;
                case EntityType.ExtendedCharacters:
                    items = Entities.ExtendedCharacters;
                    break;
            }
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(Type, Name);
        }

        public override string Name
        {
            get { return Type.ToString(); }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            //return new XTermPalette().Select(color => new EntityItem(color, Type));
            var name = providerContext.Path.Split([Path.DirectorySeparatorChar], 2).LastOrDefault();
            // Console.WriteLine("EntityContainer.GetNodeChildren: " + name);

            if (string.IsNullOrEmpty(name)) {
                return items.Select(i => new Grapheme(i));
            } /* else if (System.Management.Automation.WildCardPattern.ContainsWildcardCharacters(name)) {
                var pattern = new System.Management.Automation.WildCardPattern(name);
                return items.Where(i => pattern.IsMatch(i.Key)).Select(i => new EntityItem(i));
            } */ else {
                return items.Where(i => i.Key == name).Select(i => new Grapheme(i));
            }
        }
    }
}
