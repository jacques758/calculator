using System;

namespace Calculator_App.Operations
{
    /// <summary>
    /// Contains basic arithmetic operations: addition, subtraction, multiplication, and division
    /// </summary>
    public static class BasicOperations
    {        /// <summary>
        /// Performs addition of two numbers
        /// </summary>
        /// <param name="addend1">First number to add</param>
        /// <param name="addend2">Second number to add</param>
        /// <returns>Sum of the two numbers</returns>
        public static double Addition(double addend1, double addend2)
        {
            try
            {
                if (double.IsNaN(addend1) || double.IsNaN(addend2))
                    return double.NaN;
                
                return addend1 + addend2;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }        /// <summary>
        /// Performs subtraction of two numbers
        /// </summary>
        /// <param name="minuend">Number to subtract from</param>
        /// <param name="subtrahend">Number to subtract</param>
        /// <returns>Difference of the two numbers</returns>
        public static double Subtraction(double minuend, double subtrahend)
        {
            try
            {
                if (double.IsNaN(minuend) || double.IsNaN(subtrahend))
                    return double.NaN;
                
                return minuend - subtrahend;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }        /// <summary>
        /// Performs multiplication of two numbers
        /// </summary>
        /// <param name="factor1">First number to multiply</param>
        /// <param name="factor2">Second number to multiply</param>
        /// <returns>Product of the two numbers</returns>
        public static double Multiplication(double factor1, double factor2)
        {
            try
            {
                if (double.IsNaN(factor1) || double.IsNaN(factor2))
                    return double.NaN;
                
                return factor1 * factor2;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }        /// <summary>
        /// Performs division of two numbers
        /// </summary>
        /// <param name="dividend">Number to be divided</param>
        /// <param name="divisor">Number to divide by</param>
        /// <returns>Quotient of the two numbers</returns>
        public static double Division(double dividend, double divisor)
        {
            try
            {
                if (double.IsNaN(dividend) || double.IsNaN(divisor))
                    return double.NaN;
                
                // Division by zero handling is done at the UI level
                // Here we just perform the mathematical operation
                return dividend / divisor;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }
    }
}
