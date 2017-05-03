using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace PoshCode.Pansies
{
    public static class Entities
    {
        private static readonly char[] _entityEndingChars = new char[] { ';', '&' };

        public static Dictionary<string, string> EscapeSequences = new Dictionary<string, string>
        {
            { "Esc", "\u001B[" },
            { "Store", "\u001B[s" },
            { "Recall", "\u001B[u" },
            { "Clear", "\u001B[0m" }
        };

        public static Dictionary<string, char> ExtendedCharacters = new Dictionary<string, char>
        {
            { "ColorSeparator", '\u258C'}, // ▌
            { "ReverseColorSeparator", '\u2590'}, // ▐
            { "Separator", '\u25BA'}, // ►
            { "ReverseSeparator", '\u25C4'}, // ◄
            { "Branch", '\ue0a0'}, // Branch symbol
            { "Lock", '\ue0a2'}, // Padlock
            { "Gear", '\u26ef'}, // The settings icon, I use it for debug
            { "Power", '\u26a1'}, // The Power lightning-bolt icon
        };

        public static string Decode(string value)
        {
            var output = new StringWriter(CultureInfo.InvariantCulture);

            int l = value.Length;
            for (int i = 0; i < l; i++)
            {
                char ch = value[i];

                if (ch == '&')
                {
                    // We found a '&'. Now look for the next ';' or '&'.
                    // If we find another '&' then this is not an entity,
                    int index = value.IndexOfAny(_entityEndingChars, i + 1);
                    if (index > 0 && value[index] == ';')
                    {
                        string entity = value.Substring(i + 1, index - i - 1);
                        i = index;
                        if (EscapeSequences.ContainsKey(entity))
                        {
                            output.Write(EscapeSequences[entity]);
                            continue;
                        }
                        else if (ExtendedCharacters.ContainsKey(entity))
                        {
                            output.Write(ExtendedCharacters[entity]);
                            continue;
                        }
                        else
                        {
                            output.Write('&');
                            output.Write(entity);
                            output.Write(';');
                            continue;
                        }
                    }
                }

                output.Write(ch);
            }
            
            value = WebUtility.HtmlDecode(output.ToString());
            return value;
        }
    }
}
