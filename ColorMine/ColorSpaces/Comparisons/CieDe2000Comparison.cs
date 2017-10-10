using System;

namespace ColorMine.ColorSpaces.Comparisons
{
    /// <summary>
    /// Implements the DE2000 method of delta-e: http://en.wikipedia.org/wiki/Color_difference#CIEDE2000
    /// Correct implementation provided courtesy of Jonathan Hofinger, jaytar42
    /// </summary>
    public class CieDe2000Comparison : IColorSpaceComparison
    {
        /// <summary>
        /// Calculates the DE2000 delta-e value: http://en.wikipedia.org/wiki/Color_difference#CIEDE2000
        /// Correct implementation provided courtesy of Jonathan Hofinger, jaytar42
        /// </summary>
        public double Compare(IColorSpace c1, IColorSpace c2)
        {
            //Set weighting factors to 1
            double k_L = 1.0d;
            double k_C = 1.0d;
            double k_H = 1.0d;


            //Change Color Space to L*a*b:
            Lab lab1 = c1.To<Lab>();
            Lab lab2 = c2.To<Lab>();

            //Calculate Cprime1, Cprime2, Cabbar
            double c_star_1_ab = Math.Sqrt(lab1.A * lab1.A + lab1.B * lab1.B);
            double c_star_2_ab = Math.Sqrt(lab2.A * lab2.A + lab2.B * lab2.B);
            double c_star_average_ab = (c_star_1_ab + c_star_2_ab) / 2;

            double c_star_average_ab_pot7 = c_star_average_ab * c_star_average_ab * c_star_average_ab;
            c_star_average_ab_pot7 *= c_star_average_ab_pot7 * c_star_average_ab;

            double G = 0.5d * (1 - Math.Sqrt(c_star_average_ab_pot7 / (c_star_average_ab_pot7 + 6103515625))); //25^7
            double a1_prime = (1 + G) * lab1.A;
            double a2_prime = (1 + G) * lab2.A;

            double C_prime_1 = Math.Sqrt(a1_prime * a1_prime + lab1.B * lab1.B);
            double C_prime_2 = Math.Sqrt(a2_prime * a2_prime + lab2.B * lab2.B);
            //Angles in Degree.
            double h_prime_1 = ((Math.Atan2(lab1.B, a1_prime) * 180d / Math.PI) + 360) % 360d;
            double h_prime_2 = ((Math.Atan2(lab2.B, a2_prime) * 180d / Math.PI) + 360) % 360d;

            double delta_L_prime = lab2.L - lab1.L;
            double delta_C_prime = C_prime_2 - C_prime_1;

            double h_bar = Math.Abs(h_prime_1 - h_prime_2);
            double delta_h_prime;
            if (C_prime_1 * C_prime_2 == 0) delta_h_prime = 0;
            else
            {
                if (h_bar <= 180d)
                {
                    delta_h_prime = h_prime_2 - h_prime_1;
                }
                else if (h_bar > 180d && h_prime_2 <= h_prime_1)
                {
                    delta_h_prime = h_prime_2 - h_prime_1 + 360.0;
                }
                else
                {
                    delta_h_prime = h_prime_2 - h_prime_1 - 360.0;
                }
            }
            double delta_H_prime = 2 * Math.Sqrt(C_prime_1 * C_prime_2) * Math.Sin(delta_h_prime * Math.PI / 360d);

            // Calculate CIEDE2000
            double L_prime_average = (lab1.L + lab2.L) / 2d;
            double C_prime_average = (C_prime_1 + C_prime_2) / 2d;

            //Calculate h_prime_average

            double h_prime_average;
            if (C_prime_1 * C_prime_2 == 0) h_prime_average = 0;
            else
            {
                if (h_bar <= 180d)
                {
                    h_prime_average = (h_prime_1 + h_prime_2) / 2;
                }
                else if (h_bar > 180d && (h_prime_1 + h_prime_2) < 360d)
                {
                    h_prime_average = (h_prime_1 + h_prime_2 + 360d) / 2;
                }
                else
                {
                    h_prime_average = (h_prime_1 + h_prime_2 - 360d) / 2;
                }
            }
            double L_prime_average_minus_50_square = (L_prime_average - 50);
            L_prime_average_minus_50_square *= L_prime_average_minus_50_square;

            double S_L = 1 + ((.015d * L_prime_average_minus_50_square) / Math.Sqrt(20 + L_prime_average_minus_50_square));
            double S_C = 1 + .045d * C_prime_average;
            double T = 1
                - .17 * Math.Cos(DegToRad(h_prime_average - 30))
                + .24 * Math.Cos(DegToRad(h_prime_average * 2))
                + .32 * Math.Cos(DegToRad(h_prime_average * 3 + 6))
                - .2 * Math.Cos(DegToRad(h_prime_average * 4 - 63));
            double S_H = 1 + .015 * T * C_prime_average;
            double h_prime_average_minus_275_div_25_square = (h_prime_average - 275) / (25);
            h_prime_average_minus_275_div_25_square *= h_prime_average_minus_275_div_25_square;
            double delta_theta = 30 * Math.Exp(-h_prime_average_minus_275_div_25_square);

            double C_prime_average_pot_7 = C_prime_average * C_prime_average * C_prime_average;
            C_prime_average_pot_7 *= C_prime_average_pot_7 * C_prime_average;
            double R_C = 2 * Math.Sqrt(C_prime_average_pot_7 / (C_prime_average_pot_7 + 6103515625));

            double R_T = -Math.Sin(DegToRad(2 * delta_theta)) * R_C;

            double delta_L_prime_div_k_L_S_L = delta_L_prime / (S_L * k_L);
            double delta_C_prime_div_k_C_S_C = delta_C_prime / (S_C * k_C);
            double delta_H_prime_div_k_H_S_H = delta_H_prime / (S_H * k_H);

            double CIEDE2000 = Math.Sqrt(
                delta_L_prime_div_k_L_S_L * delta_L_prime_div_k_L_S_L
                + delta_C_prime_div_k_C_S_C * delta_C_prime_div_k_C_S_C
                + delta_H_prime_div_k_H_S_H * delta_H_prime_div_k_H_S_H
                + R_T * delta_C_prime_div_k_C_S_C * delta_H_prime_div_k_H_S_H
                );

            return CIEDE2000;
        }
        private double DegToRad(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}