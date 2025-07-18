using System;
using Calculator_App.Operations;

namespace Calculator_App.Operations
{
    /// <summary>
    /// Contains composite operations that use multiple basic operations
    /// </summary>
    public static class CompositeOperations
    {        /// <summary>
        /// Applies all four basic operations (addition, subtraction, multiplication, division) to two numbers
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns>Tuple containing results of all four operations</returns>
        public static (double addition, double subtraction, double multiplication, double division) ApplyFourOperations(double num1, double num2)
        {
            try
            {
                double add = BasicOperations.Addition(num1, num2);
                double sub = BasicOperations.Subtraction(num1, num2);
                double mul = BasicOperations.Multiplication(num1, num2);
                double div = BasicOperations.Division(num1, num2);

                return (add, sub, mul, div);
            }
            catch (Exception)
            {
                return (double.NaN, double.NaN, double.NaN, double.NaN);
            }
        }        /// <summary>
        /// Displays the results of all four basic operations with formatting
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        public static void DisplayFourOperations(double num1, double num2)
        {
            try
            {
                var results = ApplyFourOperations(num1, num2);

                Console.ForegroundColor = ConsoleColor.Magenta;
                
                // Display addition result
                if (double.IsNaN(results.addition))
                    Console.WriteLine("Addition: NaN (invalid operation)");
                else if (double.IsInfinity(results.addition))
                    Console.WriteLine("Addition: Infinity");
                else
                    Console.WriteLine($"Addition: {results.addition}");
                
                // Display subtraction result
                if (double.IsNaN(results.subtraction))
                    Console.WriteLine("Subtraction: NaN (invalid operation)");
                else if (double.IsInfinity(results.subtraction))
                    Console.WriteLine("Subtraction: Infinity");
                else
                    Console.WriteLine($"Subtraction: {results.subtraction}");
                
                // Display multiplication result
                if (double.IsNaN(results.multiplication))
                    Console.WriteLine("Multiplication: NaN (invalid operation)");
                else if (double.IsInfinity(results.multiplication))
                    Console.WriteLine("Multiplication: Infinity");
                else
                    Console.WriteLine($"Multiplication: {results.multiplication}");
                
                // Display division result
                if (double.IsNaN(results.division))
                    Console.WriteLine("Division: NaN (invalid operation)");
                else if (double.IsInfinity(results.division))
                    Console.WriteLine("Division: Infinity");
                else
                    Console.WriteLine($"Division: {results.division}");
                
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error displaying four operations: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Creates a history entry for the four operations
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns>Formatted history string</returns>
        public static string CreateFourOperationsHistoryEntry(double num1, double num2)
        {
            var results = ApplyFourOperations(num1, num2);
            
            string divisionResult = double.IsInfinity(results.division) ? "Infinity" : results.division.ToString();
            
            return $"ApplyFourOperations: {num1} + {num2} = {results.addition}, {num1} - {num2} = {results.subtraction}, {num1} * {num2} = {results.multiplication}, {num1} / {num2} = {divisionResult}";
        }
    }
}
