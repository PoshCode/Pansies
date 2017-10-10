using System;

namespace ColorMine.ColorSpaces.Comparisons
{
    /// <summary>
    /// Implements the Cie94 method of delta-e: http://en.wikipedia.org/wiki/Color_difference#CIE94
    /// </summary>
    public class Cie94Comparison : IColorSpaceComparison
    {
        /// <summary>
        /// Application type defines constants used in the Cie94 comparison
        /// </summary>
        public enum Application
        {
            GraphicArts,
            Textiles
        }


        internal ApplicationConstants Constants { get; private set; }

        /// <summary>
        /// Create new Cie94Comparison. Defaults to GraphicArts application type.
        /// </summary>
        public Cie94Comparison()
        {
            Constants = new ApplicationConstants(Application.GraphicArts);
        }

        /// <summary>
        /// Create new Cie94Comparison for specific application type.
        /// </summary>
        /// <param name="application"></param>
        public Cie94Comparison(Application application)
        {
            Constants = new ApplicationConstants(application);
        }

        /// <summary>
        /// Compare colors using the Cie94 algorithm. The first color (a) will be used as the reference color.
        /// </summary>
        /// <param name="a">Reference color</param>
        /// <param name="b">Comparison color</param>
        /// <returns></returns>
        public double Compare(IColorSpace a, IColorSpace b)
        {
            var labA = a.To<Lab>();
            var labB = b.To<Lab>();

            var deltaL = labA.L - labB.L;
            var deltaA = labA.A - labB.A;
            var deltaB = labA.B - labB.B;

            var c1 = Math.Sqrt(labA.A * labA.A + labA.B * labA.B);
            var c2 = Math.Sqrt(labB.A * labB.A + labB.B * labB.B);
            var deltaC = c1 - c2;

            var deltaH = deltaA * deltaA + deltaB * deltaB - deltaC * deltaC;
            deltaH = deltaH < 0 ? 0 : Math.Sqrt(deltaH);

            const double sl = 1.0;
            const double kc = 1.0;
            const double kh = 1.0;

            var sc = 1.0 + Constants.K1 * c1;
            var sh = 1.0 + Constants.K2 * c1;

            var deltaLKlsl = deltaL / (Constants.Kl * sl);
            var deltaCkcsc = deltaC / (kc * sc);
            var deltaHkhsh = deltaH / (kh * sh);
            var i = deltaLKlsl * deltaLKlsl + deltaCkcsc * deltaCkcsc + deltaHkhsh * deltaHkhsh;
            return i < 0 ? 0 : Math.Sqrt(i);
        }

        internal class ApplicationConstants
        {
            internal double Kl { get; private set; }
            internal double K1 { get; private set; }
            internal double K2 { get; private set; }

            public ApplicationConstants(Application application)
            {
                switch (application)
                {
                    case Application.GraphicArts:
                        Kl = 1.0;
                        K1 = .045;
                        K2 = .015;
                        break;
                    case Application.Textiles:
                        Kl = 2.0;
                        K1 = .048;
                        K2 = .014;
                        break;
                }
            }
        }
    }
}