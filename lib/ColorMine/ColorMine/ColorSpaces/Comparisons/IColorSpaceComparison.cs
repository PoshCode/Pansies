namespace ColorMine.ColorSpaces.Comparisons
{
    /// <summary>
    /// Defines how comparison methods may be called
    /// </summary>
    public interface IColorSpaceComparison
    {
        /// <summary>
        /// Returns the difference between two colors given based on the specified defined in the called class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Score based on similarity, the lower the score the closer the colors</returns>
        double Compare(IColorSpace a, IColorSpace b);
    }
}