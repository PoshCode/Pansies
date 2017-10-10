namespace ColorMine.ColorSpaces.Conversions
{
    /// <summary>
    /// HSB is another name for HSV
    /// </summary>
    internal static class HsbConverter
    {
        internal static void ToColorSpace(IRgb color, IHsb item)
        {
            var hsv = new Hsv();
            HsvConverter.ToColorSpace(color, hsv);

            item.H = hsv.H;
            item.S = hsv.S;
            item.B = hsv.V;
        }

        internal static IRgb ToColor(IHsb item)
        {
            var hsv = new Hsv
                {
                    H = item.H,
                    S = item.S,
                    V = item.B
                };
            return HsvConverter.ToColor(hsv);
        }
    }
}
