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

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            //return new XTermPalette().Select(color => new EntityItem(color, Type));
            var name = providerContext.Path.Split(new[] { System.IO.Path.DirectorySeparatorChar }, 2).LastOrDefault();

            if (string.IsNullOrEmpty(name)) {
                return items.Select(i => new EntityItem(i));
            } /* else if (System.Management.Automation.WildCardPattern.ContainsWildcardCharacters(name)) {
                var pattern = new System.Management.Automation.WildCardPattern(name);
                return items.Where(i => pattern.IsMatch(i.Key)).Select(i => new EntityItem(i));
            } */ else {
                return items.Where(i => i.Key == name).Select(i => new EntityItem(i));
            }
        }
    }

    class EntityReader : IContentReader
    {
        private EntityItem Item;

        public EntityReader(EntityItem item)
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


    class EntityItem : PathNodeBase, IGetItemContent
    {
        private readonly String Key;
        internal readonly string Value;

        public EntityItem(KeyValuePair<string, string> item)
        {
            Key = item.Key;
            Value = item.Value;
        }

        public EntityItem(string name, string value)
        {
            Key = name;
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
            return new LeafPathValue(this, Key);
        }

        public IContentReader GetContentReader(IProviderContext providerContext)
        {
            return new EntityReader(this);
        }

        public object GetContentReaderDynamicParameters(IProviderContext providerContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// supplies the name for the item at the current path value
        /// </summary>
        public override string Name
        {
            get { return Key; }
        }
    }
}
