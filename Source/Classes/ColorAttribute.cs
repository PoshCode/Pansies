using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoshCode.Pansies
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColorAttribute : ArgumentTransformationAttribute {

        public override string ToString() {
            return "[PoshCode.Pansies.Color()]";
        }

        public override Object Transform(EngineIntrinsics engine, Object inputData) {
            return Color.ConvertFrom(inputData);
        }


    }
}