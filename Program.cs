using System;


namespace Calculator_App
{
    class Program
    {
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
                Console.WriteLine("7 - Exit");

                //case statements for each menu program
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
                            Console.WriteLine("Result: " + Addition(addend1, addend2));
                            break;
                        case 2:
                            Console.WriteLine("Enter the first number:");
                            double minuend = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double subtrahend = double.Parse(Console.ReadLine());
                            Console.WriteLine("Result: " + Subtraction(minuend, subtrahend));
                            break;
                        case 3:
                            Console.WriteLine("Enter the first number:");
                            double factor1 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double factor2 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Result: " + Multiplication(factor1, factor2));
                            break;
                        case 4:
                            Console.WriteLine("Enter the first number:");
                            double dividend = double.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the second number:");
                            double divisor = double.Parse(Console.ReadLine());
                            if (divisor == 0)
                            {
                                Console.WriteLine("Result: Infinity");
                            }
                            else
                            {
                                Console.WriteLine("Result: " + Division(dividend, divisor));
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
                            Console.WriteLine("Average: " + CalculateAverage(num3, num4, num5));
                            break;
                        case 7:
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
            Console.WriteLine("Addition: " + Addition(num1, num2));
            Console.WriteLine("Subtraction:" + Subtraction(num1, num2));
            Console.WriteLine("Multiplication: " + Multiplication(num1, num2));
            double result = Division(num1, num2);
            if (double.IsInfinity(result))
            {
                Console.WriteLine("Division: Infinity");
            }
            else
            {
                Console.WriteLine("Division: " + result);
            }

        }
        static double CalculateAverage(double num1, double num2, double num3)
        {
            return (num1 + num2 + num3) / 3;
        }
    }

}
