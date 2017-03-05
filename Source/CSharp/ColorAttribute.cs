using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Poshcode.Ansi
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColorAttribute : ArgumentTransformationAttribute {

        public override string ToString() {
            return "[Poshcode.Ansi.Color()]";
        }

        public override Object Transform( EngineIntrinsics engine, Object inputData) {

            var stringColor = (string)LanguagePrimitives.ConvertTo(inputData, typeof(string));

            if(stringColor.Equals("DarkYellow", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return System.Drawing.Color.Goldenrod;
            }

            if(stringColor.Equals("Green", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return System.Drawing.Color.LimeGreen;
            }

            return LanguagePrimitives.ConvertTo(inputData, typeof(System.Drawing.Color));
        }
    }
}