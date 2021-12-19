//---------------------------------------------------------------------
// Author: jachymko
//
// Description: Base class for creating simple providers from PSObjects.
//
// Creation Date: Feb 14, 2008
//---------------------------------------------------------------------

namespace PoshCode.Pansies
{
    public class NamedObject
    {
        public NamedObject(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
    }
}
