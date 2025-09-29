using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PoshCode.Pansies
{
    public static class StringExtensions
    {
        public static IEnumerable<int> ToUtf32(this string value)
        {
            for (var i = 0; i < value.Length; i++)
            {
                if (char.IsHighSurrogate(value[i]))
                {
                    if (value.Length <= i + 1 || !char.IsLowSurrogate(value[i + 1]))
                    {
                        throw new InvalidDataException("High surrogate must be followed by a low surrogate.");
                    }

                    yield return char.ConvertToUtf32(value[i], value[++i]);
                }
                else
                {
                    yield return value[i];
                }
            }
        }

        public static string ToPsEscapedString(this string value)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < value.Length; i++)
            {
                if (char.IsHighSurrogate(value[i]))
                {
                    if (value.Length <= i + 1 || !char.IsLowSurrogate(value[i + 1]))
                    {
                        throw new InvalidDataException("High surrogate must be followed by a low surrogate.");
                    }

                    builder.AppendFormat("`u{{{0:x6}}}", char.ConvertToUtf32(value[i], value[++i]));
                    continue;
                }

                var current = value[i];

                switch (current)
                {
                    case '\u001b':
                        builder.Append("`e");
                        break;
                    case '`':
                        builder.Append("``");
                        break;
                    case '$':
                        builder.Append("`$");
                        break;
                    case '"':
                        builder.Append("`\"");
                        break;
                    default:
                        if (current < 32 || current > 126)
                        {
                            builder.AppendFormat("`u{{{0:x4}}}", (int)current);
                        }
                        else
                        {
                            builder.Append(current);
                        }
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
