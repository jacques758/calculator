using System;

namespace Calculator_App.Operations
{
    /// <summary>
    /// Contains scientific mathematical operations
    /// </summary>
    public static class ScientificOperations
    {
        /// <summary>
        /// Calculates the sine of an angle in radians
        /// </summary>
        /// <param name="angle">Angle in radians</param>
        /// <returns>Sine of the angle</returns>
        public static double Sin(double angle)
        {
            try
            {
                if (double.IsNaN(angle) || double.IsInfinity(angle))
                    return double.NaN;
                
                return Math.Sin(angle);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates the cosine of an angle in radians
        /// </summary>
        /// <param name="angle">Angle in radians</param>
        /// <returns>Cosine of the angle</returns>
        public static double Cos(double angle)
        {
            try
            {
                if (double.IsNaN(angle) || double.IsInfinity(angle))
                    return double.NaN;
                
                return Math.Cos(angle);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates the tangent of an angle in radians
        /// </summary>
        /// <param name="angle">Angle in radians</param>
        /// <returns>Tangent of the angle</returns>
        public static double Tan(double angle)
        {
            try
            {
                if (double.IsNaN(angle) || double.IsInfinity(angle))
                    return double.NaN;
                
                return Math.Tan(angle);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates the natural logarithm of a number
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>Natural logarithm of the number</returns>
        public static double Ln(double number)
        {
            try
            {
                if (double.IsNaN(number) || number < 0)
                    return double.NaN;
                
                if (number == 0)
                    return double.NegativeInfinity;
                
                return Math.Log(number);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates the base-10 logarithm of a number
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>Base-10 logarithm of the number</returns>
        public static double Log10(double number)
        {
            try
            {
                if (double.IsNaN(number) || number < 0)
                    return double.NaN;
                
                if (number == 0)
                    return double.NegativeInfinity;
                
                return Math.Log10(number);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates e raised to the power of x
        /// </summary>
        /// <param name="x">The exponent</param>
        /// <returns>e^x</returns>
        public static double Exp(double x)
        {
            try
            {
                if (double.IsNaN(x))
                    return double.NaN;
                
                return Math.Exp(x);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="degrees">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static double DegreesToRadians(double degrees)
        {
            try
            {
                if (double.IsNaN(degrees))
                    return double.NaN;
                
                return degrees * Math.PI / 180.0;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="radians">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static double RadiansToDegrees(double radians)
        {
            try
            {
                if (double.IsNaN(radians))
                    return double.NaN;
                
                return radians * 180.0 / Math.PI;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates the factorial of a non-negative integer
        /// </summary>
        /// <param name="n">The number to calculate factorial for</param>
        /// <returns>Factorial of n</returns>
        public static double Factorial(int n)
        {
            try
            {
                if (n < 0)
                    return double.NaN;
                
                if (n == 0 || n == 1)
                    return 1;
                
                double result = 1;
                for (int i = 2; i <= n; i++)
                {
                    result *= i;
                    if (double.IsInfinity(result))
                        return double.PositiveInfinity;
                }
                
                return result;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Calculates the absolute value of a number
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>Absolute value of the number</returns>
        public static double Abs(double number)
        {
            try
            {
                if (double.IsNaN(number))
                    return double.NaN;
                
                return Math.Abs(number);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }
    }
}
