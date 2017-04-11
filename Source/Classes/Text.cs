using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;

namespace PoshCode.Pansies
{
    public class Text : IEquatable<Text>
    {
        private string _text;
        private Regex _escapeCode = new Regex("\u001B\\P{L}+\\p{L}", RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the object. The Object will be converted to string when it's set, and this property always returns a string.
        /// </summary>
        /// <value>A string</value>
        public object Object
        {
            get
            {
                return _text;
            }
            set
            {
                _text = (string)LanguagePrimitives.ConvertTo(value, typeof(string));

                // If there's actually no output, report a negative length to ignore this block
                if (string.IsNullOrEmpty(_text))
                {
                    Length = -1;
                }
                else
                {
                    // The Length is measured without escape sequences (Esc + non-letters + any letter)
                    Length = _escapeCode.Replace(_text, "").Length;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the background color for the block
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or Sets the foreground color for the block
        /// </summary>
        public Color ForegroundColor { get; set; }

        /// <summary>
        /// Gets the length of the text representation (without ANSI escape sequences).
        /// </summary>
        public int Length { get; private set; }

        public bool Clear { get; set; } = true;
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
                    BackgroundColor = Color.ConvertFrom(values[key]);
                }
                else if ("fg".Equals(key, StringComparison.OrdinalIgnoreCase) || Regex.IsMatch("ForegroundColor", pattern, RegexOptions.IgnoreCase))
                {
                    ForegroundColor = Color.ConvertFrom(values[key]);
                }
                else if (Regex.IsMatch("text", pattern, RegexOptions.IgnoreCase) || Regex.IsMatch("Content", pattern, RegexOptions.IgnoreCase) || Regex.IsMatch("Object", pattern, RegexOptions.IgnoreCase))
                {
                    Object = values[key];
                }
                else
                {
                    throw new ArgumentException("Unknown key '" + key + "' in hashtable. Allowed values are BackgroundColor, ForegroundColor, and Object (also called Content or Text)");
                }
            }
        }
        // Make sure we can output plain text
        public Text(string text) : this()
        {
            Object = text;
        }

        // Make sure we support the default ctor
        public Text() { Length = -1; }

        public override string ToString()
        {
            return GetString(ForegroundColor, BackgroundColor, (string)Object, Clear, Entities);
        }

        public static string GetString(Color foreground, Color background, object @object, bool clear = true, bool entities = true)
        {
            var output = new StringBuilder();

            if (null != background)
            {
                output.Append(background.ToString(true));
            }
            if (null != foreground)
            {
                output.Append(foreground.ToString(false));
            }

            if (null != @object)
            {
                var scriptBlock = @object as ScriptBlock;
                var text = (string)LanguagePrimitives.ConvertTo(scriptBlock != null ? scriptBlock.Invoke() : @object, typeof(string));

                if (!string.IsNullOrEmpty(text))
                {
                    output.Append(text);
                }
            }

            if (clear)
            {
                // clear background
                output.Append("\u001B[49m");
                // clear foreground
                output.Append("\u001B[39m");
            }

            if(entities)
            {
                return System.Net.WebUtility.HtmlDecode(output.ToString());
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
