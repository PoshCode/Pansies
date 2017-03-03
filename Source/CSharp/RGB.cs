using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;

namespace Poshcode.Ansi
{
    public static class RGB
    {
        public static string GetCode(this Color? color, bool forBackground = false)
        {
            string colorCode = color == null ? "Default" : color.ToString();

            return string.Format(forBackground ? backgroundRgb : foregroundRgb, color.Value.R, color.Value.G, color.Value.B);
        }

        //static string reset           = "\u001B[27m"; // Use normal colors
        //static string foregroundXterm = "\u001B[38;5;{0}m"; // Set xterm text color, n is color index from 0 to 255
        static string foregroundRgb   = "\u001B[38;2;{0};{1};{2}m"; // Set xterm 24-bit text color, r, g, b are from 0 to 255
        static string foregroundReset = "\u001B[39m"; // Reset text color to defaults
        //static string backgroundXterm = "\u001B[48;5;{0}m"; // Set xterm background color, n is color index from 0 to 255
        static string backgroundRgb   = "\u001B[48;2;{0};{1};{2}m"; // Set xterm 24-bit background color, r, g, b are from 0 to 255
        static string backgroundReset = "\u001B[49m"; // Reset background color to defaults

        public static string DefaultForeground = "\u001B[97m";
        public static string DefaultBackground = "\u001B[104m";

        public static string WriteAnsi(Color? foreground, Color? background, string value, bool clear = false)
        {
            var output = new StringBuilder();

            output.Append(background.GetCode(true));
            output.Append(foreground.GetCode());

            output.Append(value);
            if (clear)
            {
                output.Append(backgroundReset);
                output.Append(foregroundReset);
            }
            return output.ToString();
        }

        public static string GetString(object @object)
        {
            var scriptBlock = @object as ScriptBlock;
            return (string)LanguagePrimitives.ConvertTo(scriptBlock != null ? scriptBlock.Invoke() : @object, typeof(string));
        }

        static RGB()
        {
            Console.ResetColor();
            DefaultForeground = ConsoleColor.Foreground["Default"];
            DefaultBackground = ConsoleColor.Background["Default"];
        }

        public struct EscapeCodes
        {
            public static readonly string Esc = "\u001B[";
            public static readonly string Clear = "\u001B[0m";
            public static readonly string PromptLocation = "\u001B[s";
            public static readonly string Recall = "\u001B[u";
        };
    }
}