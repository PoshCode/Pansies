using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace PoshCode.Pansies
{
    public class Text : IEquatable<Text>
    {
        private Regex _escapeCode = new Regex("\u001B\\P{L}+\\p{L}", RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        /// <value>A string</value>
        public object Object { get; set; }

        public object Separator { get; set; } = " ";

        /// <summary>
        /// Copy the simple stringification from Write-Host instead of using LanguagePrimitives.ConvertTo
        /// </summary>
        /// <remarks>
        ///     Specifically, Write-Host uses it's own <code>Separator</code> instead of $OFS
        /// </remarks>
        /// <param name="object">The object</param>
        /// <param name="separator">The separator</param>
        /// <returns>A simple string representation</returns>
        public static string ConvertToString(object @object, string separator = " ")
        {
            if (@object != null)
            {
                string s = @object as string;
                IEnumerable enumerable = null;
                if (s != null)
                {
                    // strings are IEnumerable, so we special case them
                    if (s.Length > 0)
                    {
                        return s;
                    }
                }
                else if ((enumerable = @object as IEnumerable) != null)
                {
                    // unroll enumerables, including arrays.

                    bool printSeparator = false;
                    StringBuilder result = new StringBuilder();

                    foreach (object element in enumerable)
                    {
                        if (printSeparator == true && separator != null)
                        {
                            result.Append(separator.ToString());
                        }

                        result.Append(ConvertToString(element, separator));
                        printSeparator = true;
                    }

                    return result.ToString();
                }
                else
                {
                    s = @object.ToString();

                    if (s.Length > 0)
                    {
                        return s;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets or Sets the background color for the block
        /// </summary>
        public RgbColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets or Sets the foreground color for the block
        /// </summary>
        public RgbColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets the length of the text representation (without ANSI escape sequences).
        /// </summary>
        public int Length {
            get {
                var result = ConvertToString(Object, Separator.ToString());
                return result != null ? result.Length : 0 ;
            }
        }

        public bool Clear { get; set; } = false;
        public bool Entities { get; set; } = true;

        /// <summary>
        /// This constructor is here so we can allow partial matches to the property names.
        /// </summary>
        /// <param name="values"></param>
        public Text(IDictionary values) : this()
        {
            foreach (string key in values.Keys)
            {
                var pattern = "^" + Regex.Escape(key);
                if ("bg".Equals(key, StringComparison.OrdinalIgnoreCase) || Regex.IsMatch("BackgroundColor", pattern, RegexOptions.IgnoreCase))
                {
                    BackgroundColor = RgbColor.ConvertFrom(values[key]);
                }
                else if ("fg".Equals(key, StringComparison.OrdinalIgnoreCase) || Regex.IsMatch("ForegroundColor", pattern, RegexOptions.IgnoreCase))
                {
                    ForegroundColor = RgbColor.ConvertFrom(values[key]);
                }
                else if (Regex.IsMatch("text", pattern, RegexOptions.IgnoreCase) || Regex.IsMatch("Content", pattern, RegexOptions.IgnoreCase) || Regex.IsMatch("Object", pattern, RegexOptions.IgnoreCase))
                {
                    Object = values[key];
                }
                else
                {
                    throw new ArgumentException("Unknown key '" + key + "' in " + values.GetType().Name + ". Allowed values are BackgroundColor (or bg), ForegroundColor (or fg), and Object (also called Content or Text)");
                }
            }
        }
        // Make sure we can output plain text
        public Text(string text) : this()
        {
            Object = text;
        }

        // Make sure we support the default ctor
        public Text() { }

        public override string ToString()
        {
            return GetString(ForegroundColor, BackgroundColor, Object, Separator.ToString(), Clear, Entities);
        }

        public static string GetString(RgbColor foreground, RgbColor background, object @object, string separator = " ", bool clear = false, bool entities = true)
        {
            var output = new StringBuilder();
            // There's a bug in Conhost where an advanced 48;2 RGB code followed by a console code doesn't render the RGB value
            // So we try to put the ConsoleColor first, if it's there ...
            if (null != foreground)
            {
                if (foreground.Mode == ColorMode.ConsoleColor)
                {
                    output.Append(foreground.ToVtEscapeSequence(false));
                    if (null != background)
                    {
                        output.Append(background.ToVtEscapeSequence(true));
                    }
                }
                else
                {
                    if (null != background)
                    {
                        output.Append(background.ToVtEscapeSequence(true));
                    }
                    output.Append(foreground.ToVtEscapeSequence(false));
                }
            }
            else if (null != background)
            {
                output.Append(background.ToVtEscapeSequence(true));
            }

            if (null != @object)
            {
                //var scriptBlock = @object as ScriptBlock;
                //var text = (string)LanguagePrimitives.ConvertTo(scriptBlock != null ? scriptBlock.Invoke() : @object, typeof(string));
                var text = ConvertToString(@object, separator);

                if (!string.IsNullOrEmpty(text))
                {
                    output.Append(text);
                }
            }

            if (clear)
            {
                if (null != background)
                {
                    // clear background
                    output.Append("\u001B[49m");
                }
                if (null != foreground)
                {
                    // clear foreground
                    output.Append("\u001B[39m");
                }
            }

            if(entities)
            {
                return PoshCode.Pansies.Entities.Decode(output.ToString());
            }
            else
            {
                return output.ToString();
            }
        }

        public bool Equals(Text other)
        {
            return other != null && (Object == other.Object && ForegroundColor == other.ForegroundColor && BackgroundColor == other.BackgroundColor);
        }
    }

}
