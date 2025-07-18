using System;

namespace Calculator_App.UI
{
    /// <summary>
    /// Handles user interface operations and input/output formatting
    /// </summary>
    public static class UIManager
    {
        /// <summary>
        /// Clears the console and displays the main menu
        /// </summary>
        public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("         CALCULATOR APP");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please choose an option by entering the number:");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Addition");
            Console.WriteLine(" 2. Subtraction");
            Console.WriteLine(" 3. Multiplication");
            Console.WriteLine(" 4. Division");
            Console.WriteLine(" 5. Apply all four operations");
            Console.WriteLine(" 6. Calculate average");
            Console.WriteLine(" 7. Square root");
            Console.WriteLine(" 8. Exponentiation");
            Console.WriteLine(" 9. View history");
            Console.WriteLine("10. Save history");
            Console.WriteLine("11. Load history");
            Console.WriteLine("12. Exit");
            Console.ResetColor();
        }        /// <summary>
        /// Prompts the user for their menu choice
        /// </summary>
        /// <returns>The user's choice as an integer, or -1 if invalid</returns>
        public static int GetUserChoice()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nYour choice: ");
                Console.ResetColor();
                
                string? input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    return -1;
                }
                
                if (int.TryParse(input.Trim(), out int choice))
                {
                    return choice;
                }
                
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }/// <summary>
        /// Prompts the user to enter a number with a custom message
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <returns>The number entered by the user</returns>
        public static double GetNumberInput(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(prompt);
                    Console.ResetColor();
                    
                    string? input = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        DisplayError("Input cannot be empty. Please enter a valid number.");
                        continue;
                    }
                    
                    if (double.TryParse(input.Trim(), out double result))
                    {
                        if (double.IsInfinity(result))
                        {
                            DisplayError("The number is too large. Please enter a smaller number.");
                            continue;
                        }
                        
                        if (double.IsNaN(result))
                        {
                            DisplayError("Invalid number format. Please enter a valid number.");
                            continue;
                        }
                        
                        return result;
                    }
                    else
                    {
                        DisplayError("Invalid input. Please enter a valid number (e.g., 123, -45.67, 3.14).");
                    }
                }
                catch (Exception ex)
                {
                    DisplayError($"An unexpected error occurred: {ex.Message}. Please try again.");
                }
            }
        }

        /// <summary>
        /// Displays a result with formatting
        /// </summary>
        /// <param name="result">The result to display</param>
        public static void DisplayResult(double result)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Result: {result}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a result with custom label and formatting
        /// </summary>
        /// <param name="label">The label for the result</param>
        /// <param name="result">The result to display</param>
        public static void DisplayResult(string label, double result)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{label}: {result}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an error message
        /// </summary>
        /// <param name="message">The error message to display</param>
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a success message
        /// </summary>
        /// <param name="message">The success message to display</param>
        public static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an invalid selection message
        /// </summary>
        public static void DisplayInvalidSelection()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid selection. Please enter a valid value.");
            Console.ResetColor();
        }

        /// <summary>
        /// Pauses execution and waits for user input to continue
        /// </summary>
        public static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a goodbye message
        /// </summary>
        public static void DisplayGoodbye()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thank you for using my calculator application!");
            Console.ResetColor();
        }

        /// <summary>
        /// Gets a positive number for operations that require positive inputs (like square root)
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <returns>A positive number entered by the user</returns>
        public static double GetPositiveNumberInput(string prompt)
        {
            while (true)
            {
                double number = GetNumberInput(prompt);
                
                if (number < 0)
                {
                    DisplayError("This operation requires a non-negative number. Please enter a positive number or zero.");
                    continue;
                }
                
                return number;
            }
        }

        /// <summary>
        /// Gets a non-zero number for operations that require non-zero inputs (like division)
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <returns>A non-zero number entered by the user</returns>
        public static double GetNonZeroNumberInput(string prompt)
        {
            while (true)
            {
                double number = GetNumberInput(prompt);
                
                if (number == 0)
                {
                    DisplayError("This operation requires a non-zero number. Please enter a number other than zero.");
                    continue;
                }
                
                return number;
            }
        }

        /// <summary>
        /// Gets a valid integer within a specified range
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <param name="min">Minimum valid value</param>
        /// <param name="max">Maximum valid value</param>
        /// <returns>A valid integer within the specified range</returns>
        public static int GetValidIntegerInput(string prompt, int min, int max)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(prompt);
                    Console.ResetColor();
                    
                    string? input = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        DisplayError("Input cannot be empty. Please enter a valid integer.");
                        continue;
                    }
                    
                    if (int.TryParse(input.Trim(), out int result))
                    {
                        if (result < min || result > max)
                        {
                            DisplayError($"Please enter a number between {min} and {max}.");
                            continue;
                        }
                        
                        return result;
                    }
                    else
                    {
                        DisplayError("Invalid input. Please enter a valid integer.");
                    }
                }
                catch (Exception ex)
                {
                    DisplayError($"An unexpected error occurred: {ex.Message}. Please try again.");
                }
            }
        }

        /// <summary>
        /// Displays a warning message
        /// </summary>
        /// <param name="message">The warning message to display</param>
        public static void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Warning: {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the mode selection menu
        /// </summary>
        public static void DisplayModeSelectionMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("         CALCULATOR APP");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please select a calculator mode:");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Basic Mode");
            Console.WriteLine(" 2. Scientific Mode");
            Console.WriteLine(" 3. Programmer Mode");
            Console.WriteLine(" 4. Exit");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the scientific mode menu
        /// </summary>
        public static void DisplayScientificMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("      SCIENTIFIC CALCULATOR");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please choose an operation:");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Basic Operations");
            Console.WriteLine(" 2. Trigonometric Functions");
            Console.WriteLine(" 3. Logarithmic Functions");
            Console.WriteLine(" 4. Exponential Functions");
            Console.WriteLine(" 5. Other Functions");
            Console.WriteLine(" 6. View History");
            Console.WriteLine(" 7. Save History");
            Console.WriteLine(" 8. Load History");
            Console.WriteLine(" 9. Back to Mode Selection");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the programmer mode menu
        /// </summary>
        public static void DisplayProgrammerMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("      PROGRAMMER CALCULATOR");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please choose an operation:");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Number Base Conversion");
            Console.WriteLine(" 2. Bitwise Operations");
            Console.WriteLine(" 3. Bit Shifting");
            Console.WriteLine(" 4. Other Operations");
            Console.WriteLine(" 5. View History");
            Console.WriteLine(" 6. Save History");
            Console.WriteLine(" 7. Load History");
            Console.WriteLine(" 8. Back to Mode Selection");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the trigonometric functions submenu
        /// </summary>
        public static void DisplayTrigonometricMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Trigonometric Functions");
            Console.WriteLine("=======================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Sin (sine)");
            Console.WriteLine(" 2. Cos (cosine)");
            Console.WriteLine(" 3. Tan (tangent)");
            Console.WriteLine(" 4. Degrees to Radians");
            Console.WriteLine(" 5. Radians to Degrees");
            Console.WriteLine(" 6. Back to Scientific Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the logarithmic functions submenu
        /// </summary>
        public static void DisplayLogarithmicMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Logarithmic Functions");
            Console.WriteLine("=====================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Natural Logarithm (ln)");
            Console.WriteLine(" 2. Base-10 Logarithm (log)");
            Console.WriteLine(" 3. Back to Scientific Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the exponential functions submenu
        /// </summary>
        public static void DisplayExponentialMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Exponential Functions");
            Console.WriteLine("=====================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. e^x (exponential)");
            Console.WriteLine(" 2. x^y (power)");
            Console.WriteLine(" 3. Back to Scientific Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the other scientific functions submenu
        /// </summary>
        public static void DisplayOtherScientificMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Other Scientific Functions");
            Console.WriteLine("==========================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Factorial (n!)");
            Console.WriteLine(" 2. Absolute Value (|x|)");
            Console.WriteLine(" 3. Square Root");
            Console.WriteLine(" 4. Back to Scientific Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the number base conversion submenu
        /// </summary>
        public static void DisplayBaseConversionMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Number Base Conversion");
            Console.WriteLine("======================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Decimal to Binary");
            Console.WriteLine(" 2. Decimal to Hexadecimal");
            Console.WriteLine(" 3. Decimal to Octal");
            Console.WriteLine(" 4. Binary to Decimal");
            Console.WriteLine(" 5. Hexadecimal to Decimal");
            Console.WriteLine(" 6. Octal to Decimal");
            Console.WriteLine(" 7. Back to Programmer Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the bitwise operations submenu
        /// </summary>
        public static void DisplayBitwiseMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Bitwise Operations");
            Console.WriteLine("==================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Bitwise AND (&)");
            Console.WriteLine(" 2. Bitwise OR (|)");
            Console.WriteLine(" 3. Bitwise XOR (^)");
            Console.WriteLine(" 4. Bitwise NOT (~)");
            Console.WriteLine(" 5. Back to Programmer Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the bit shifting submenu
        /// </summary>
        public static void DisplayBitShiftingMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Bit Shifting Operations");
            Console.WriteLine("=======================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Left Shift (<<)");
            Console.WriteLine(" 2. Right Shift (>>)");
            Console.WriteLine(" 3. Back to Programmer Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the other programmer operations submenu
        /// </summary>
        public static void DisplayOtherProgrammerMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Other Programmer Operations");
            Console.WriteLine("===========================");
            Console.ResetColor();
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 1. Population Count (bit count)");
            Console.WriteLine(" 2. Back to Programmer Menu");
            Console.ResetColor();
        }

        /// <summary>
        /// Gets string input from the user with validation
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <returns>The string entered by the user</returns>
        public static string GetStringInput(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(prompt);
            Console.ResetColor();
            
            string? input = Console.ReadLine();
            return input?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Gets integer input from the user
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <returns>The integer entered by the user</returns>
        public static int GetIntegerInput(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(prompt);
                    Console.ResetColor();
                    
                    string? input = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        DisplayError("Input cannot be empty. Please enter a valid integer.");
                        continue;
                    }
                    
                    if (int.TryParse(input.Trim(), out int result))
                    {
                        return result;
                    }
                    
                    DisplayError("Invalid input. Please enter a valid integer.");
                }
                catch (Exception ex)
                {
                    DisplayError($"Error reading input: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Gets long integer input from the user
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <returns>The long integer entered by the user</returns>
        public static long GetLongInput(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(prompt);
                    Console.ResetColor();
                    
                    string? input = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        DisplayError("Input cannot be empty. Please enter a valid number.");
                        continue;
                    }
                    
                    if (long.TryParse(input.Trim(), out long result))
                    {
                        return result;
                    }
                    
                    DisplayError("Invalid input. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    DisplayError($"Error reading input: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Displays multiple number formats for programmer mode
        /// </summary>
        /// <param name="decimal">Decimal value</param>
        /// <param name="binary">Binary representation</param>
        /// <param name="hex">Hexadecimal representation</param>
        /// <param name="octal">Octal representation</param>
        public static void DisplayMultipleFormats(long decimalValue, string binary, string hex, string octal)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Result in multiple formats:");
            Console.WriteLine("===========================");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Decimal: {decimalValue}");
            Console.WriteLine($"Binary:  {binary}");
            Console.WriteLine($"Hex:     {hex}");
            Console.WriteLine($"Octal:   {octal}");
            Console.ResetColor();
        }
    }
}
