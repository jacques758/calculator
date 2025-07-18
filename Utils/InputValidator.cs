using System;

namespace Calculator_App.Utils
{
    /// <summary>
    /// Provides centralized input validation utilities for the calculator application
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Validates and parses a string input to a double
        /// </summary>
        /// <param name="input">The input string to validate</param>
        /// <param name="result">The parsed double value if successful</param>
        /// <param name="errorMessage">The error message if validation fails</param>
        /// <returns>True if validation succeeds, false otherwise</returns>
        public static bool TryParseDouble(string? input, out double result, out string errorMessage)
        {
            result = 0;
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    errorMessage = "Input cannot be empty. Please enter a valid number.";
                    return false;
                }

                string trimmedInput = input.Trim();

                if (!double.TryParse(trimmedInput, out result))
                {
                    errorMessage = "Invalid input. Please enter a valid number (e.g., 123, -45.67, 3.14).";
                    return false;
                }

                if (double.IsInfinity(result))
                {
                    errorMessage = "The number is too large. Please enter a smaller number.";
                    result = 0;
                    return false;
                }

                if (double.IsNaN(result))
                {
                    errorMessage = "Invalid number format. Please enter a valid number.";
                    result = 0;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"An unexpected error occurred while parsing input: {ex.Message}";
                result = 0;
                return false;
            }
        }

        /// <summary>
        /// Validates and parses a string input to an integer within a specified range
        /// </summary>
        /// <param name="input">The input string to validate</param>
        /// <param name="min">Minimum valid value</param>
        /// <param name="max">Maximum valid value</param>
        /// <param name="result">The parsed integer value if successful</param>
        /// <param name="errorMessage">The error message if validation fails</param>
        /// <returns>True if validation succeeds, false otherwise</returns>
        public static bool TryParseInteger(string? input, int min, int max, out int result, out string errorMessage)
        {
            result = 0;
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    errorMessage = "Input cannot be empty. Please enter a valid integer.";
                    return false;
                }

                string trimmedInput = input.Trim();

                if (!int.TryParse(trimmedInput, out result))
                {
                    errorMessage = "Invalid input. Please enter a valid integer.";
                    return false;
                }

                if (result < min || result > max)
                {
                    errorMessage = $"Please enter a number between {min} and {max}.";
                    result = 0;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"An unexpected error occurred while parsing input: {ex.Message}";
                result = 0;
                return false;
            }
        }

        /// <summary>
        /// Validates that a double value is positive (greater than or equal to 0)
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="errorMessage">The error message if validation fails</param>
        /// <returns>True if the value is non-negative, false otherwise</returns>
        public static bool IsNonNegative(double value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (double.IsNaN(value))
            {
                errorMessage = "Value is not a valid number.";
                return false;
            }

            if (value < 0)
            {
                errorMessage = "This operation requires a non-negative number. Please enter a positive number or zero.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that a double value is not zero
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="errorMessage">The error message if validation fails</param>
        /// <returns>True if the value is non-zero, false otherwise</returns>
        public static bool IsNonZero(double value, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (double.IsNaN(value))
            {
                errorMessage = "Value is not a valid number.";
                return false;
            }

            if (value == 0)
            {
                errorMessage = "This operation requires a non-zero number. Please enter a number other than zero.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that a mathematical result is valid
        /// </summary>
        /// <param name="result">The result to validate</param>
        /// <param name="operationName">The name of the operation for error messages</param>
        /// <param name="statusMessage">Status message about the result</param>
        /// <returns>True if the result is finite, false if infinite or NaN</returns>
        public static bool IsValidResult(double result, string operationName, out string statusMessage)
        {
            statusMessage = string.Empty;

            if (double.IsNaN(result))
            {
                statusMessage = $"{operationName}: NaN (invalid operation)";
                return false;
            }

            if (double.IsInfinity(result))
            {
                statusMessage = $"{operationName}: Infinity";
                return false;
            }

            statusMessage = $"{operationName}: {result}";
            return true;
        }
    }
}
