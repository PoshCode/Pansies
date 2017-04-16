using System;
using System.Collections.Generic;
using System.Text;

namespace PoshCode.Pansies
{
    public static class Entities
    {
        public static Dictionary<string, string> EscapeSequences = new Dictionary<string, string>
            {
                { "Esc", "\u001B[" },
                { "Store", "\u001B[s" },
                { "Recall", "\u001B[u" },
                { "Clear", "\u001B[0m" }
            };

    }
}
