using System;
using System.Management.Automation;

/// <summary>
/// An argument transformation attribute for treating empty strings as null when casting an empty string won't work
/// </summary>
public class EmptyStringAsNullAttribute : ArgumentTransformationAttribute
{
    public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
    {
        if (inputData is string && ((string)inputData).Length == 0)
        {
            return null;
        }
        else
        {
            return inputData;
        }
    }
}