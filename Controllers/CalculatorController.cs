using System;
using Calculator_App.Operations;
using Calculator_App.Services;
using Calculator_App.UI;

namespace Calculator_App.Controllers
{
    /// <summary>
    /// Controls the main application flow and coordinates between different components
    /// </summary>
    public class CalculatorController
    {
        private readonly HistoryManager historyManager;
        private bool isRunning;

        /// <summary>
        /// Initializes a new instance of the CalculatorController class
        /// </summary>
        public CalculatorController()
        {
            historyManager = new HistoryManager();
            isRunning = true;
        }        /// <summary>
        /// Runs the main calculator application loop
        /// </summary>
        public void Run()
        {
            try
            {
                UIManager.DisplaySuccess("Welcome to the Calculator Application!");
                
                while (isRunning)
                {
                    try
                    {
                        UIManager.DisplayModeSelectionMenu();
                        int modeChoice = UIManager.GetUserChoice();

                        if (modeChoice == -1)
                        {
                            UIManager.DisplayInvalidSelection();
                            UIManager.Pause();
                            continue;
                        }

                        ProcessModeChoice(modeChoice);
                    }
                    catch (Exception ex)
                    {
                        UIManager.DisplayError($"An unexpected error occurred: {ex.Message}");
                        UIManager.DisplayWarning("The application will continue running. Please try your operation again.");
                        UIManager.Pause();
                    }
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"A critical error occurred: {ex.Message}");
                UIManager.DisplayError("The application will now exit.");
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes the user's menu choice and executes the corresponding operation
        /// </summary>
        /// <param name="choice">The user's menu choice</param>
        private void ProcessUserChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    PerformAddition();
                    break;
                case 2:
                    PerformSubtraction();
                    break;
                case 3:
                    PerformMultiplication();
                    break;
                case 4:
                    PerformDivision();
                    break;
                case 5:
                    PerformFourOperations();
                    break;
                case 6:
                    PerformAverageCalculation();
                    break;
                case 7:
                    PerformSquareRoot();
                    break;
                case 8:
                    PerformExponentiation();
                    break;
                case 9:
                    ViewHistory();
                    break;
                case 10:
                    SaveHistory();
                    break;
                case 11:
                    LoadHistory();
                    break;
                case 12:
                    ExitApplication();
                    break;
                default:
                    UIManager.DisplayInvalidSelection();
                    UIManager.Pause();
                    break;
            }
        }        private void PerformAddition()
        {
            try
            {
                double num1 = UIManager.GetNumberInput("Enter the first number: ");
                double num2 = UIManager.GetNumberInput("Enter the second number: ");
                double result = BasicOperations.Addition(num1, num2);
                
                if (double.IsInfinity(result))
                {
                    UIManager.DisplayWarning("Result is infinite due to overflow.");
                    UIManager.DisplayError("Result: Infinity");
                    historyManager.AddEntry($"Addition: {num1} + {num2} = Infinity (overflow)");
                }
                else if (double.IsNaN(result))
                {
                    UIManager.DisplayError("Result: NaN (invalid operation)");
                    historyManager.AddEntry($"Addition: {num1} + {num2} = NaN (invalid operation)");
                }
                else
                {
                    UIManager.DisplayResult(result);
                    historyManager.AddEntry($"Addition: {num1} + {num2} = {result}");
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing addition: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformSubtraction()
        {
            try
            {
                double num1 = UIManager.GetNumberInput("Enter the first number: ");
                double num2 = UIManager.GetNumberInput("Enter the second number: ");
                double result = BasicOperations.Subtraction(num1, num2);
                
                if (double.IsInfinity(result))
                {
                    UIManager.DisplayWarning("Result is infinite due to overflow.");
                    UIManager.DisplayError("Result: Infinity");
                    historyManager.AddEntry($"Subtraction: {num1} - {num2} = Infinity (overflow)");
                }
                else if (double.IsNaN(result))
                {
                    UIManager.DisplayError("Result: NaN (invalid operation)");
                    historyManager.AddEntry($"Subtraction: {num1} - {num2} = NaN (invalid operation)");
                }
                else
                {
                    UIManager.DisplayResult(result);
                    historyManager.AddEntry($"Subtraction: {num1} - {num2} = {result}");
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing subtraction: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformMultiplication()
        {
            try
            {
                double num1 = UIManager.GetNumberInput("Enter the first number: ");
                double num2 = UIManager.GetNumberInput("Enter the second number: ");
                double result = BasicOperations.Multiplication(num1, num2);
                
                if (double.IsInfinity(result))
                {
                    UIManager.DisplayWarning("Result is infinite due to overflow.");
                    UIManager.DisplayError("Result: Infinity");
                    historyManager.AddEntry($"Multiplication: {num1} * {num2} = Infinity (overflow)");
                }
                else if (double.IsNaN(result))
                {
                    UIManager.DisplayError("Result: NaN (invalid operation)");
                    historyManager.AddEntry($"Multiplication: {num1} * {num2} = NaN (invalid operation)");
                }
                else
                {
                    UIManager.DisplayResult(result);
                    historyManager.AddEntry($"Multiplication: {num1} * {num2} = {result}");
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing multiplication: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformDivision()
        {
            try
            {
                double num1 = UIManager.GetNumberInput("Enter the first number (dividend): ");
                double num2 = UIManager.GetNumberInput("Enter the second number (divisor): ");
                
                if (num2 == 0)
                {
                    UIManager.DisplayWarning("Division by zero detected.");
                    UIManager.DisplayError("Result: Infinity");
                    historyManager.AddEntry($"Division: {num1} / {num2} = Infinity (division by zero)");
                }
                else
                {
                    double result = BasicOperations.Division(num1, num2);
                    
                    if (double.IsInfinity(result))
                    {
                        UIManager.DisplayWarning("Result is infinite due to overflow.");
                        UIManager.DisplayError("Result: Infinity");
                        historyManager.AddEntry($"Division: {num1} / {num2} = Infinity (overflow)");
                    }
                    else if (double.IsNaN(result))
                    {
                        UIManager.DisplayError("Result: NaN (invalid operation)");
                        historyManager.AddEntry($"Division: {num1} / {num2} = NaN (invalid operation)");
                    }
                    else
                    {
                        UIManager.DisplayResult(result);
                        historyManager.AddEntry($"Division: {num1} / {num2} = {result}");
                    }
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing division: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformFourOperations()
        {
            try
            {
                double num1 = UIManager.GetNumberInput("Enter the first number: ");
                double num2 = UIManager.GetNumberInput("Enter the second number: ");
                
                CompositeOperations.DisplayFourOperations(num1, num2);
                string historyEntry = CompositeOperations.CreateFourOperationsHistoryEntry(num1, num2);
                historyManager.AddEntry(historyEntry);
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing four operations: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformAverageCalculation()
        {
            try
            {
                double num1 = UIManager.GetNumberInput("Enter the first number: ");
                double num2 = UIManager.GetNumberInput("Enter the second number: ");
                double num3 = UIManager.GetNumberInput("Enter the third number: ");
                double result = AdvancedOperations.CalculateAverage(num1, num2, num3);
                
                if (double.IsInfinity(result))
                {
                    UIManager.DisplayWarning("Result is infinite due to overflow.");
                    UIManager.DisplayError("Average: Infinity");
                    historyManager.AddEntry($"Average: ({num1} + {num2} + {num3}) / 3 = Infinity (overflow)");
                }
                else if (double.IsNaN(result))
                {
                    UIManager.DisplayError("Average: NaN (invalid operation)");
                    historyManager.AddEntry($"Average: ({num1} + {num2} + {num3}) / 3 = NaN (invalid operation)");
                }
                else
                {
                    UIManager.DisplayResult("Average", result);
                    historyManager.AddEntry($"Average: ({num1} + {num2} + {num3}) / 3 = {result}");
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error calculating average: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformSquareRoot()
        {
            try
            {
                double number = UIManager.GetPositiveNumberInput("Enter the number to find the square root (must be non-negative): ");
                double result = AdvancedOperations.SquareRoot(number);
                
                if (double.IsNaN(result))
                {
                    UIManager.DisplayError("Result: NaN (cannot take square root of a negative number)");
                    historyManager.AddEntry($"Square root: sqrt({number}) = NaN");
                }
                else if (double.IsInfinity(result))
                {
                    UIManager.DisplayWarning("Result is infinite due to overflow.");
                    UIManager.DisplayError("Result: Infinity");
                    historyManager.AddEntry($"Square root: sqrt({number}) = Infinity (overflow)");
                }
                else
                {
                    UIManager.DisplayResult(result);
                    historyManager.AddEntry($"Square root: sqrt({number}) = {result}");
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error calculating square root: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void PerformExponentiation()
        {
            try
            {
                double baseNum = UIManager.GetNumberInput("Enter the base number: ");
                double exponent = UIManager.GetNumberInput("Enter the exponent: ");
                double result = AdvancedOperations.Exponentiation(baseNum, exponent);
                
                if (double.IsInfinity(result))
                {
                    UIManager.DisplayWarning("Result is infinite due to overflow or mathematical properties.");
                    UIManager.DisplayError("Result: Infinity");
                    historyManager.AddEntry($"Exponentiation: {baseNum} ^ {exponent} = Infinity");
                }
                else if (double.IsNaN(result))
                {
                    UIManager.DisplayError("Result: NaN (invalid operation)");
                    historyManager.AddEntry($"Exponentiation: {baseNum} ^ {exponent} = NaN (invalid operation)");
                }
                else
                {
                    UIManager.DisplayResult(result);
                    historyManager.AddEntry($"Exponentiation: {baseNum} ^ {exponent} = {result}");
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing exponentiation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }        private void ViewHistory()
        {
            try
            {
                historyManager.DisplayHistory();
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error viewing history: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        private void SaveHistory()
        {
            try
            {
                historyManager.SaveHistory();
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error saving history: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        private void LoadHistory()
        {
            try
            {
                historyManager.LoadHistory();
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error loading history: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        private void ExitApplication()
        {
            UIManager.DisplayGoodbye();
            isRunning = false;
        }

        /// <summary>
        /// Processes the user's mode choice
        /// </summary>
        /// <param name="modeChoice">The user's mode choice</param>
        private void ProcessModeChoice(int modeChoice)
        {
            switch (modeChoice)
            {
                case 1:
                    RunBasicMode();
                    break;
                case 2:
                    RunScientificMode();
                    break;
                case 3:
                    RunProgrammerMode();
                    break;
                case 4:
                    ExitApplication();
                    break;
                default:
                    UIManager.DisplayInvalidSelection();
                    UIManager.Pause();
                    break;
            }
        }

        /// <summary>
        /// Runs the basic calculator mode
        /// </summary>
        private void RunBasicMode()
        {
            bool backToModeSelection = false;
            
            while (!backToModeSelection && isRunning)
            {
                try
                {
                    UIManager.DisplayMainMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 13) // Add option to go back to mode selection
                    {
                        backToModeSelection = true;
                        continue;
                    }

                    ProcessUserChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs the scientific calculator mode
        /// </summary>
        private void RunScientificMode()
        {
            bool backToModeSelection = false;
            
            while (!backToModeSelection && isRunning)
            {
                try
                {
                    UIManager.DisplayScientificMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 9) // Back to mode selection
                    {
                        backToModeSelection = true;
                        continue;
                    }

                    ProcessScientificChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs the programmer calculator mode
        /// </summary>
        private void RunProgrammerMode()
        {
            bool backToModeSelection = false;
            
            while (!backToModeSelection && isRunning)
            {
                try
                {
                    UIManager.DisplayProgrammerMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 8) // Back to mode selection
                    {
                        backToModeSelection = true;
                        continue;
                    }

                    ProcessProgrammerChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Processes the user's scientific mode choice
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessScientificChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    RunBasicOperationsFromScientific();
                    break;
                case 2:
                    RunTrigonometricOperations();
                    break;
                case 3:
                    RunLogarithmicOperations();
                    break;
                case 4:
                    RunExponentialOperations();
                    break;
                case 5:
                    RunOtherScientificOperations();
                    break;
                case 6:
                    ViewHistory();
                    break;
                case 7:
                    SaveHistory();
                    break;
                case 8:
                    LoadHistory();
                    break;
                default:
                    UIManager.DisplayInvalidSelection();
                    UIManager.Pause();
                    break;
            }
        }

        /// <summary>
        /// Processes the user's programmer mode choice
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessProgrammerChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    RunBaseConversionOperations();
                    break;
                case 2:
                    RunBitwiseOperations();
                    break;
                case 3:
                    RunBitShiftingOperations();
                    break;
                case 4:
                    RunOtherProgrammerOperations();
                    break;
                case 5:
                    ViewHistory();
                    break;
                case 6:
                    SaveHistory();
                    break;
                case 7:
                    LoadHistory();
                    break;
                default:
                    UIManager.DisplayInvalidSelection();
                    UIManager.Pause();
                    break;
            }
        }

        /// <summary>
        /// Runs basic operations from scientific mode
        /// </summary>
        private void RunBasicOperationsFromScientific()
        {
            bool backToScientific = false;
            
            while (!backToScientific)
            {
                try
                {
                    UIManager.DisplayMainMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 12) // Exit becomes "back to scientific"
                    {
                        backToScientific = true;
                        continue;
                    }

                    ProcessUserChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs trigonometric operations
        /// </summary>
        private void RunTrigonometricOperations()
        {
            bool backToScientific = false;
            
            while (!backToScientific)
            {
                try
                {
                    UIManager.DisplayTrigonometricMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 6) // Back to scientific menu
                    {
                        backToScientific = true;
                        continue;
                    }

                    ProcessTrigonometricChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs logarithmic operations
        /// </summary>
        private void RunLogarithmicOperations()
        {
            bool backToScientific = false;
            
            while (!backToScientific)
            {
                try
                {
                    UIManager.DisplayLogarithmicMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 3) // Back to scientific menu
                    {
                        backToScientific = true;
                        continue;
                    }

                    ProcessLogarithmicChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs exponential operations
        /// </summary>
        private void RunExponentialOperations()
        {
            bool backToScientific = false;
            
            while (!backToScientific)
            {
                try
                {
                    UIManager.DisplayExponentialMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 3) // Back to scientific menu
                    {
                        backToScientific = true;
                        continue;
                    }

                    ProcessExponentialChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs other scientific operations
        /// </summary>
        private void RunOtherScientificOperations()
        {
            bool backToScientific = false;
            
            while (!backToScientific)
            {
                try
                {
                    UIManager.DisplayOtherScientificMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 4) // Back to scientific menu
                    {
                        backToScientific = true;
                        continue;
                    }

                    ProcessOtherScientificChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Processes trigonometric function choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessTrigonometricChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Sin
                        {
                            double angle = UIManager.GetNumberInput("Enter angle in radians: ");
                            double result = ScientificOperations.Sin(angle);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"Sin({angle}) = {result}");
                        }
                        break;
                    case 2: // Cos
                        {
                            double angle = UIManager.GetNumberInput("Enter angle in radians: ");
                            double result = ScientificOperations.Cos(angle);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"Cos({angle}) = {result}");
                        }
                        break;
                    case 3: // Tan
                        {
                            double angle = UIManager.GetNumberInput("Enter angle in radians: ");
                            double result = ScientificOperations.Tan(angle);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"Tan({angle}) = {result}");
                        }
                        break;
                    case 4: // Degrees to Radians
                        {
                            double degrees = UIManager.GetNumberInput("Enter angle in degrees: ");
                            double result = ScientificOperations.DegreesToRadians(degrees);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"{degrees}° = {result} radians");
                        }
                        break;
                    case 5: // Radians to Degrees
                        {
                            double radians = UIManager.GetNumberInput("Enter angle in radians: ");
                            double result = ScientificOperations.RadiansToDegrees(radians);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"{radians} radians = {result}°");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing trigonometric operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes logarithmic function choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessLogarithmicChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Natural logarithm
                        {
                            double number = UIManager.GetNumberInput("Enter number: ");
                            double result = ScientificOperations.Ln(number);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"ln({number}) = {result}");
                        }
                        break;
                    case 2: // Base-10 logarithm
                        {
                            double number = UIManager.GetNumberInput("Enter number: ");
                            double result = ScientificOperations.Log10(number);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"log({number}) = {result}");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing logarithmic operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes exponential function choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessExponentialChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // e^x
                        {
                            double x = UIManager.GetNumberInput("Enter exponent: ");
                            double result = ScientificOperations.Exp(x);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"e^{x} = {result}");
                        }
                        break;
                    case 2: // x^y
                        {
                            double baseNum = UIManager.GetNumberInput("Enter base number: ");
                            double exponent = UIManager.GetNumberInput("Enter exponent: ");
                            double result = AdvancedOperations.Exponentiation(baseNum, exponent);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"{baseNum}^{exponent} = {result}");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing exponential operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes other scientific function choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessOtherScientificChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Factorial
                        {
                            int n = UIManager.GetIntegerInput("Enter non-negative integer: ");
                            double result = ScientificOperations.Factorial(n);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"{n}! = {result}");
                        }
                        break;
                    case 2: // Absolute value
                        {
                            double number = UIManager.GetNumberInput("Enter number: ");
                            double result = ScientificOperations.Abs(number);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"|{number}| = {result}");
                        }
                        break;
                    case 3: // Square root
                        {
                            double number = UIManager.GetNumberInput("Enter number: ");
                            double result = AdvancedOperations.SquareRoot(number);
                            UIManager.DisplayResult(result);
                            historyManager.AddEntry($"√{number} = {result}");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing scientific operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Runs base conversion operations
        /// </summary>
        private void RunBaseConversionOperations()
        {
            bool backToProgrammer = false;
            
            while (!backToProgrammer)
            {
                try
                {
                    UIManager.DisplayBaseConversionMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 7) // Back to programmer menu
                    {
                        backToProgrammer = true;
                        continue;
                    }

                    ProcessBaseConversionChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs bitwise operations
        /// </summary>
        private void RunBitwiseOperations()
        {
            bool backToProgrammer = false;
            
            while (!backToProgrammer)
            {
                try
                {
                    UIManager.DisplayBitwiseMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 5) // Back to programmer menu
                    {
                        backToProgrammer = true;
                        continue;
                    }

                    ProcessBitwiseChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs bit shifting operations
        /// </summary>
        private void RunBitShiftingOperations()
        {
            bool backToProgrammer = false;
            
            while (!backToProgrammer)
            {
                try
                {
                    UIManager.DisplayBitShiftingMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 3) // Back to programmer menu
                    {
                        backToProgrammer = true;
                        continue;
                    }

                    ProcessBitShiftingChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Runs other programmer operations
        /// </summary>
        private void RunOtherProgrammerOperations()
        {
            bool backToProgrammer = false;
            
            while (!backToProgrammer)
            {
                try
                {
                    UIManager.DisplayOtherProgrammerMenu();
                    int choice = UIManager.GetUserChoice();

                    if (choice == -1)
                    {
                        UIManager.DisplayInvalidSelection();
                        UIManager.Pause();
                        continue;
                    }

                    if (choice == 2) // Back to programmer menu
                    {
                        backToProgrammer = true;
                        continue;
                    }

                    ProcessOtherProgrammerChoice(choice);
                }
                catch (Exception ex)
                {
                    UIManager.DisplayError($"An error occurred: {ex.Message}");
                    UIManager.Pause();
                }
            }
        }

        /// <summary>
        /// Processes base conversion choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessBaseConversionChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Decimal to Binary
                        {
                            long number = UIManager.GetLongInput("Enter decimal number: ");
                            string binary = ProgrammerOperations.DecimalToBinary(number);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(number);
                            string octal = ProgrammerOperations.DecimalToOctal(number);
                            UIManager.DisplayMultipleFormats(number, binary, hex, octal);
                            historyManager.AddEntry($"Decimal {number} = Binary {binary}");
                        }
                        break;
                    case 2: // Decimal to Hexadecimal
                        {
                            long number = UIManager.GetLongInput("Enter decimal number: ");
                            string binary = ProgrammerOperations.DecimalToBinary(number);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(number);
                            string octal = ProgrammerOperations.DecimalToOctal(number);
                            UIManager.DisplayMultipleFormats(number, binary, hex, octal);
                            historyManager.AddEntry($"Decimal {number} = Hex {hex}");
                        }
                        break;
                    case 3: // Decimal to Octal
                        {
                            long number = UIManager.GetLongInput("Enter decimal number: ");
                            string binary = ProgrammerOperations.DecimalToBinary(number);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(number);
                            string octal = ProgrammerOperations.DecimalToOctal(number);
                            UIManager.DisplayMultipleFormats(number, binary, hex, octal);
                            historyManager.AddEntry($"Decimal {number} = Octal {octal}");
                        }
                        break;
                    case 4: // Binary to Decimal
                        {
                            string binary = UIManager.GetStringInput("Enter binary number: ");
                            if (!ProgrammerOperations.IsValidBinary(binary))
                            {
                                UIManager.DisplayError("Invalid binary format. Please use only 0s and 1s.");
                                break;
                            }
                            long decimalValue = ProgrammerOperations.BinaryToDecimal(binary);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(decimalValue);
                            string octal = ProgrammerOperations.DecimalToOctal(decimalValue);
                            UIManager.DisplayMultipleFormats(decimalValue, binary, hex, octal);
                            historyManager.AddEntry($"Binary {binary} = Decimal {decimalValue}");
                        }
                        break;
                    case 5: // Hexadecimal to Decimal
                        {
                            string hex = UIManager.GetStringInput("Enter hexadecimal number: ");
                            if (!ProgrammerOperations.IsValidHexadecimal(hex))
                            {
                                UIManager.DisplayError("Invalid hexadecimal format.");
                                break;
                            }
                            long decimalValue = ProgrammerOperations.HexadecimalToDecimal(hex);
                            string binary = ProgrammerOperations.DecimalToBinary(decimalValue);
                            string octal = ProgrammerOperations.DecimalToOctal(decimalValue);
                            UIManager.DisplayMultipleFormats(decimalValue, binary, hex.ToUpper(), octal);
                            historyManager.AddEntry($"Hex {hex} = Decimal {decimalValue}");
                        }
                        break;
                    case 6: // Octal to Decimal
                        {
                            string octal = UIManager.GetStringInput("Enter octal number: ");
                            if (!ProgrammerOperations.IsValidOctal(octal))
                            {
                                UIManager.DisplayError("Invalid octal format. Please use only digits 0-7.");
                                break;
                            }
                            long decimalValue = ProgrammerOperations.OctalToDecimal(octal);
                            string binary = ProgrammerOperations.DecimalToBinary(decimalValue);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(decimalValue);
                            UIManager.DisplayMultipleFormats(decimalValue, binary, hex, octal);
                            historyManager.AddEntry($"Octal {octal} = Decimal {decimalValue}");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing base conversion: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes bitwise operation choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessBitwiseChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Bitwise AND
                        {
                            long a = UIManager.GetLongInput("Enter first number: ");
                            long b = UIManager.GetLongInput("Enter second number: ");
                            long result = ProgrammerOperations.BitwiseAnd(a, b);
                            string binary = ProgrammerOperations.DecimalToBinary(result);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(result);
                            string octal = ProgrammerOperations.DecimalToOctal(result);
                            UIManager.DisplayMultipleFormats(result, binary, hex, octal);
                            historyManager.AddEntry($"{a} AND {b} = {result}");
                        }
                        break;
                    case 2: // Bitwise OR
                        {
                            long a = UIManager.GetLongInput("Enter first number: ");
                            long b = UIManager.GetLongInput("Enter second number: ");
                            long result = ProgrammerOperations.BitwiseOr(a, b);
                            string binary = ProgrammerOperations.DecimalToBinary(result);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(result);
                            string octal = ProgrammerOperations.DecimalToOctal(result);
                            UIManager.DisplayMultipleFormats(result, binary, hex, octal);
                            historyManager.AddEntry($"{a} OR {b} = {result}");
                        }
                        break;
                    case 3: // Bitwise XOR
                        {
                            long a = UIManager.GetLongInput("Enter first number: ");
                            long b = UIManager.GetLongInput("Enter second number: ");
                            long result = ProgrammerOperations.BitwiseXor(a, b);
                            string binary = ProgrammerOperations.DecimalToBinary(result);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(result);
                            string octal = ProgrammerOperations.DecimalToOctal(result);
                            UIManager.DisplayMultipleFormats(result, binary, hex, octal);
                            historyManager.AddEntry($"{a} XOR {b} = {result}");
                        }
                        break;
                    case 4: // Bitwise NOT
                        {
                            long a = UIManager.GetLongInput("Enter number: ");
                            long result = ProgrammerOperations.BitwiseNot(a);
                            string binary = ProgrammerOperations.DecimalToBinary(result);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(result);
                            string octal = ProgrammerOperations.DecimalToOctal(result);
                            UIManager.DisplayMultipleFormats(result, binary, hex, octal);
                            historyManager.AddEntry($"NOT {a} = {result}");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing bitwise operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes bit shifting choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessBitShiftingChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Left shift
                        {
                            long number = UIManager.GetLongInput("Enter number: ");
                            int positions = UIManager.GetIntegerInput("Enter positions to shift: ");
                            long result = ProgrammerOperations.LeftShift(number, positions);
                            string binary = ProgrammerOperations.DecimalToBinary(result);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(result);
                            string octal = ProgrammerOperations.DecimalToOctal(result);
                            UIManager.DisplayMultipleFormats(result, binary, hex, octal);
                            historyManager.AddEntry($"{number} << {positions} = {result}");
                        }
                        break;
                    case 2: // Right shift
                        {
                            long number = UIManager.GetLongInput("Enter number: ");
                            int positions = UIManager.GetIntegerInput("Enter positions to shift: ");
                            long result = ProgrammerOperations.RightShift(number, positions);
                            string binary = ProgrammerOperations.DecimalToBinary(result);
                            string hex = ProgrammerOperations.DecimalToHexadecimal(result);
                            string octal = ProgrammerOperations.DecimalToOctal(result);
                            UIManager.DisplayMultipleFormats(result, binary, hex, octal);
                            historyManager.AddEntry($"{number} >> {positions} = {result}");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing bit shifting operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }

        /// <summary>
        /// Processes other programmer operation choices
        /// </summary>
        /// <param name="choice">The user's choice</param>
        private void ProcessOtherProgrammerChoice(int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // Population count
                        {
                            long number = UIManager.GetLongInput("Enter number: ");
                            int count = ProgrammerOperations.PopCount(number);
                            UIManager.DisplayResult(count);
                            historyManager.AddEntry($"Pop count of {number} = {count} bits set");
                        }
                        break;
                    default:
                        UIManager.DisplayInvalidSelection();
                        break;
                }
            }
            catch (Exception ex)
            {
                UIManager.DisplayError($"Error performing programmer operation: {ex.Message}");
            }
            finally
            {
                UIManager.Pause();
            }
        }
    }
}
