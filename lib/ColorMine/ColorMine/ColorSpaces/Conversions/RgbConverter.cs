namespace ColorMine.ColorSpaces.Conversions
{
    internal static class RgbConverter
    {
        internal static void ToColorSpace(IRgb color, IRgb item)
        {
            item.R = color.R;
            item.G = color.G;
            item.B = color.B;
        }

        internal static IRgb ToColor(IRgb item)
        {
            return item;
        }
    }
}