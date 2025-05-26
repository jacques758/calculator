using System;


namespace Calculator_App
{
    class Program
    {
        // File path for saving/loading history
        static string historyFilePath = "history.txt";
    {
        // List to store calculation history
        static System.Collections.Generic.List<string> history = new System.Collections.Generic.List<string>();
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                //main program with menu options
                Console.WriteLine("=======Calculator App==========");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Addition");
                Console.WriteLine("2 - Subtraction");
                Console.WriteLine("3 - Multiplication");
                Console.WriteLine("4 - Division");
                Console.WriteLine("5 - Apply four operations");
                Console.WriteLine("6 - Calculate average");
                Console.WriteLine("7 - Square root");
                Console.WriteLine("8 - Exponentiation");
                Console.WriteLine("9 - View history");
                Console.WriteLine("10 - Save history");
                Console.WriteLine("11 - Load history");
                Console.WriteLine("12 - Exit");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter the first number:");
                            double addend1 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double addend2 = double.Parse(Console.ReadLine());
                            double addResult = Addition(addend1, addend2);
                            Console.WriteLine("Result: " + addResult);
                            history.Add($"Addition: {addend1} + {addend2} = {addResult}");
                            break;
                        case 2:
                            Console.WriteLine("Enter the first number:");
                            double minuend = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double subtrahend = double.Parse(Console.ReadLine());
                            double subResult = Subtraction(minuend, subtrahend);
                            Console.WriteLine("Result: " + subResult);
                            history.Add($"Subtraction: {minuend} - {subtrahend} = {subResult}");
                            break;
                        case 3:
                            Console.WriteLine("Enter the first number:");
                            double factor1 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double factor2 = double.Parse(Console.ReadLine());
                            double mulResult = Multiplication(factor1, factor2);
                            Console.WriteLine("Result: " + mulResult);
                            history.Add($"Multiplication: {factor1} * {factor2} = {mulResult}");
                            break;
                        case 4:
                            Console.WriteLine("Enter the first number:");
                            double dividend = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double divisor = double.Parse(Console.ReadLine());
                            if (divisor == 0)
                            {
                                Console.WriteLine("Result: Infinity");
                                history.Add($"Division: {dividend} / {divisor} = Infinity");
                            }
                            else
                            {
                                double divResult = Division(dividend, divisor);
                                Console.WriteLine("Result: " + divResult);
                                history.Add($"Division: {dividend} / {divisor} = {divResult}");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Enter the first number:");
                            double num1 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double num2 = double.Parse(Console.ReadLine());
                            ApplyFourOperations(num1, num2);
                            break;
                        case 6:
                            Console.WriteLine("Enter the first number:");
                            double num3 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double num4 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the third number:");
                            double num5 = double.Parse(Console.ReadLine());
                            double avg = CalculateAverage(num3, num4, num5);
                            Console.WriteLine("Average: " + avg);
                            history.Add($"Average: ({num3} + {num4} + {num5}) / 3 = {avg}");
                            break;
                        case 7:
                            Console.WriteLine("Enter the number to find the square root:");
                            double sqrtInput = double.Parse(Console.ReadLine());
                            if (sqrtInput < 0)
                            {
                                Console.WriteLine("Result: NaN (cannot take square root of a negative number)");
                                history.Add($"Square root: sqrt({sqrtInput}) = NaN");
                            }
                            else
                            {
                                double sqrtResult = Math.Sqrt(sqrtInput);
                                Console.WriteLine("Result: " + sqrtResult);
                                history.Add($"Square root: sqrt({sqrtInput}) = {sqrtResult}");
                            }
                            break;
                        case 8:
                            Console.WriteLine("Enter the base number:");
                            double baseNum = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the exponent:");
                            double exponent = double.Parse(Console.ReadLine());
                            double expResult = Math.Pow(baseNum, exponent);
                            Console.WriteLine("Result: " + expResult);
                            history.Add($"Exponentiation: {baseNum} ^ {exponent} = {expResult}");
                            break;
                        case 9:
                            Console.WriteLine("======= Calculation History =======");
                            if (history.Count == 0)
                            {
                                Console.WriteLine("No calculations yet.");
                            }
                            else
                            {
                                foreach (var entry in history)
                                {
                                    Console.WriteLine(entry);
                                }
                            }
                            Console.WriteLine("==================================");
                            break;
                        case 10:
                            SaveHistory();
                            break;
                        case 11:
                            LoadHistory();
                            break;
                        case 12:
                            Console.WriteLine("Thank you for using my calculator application!");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid selection. Please enter a valid value.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please enter a valid value.");
                }
                Console.WriteLine();
            }
        }

        // Save history to file
        static void SaveHistory()
        {
            try
            {
                System.IO.File.WriteAllLines(historyFilePath, history);
                Console.WriteLine($"History saved to {historyFilePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving history: {ex.Message}");
            }
        }

        // Load history from file
        static void LoadHistory()
        {
            try
            {
                if (System.IO.File.Exists(historyFilePath))
                {
                    history = new System.Collections.Generic.List<string>(System.IO.File.ReadAllLines(historyFilePath));
                    Console.WriteLine($"History loaded from {historyFilePath}.");
                }
                else
                {
                    Console.WriteLine($"No history file found at {historyFilePath}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading history: {ex.Message}");
            }
        }
                        default:
                            Console.WriteLine("Invalid selection. Please enter a valid value.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please enter a valid value.");
                }
                Console.WriteLine();
            }
        }
        //functions for each mathematical operation.
        static double Addition(double addend1, double addend2)
        {
            return addend1 + addend2;
        }

        static double Subtraction(double minuend, double subtrahend)
        {
            return minuend - subtrahend;
        }

        static double Multiplication(double factor1, double factor2)
        {
            return factor1 * factor2;
        }

        static double Division(double dividend, double divisor)
        {
            return dividend / divisor;
        }

        static void ApplyFourOperations(double num1, double num2)
        {
            double add = Addition(num1, num2);
            double sub = Subtraction(num1, num2);
            double mul = Multiplication(num1, num2);
            double div = Division(num1, num2);
            Console.WriteLine("Addition: " + add);
            Console.WriteLine("Subtraction: " + sub);
            Console.WriteLine("Multiplication: " + mul);
            if (double.IsInfinity(div))
            {
                Console.WriteLine("Division: Infinity");
                history.Add($"ApplyFourOperations: {num1} + {num2} = {add}, {num1} - {num2} = {sub}, {num1} * {num2} = {mul}, {num1} / {num2} = Infinity");
            }
            else
            {
                Console.WriteLine("Division: " + div);
                history.Add($"ApplyFourOperations: {num1} + {num2} = {add}, {num1} - {num2} = {sub}, {num1} * {num2} = {mul}, {num1} / {num2} = {div}");
            }
        }
        static double CalculateAverage(double num1, double num2, double num3)
        {
            return (num1 + num2 + num3) / 3;
        }
    }

}
