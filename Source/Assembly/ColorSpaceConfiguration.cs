using PoshCode.Pansies.ColorSpaces;
using PoshCode.Pansies.ColorSpaces.Conversions;

namespace PoshCode.Pansies
{
    /// <summary>
    /// Provides helpers for configuring color space defaults exposed to PowerShell consumers.
    /// </summary>
    public static class ColorSpaceConfiguration
    {
        public static IXyz GetWhiteReference()
        {
            return XyzConverter.GetWhiteReference();
        }

        public static void SetWhiteReference(IXyz whiteReference)
        {
            XyzConverter.SetWhiteReference(whiteReference);
        }

        public static void ResetWhiteReference()
        {
            XyzConverter.ResetWhiteReference();
        }
    }
}
