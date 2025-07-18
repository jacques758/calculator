using System;

namespace Calculator_App.Operations
{
    /// <summary>
    /// Contains advanced mathematical operations
    /// </summary>
    public static class AdvancedOperations
    {        /// <summary>
        /// Calculates the square root of a number
        /// </summary>
        /// <param name="number">The number to find the square root of</param>
        /// <returns>Square root of the number, or NaN if negative</returns>
        public static double SquareRoot(double number)
        {
            try
            {
                if (double.IsNaN(number))
                    return double.NaN;
                
                if (double.IsInfinity(number))
                    return double.PositiveInfinity;
                
                if (number < 0)
                    return double.NaN;
                
                return Math.Sqrt(number);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }        /// <summary>
        /// Raises a base number to the power of an exponent
        /// </summary>
        /// <param name="baseNumber">The base number</param>
        /// <param name="exponent">The exponent</param>
        /// <returns>Result of base raised to the power of exponent</returns>
        public static double Exponentiation(double baseNumber, double exponent)
        {
            try
            {
                if (double.IsNaN(baseNumber) || double.IsNaN(exponent))
                    return double.NaN;
                
                // Handle special cases
                if (baseNumber == 0 && exponent < 0)
                    return double.PositiveInfinity;
                
                if (baseNumber == 0 && exponent == 0)
                    return 1; // Mathematical convention: 0^0 = 1
                
                return Math.Pow(baseNumber, exponent);
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }        /// <summary>
        /// Calculates the average of three numbers
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <param name="num3">Third number</param>
        /// <returns>Average of the three numbers</returns>
        public static double CalculateAverage(double num1, double num2, double num3)
        {
            try
            {
                if (double.IsNaN(num1) || double.IsNaN(num2) || double.IsNaN(num3))
                    return double.NaN;
                
                return (num1 + num2 + num3) / 3;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }        /// <summary>
        /// Calculates the average of an array of numbers
        /// </summary>
        /// <param name="numbers">Array of numbers to average</param>
        /// <returns>Average of all numbers</returns>
        public static double CalculateAverage(params double[] numbers)
        {
            try
            {
                if (numbers == null || numbers.Length == 0)
                    return 0;

                double sum = 0;
                foreach (double num in numbers)
                {
                    if (double.IsNaN(num))
                        return double.NaN;
                    
                    sum += num;
                }
                return sum / numbers.Length;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }
    }
}
