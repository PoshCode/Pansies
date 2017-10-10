using ColorMine.ColorSpaces.Comparisons;

namespace ColorMine.ColorSpaces
{
    public delegate double ComparisonAlgorithm(IColorSpace a, IColorSpace b);

    /// <summary>
    /// Defines the public methods for all color spaces
    /// </summary>
    public interface IColorSpace
    {
        /// <summary>
        /// Initialize settings from an Rgb object
        /// </summary>
        /// <param name="color"></param>
        void Initialize(IRgb color);

        /// <summary>
        /// Convert the color space to Rgb, you should probably using the "To" method instead. Need to figure out a way to "hide" or otherwise remove this method from the public interface.
        /// </summary>
        /// <returns></returns>
        IRgb ToRgb();

        /// <summary>
        /// Convert any IColorSpace to any other IColorSpace.
        /// </summary>
        /// <typeparam name="T">IColorSpace type to convert to</typeparam>
        /// <returns></returns>
        T To<T>() where T : IColorSpace, new();

        /// <summary>
        /// Determine how close two IColorSpaces are to each other using a passed in algorithm
        /// </summary>
        /// <param name="compareToValue">Other IColorSpace to compare to</param>
        /// <param name="comparer">Algorithm to use for comparison</param>
        /// <returns>Distance in 3d space as double</returns>
        double Compare(IColorSpace compareToValue, IColorSpaceComparison comparer);

        /// <summary>
        /// Array of signifigant values in a consistent order. Useful for generic n-dimensional math.
        /// </summary>
        double[] Ordinals { get; set; }
    }

    /// <summary>
    /// Abstract ColorSpace class, defines the To method that converts between any IColorSpace.
    /// </summary>
    public abstract class ColorSpace : IColorSpace
    {
        public abstract void Initialize(IRgb color);
        public abstract IRgb ToRgb();
        public abstract double[] Ordinals { get; set; }

        /// <summary>
        /// Convienience method for comparing any IColorSpace
        /// </summary>
        /// <param name="compareToValue"></param>
        /// <param name="comparer"></param>
        /// <returns>Single number representing the difference between two colors</returns>
        public double Compare(IColorSpace compareToValue, IColorSpaceComparison comparer)
        {
            return comparer.Compare(this, compareToValue);
        }

        /// <summary>
        /// Convert any IColorSpace to any other IColorSpace
        /// </summary>
        /// <typeparam name="T">Must implement IColorSpace, new()</typeparam>
        /// <returns></returns>
        public T To<T>() where T : IColorSpace, new()
        {
            if (typeof(T) == GetType())
            {
                return (T)MemberwiseClone();
            }

            var newColorSpace = new T();
            newColorSpace.Initialize(ToRgb());

            return newColorSpace;
        }
    }
}