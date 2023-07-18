using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PoshCode.Pansies
{
    public static partial class Entities
    {
        public static bool EnableEmoji = true;
        public static bool EnableNerdFonts = true;
        private static readonly char[] _entityEndingChars = new char[] { ';', '&' };

        public static SortedList<string, string> EscapeSequences = new SortedList<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            ["Clear"] = "\u001B[0m",
            ["Esc"] = "\u001B[",
            ["Store"] = "\u001B[s",
            ["Recall"] = "\u001B[u",
        };

        public static SortedList<string, string> ExtendedCharacters = new SortedList<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            ["Branch"] = "\ue0a0",                // Branch symbol
            ["ColorSeparator"] = "\u258C",        // ▌
            ["Gear"] = "\u26ef",                  // The settings icon, I use it for debug
            ["Lock"] = "\ue0a2",                  // Padlock
            ["Power"] = "\u26a1",                 // The Power lightning-bolt icon
            ["ReverseColorSeparator"] = "\u2590", // ▐
            ["ReverseSeparator"] = "\u25C4",      // ◄
            ["Separator"] = "\u25BA",             // ►
        };

        public static string Decode(string value)
        {
            // Don't create StringBuilder if we don't have anything to encode
            if ((string.IsNullOrEmpty(value)) || value.IndexOf('&') == -1)
            {
                return value;
            }
            int stop = value.Length;
            var output = new StringBuilder(stop);

            int end = 0, start = 0;
            while((start = value.IndexOf('&', end)) != -1)
            {
                // if it's at the end, we're done here
                if (start == value.Length - 1)
                {
                    break;
                }
                string result;
                output.Append(value.Substring(end, start - end));

                // We found a '&'. Now look for the next ';' or '&'.
                // If we find another '&' then this is not an entity, but that one might be
                end = value.IndexOfAny(_entityEndingChars, start + 1);
                if (end > 0 && value[end] == ';')
                {
                    string entity = value.Substring(start + 1, end - start - 1);
                    end++;

                    if (EscapeSequences.TryGetValue(entity, out result))
                    {
                        output.Append(result);
                    }
                    else if (ExtendedCharacters.TryGetValue(entity, out result))
                    {
                        output.Append(result);
                    }
                    else if (EnableEmoji && Emoji.TryGetValue(entity, out result))
                    {
                        output.Append(result);
                    }
                    else if (EnableNerdFonts && NerdFonts.TryGetValue(entity, out result))
                    {
                        output.Append(result);
                    }
                    else
                    {
                        output.Append('&');
                        output.Append(entity);
                        output.Append(';');
                        continue;
                    }
                }
                // if we reached the end, stop looking
                if (end < 0)
                {
                    // and don't loose the end of the string
                    end = start;
                    break;
                }

            }
            // make sure we don't loose anything off the end
            output.Append(value.Substring(end, value.Length - end));

            // we don't handle &#123; let WebUtility do that
            value = WebUtility.HtmlDecode(output.ToString());
            return value;
        }
    }
}
