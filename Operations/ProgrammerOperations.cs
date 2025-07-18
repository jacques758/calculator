using System;
using System.Globalization;

namespace Calculator_App.Operations
{
    /// <summary>
    /// Contains programmer operations including binary, hexadecimal, and bitwise operations
    /// </summary>
    public static class ProgrammerOperations
    {
        /// <summary>
        /// Converts decimal to binary representation
        /// </summary>
        /// <param name="number">Decimal number to convert</param>
        /// <returns>Binary representation as string</returns>
        public static string DecimalToBinary(long number)
        {
            try
            {
                if (number == 0)
                    return "0";
                
                return Convert.ToString(number, 2);
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        /// <summary>
        /// Converts decimal to hexadecimal representation
        /// </summary>
        /// <param name="number">Decimal number to convert</param>
        /// <returns>Hexadecimal representation as string</returns>
        public static string DecimalToHexadecimal(long number)
        {
            try
            {
                return Convert.ToString(number, 16).ToUpper();
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        /// <summary>
        /// Converts decimal to octal representation
        /// </summary>
        /// <param name="number">Decimal number to convert</param>
        /// <returns>Octal representation as string</returns>
        public static string DecimalToOctal(long number)
        {
            try
            {
                return Convert.ToString(number, 8);
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        /// <summary>
        /// Converts binary to decimal
        /// </summary>
        /// <param name="binary">Binary string to convert</param>
        /// <returns>Decimal representation</returns>
        public static long BinaryToDecimal(string binary)
        {
            try
            {
                return Convert.ToInt64(binary, 2);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid binary format");
            }
        }

        /// <summary>
        /// Converts hexadecimal to decimal
        /// </summary>
        /// <param name="hex">Hexadecimal string to convert</param>
        /// <returns>Decimal representation</returns>
        public static long HexadecimalToDecimal(string hex)
        {
            try
            {
                return Convert.ToInt64(hex, 16);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid hexadecimal format");
            }
        }

        /// <summary>
        /// Converts octal to decimal
        /// </summary>
        /// <param name="octal">Octal string to convert</param>
        /// <returns>Decimal representation</returns>
        public static long OctalToDecimal(string octal)
        {
            try
            {
                return Convert.ToInt64(octal, 8);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid octal format");
            }
        }

        /// <summary>
        /// Performs bitwise AND operation
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of bitwise AND</returns>
        public static long BitwiseAnd(long a, long b)
        {
            return a & b;
        }

        /// <summary>
        /// Performs bitwise OR operation
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of bitwise OR</returns>
        public static long BitwiseOr(long a, long b)
        {
            return a | b;
        }

        /// <summary>
        /// Performs bitwise XOR operation
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of bitwise XOR</returns>
        public static long BitwiseXor(long a, long b)
        {
            return a ^ b;
        }

        /// <summary>
        /// Performs bitwise NOT operation
        /// </summary>
        /// <param name="a">Operand</param>
        /// <returns>Result of bitwise NOT</returns>
        public static long BitwiseNot(long a)
        {
            return ~a;
        }

        /// <summary>
        /// Performs left shift operation
        /// </summary>
        /// <param name="number">Number to shift</param>
        /// <param name="positions">Number of positions to shift</param>
        /// <returns>Result of left shift</returns>
        public static long LeftShift(long number, int positions)
        {
            try
            {
                if (positions < 0 || positions > 63)
                    throw new ArgumentException("Shift positions must be between 0 and 63");
                
                return number << positions;
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid shift operation");
            }
        }

        /// <summary>
        /// Performs right shift operation
        /// </summary>
        /// <param name="number">Number to shift</param>
        /// <param name="positions">Number of positions to shift</param>
        /// <returns>Result of right shift</returns>
        public static long RightShift(long number, int positions)
        {
            try
            {
                if (positions < 0 || positions > 63)
                    throw new ArgumentException("Shift positions must be between 0 and 63");
                
                return number >> positions;
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid shift operation");
            }
        }

        /// <summary>
        /// Gets the bit count (number of set bits) in a number
        /// </summary>
        /// <param name="number">Number to count bits for</param>
        /// <returns>Number of set bits</returns>
        public static int PopCount(long number)
        {
            int count = 0;
            while (number != 0)
            {
                count += (int)(number & 1);
                number >>= 1;
            }
            return count;
        }

        /// <summary>
        /// Validates if a string is a valid binary number
        /// </summary>
        /// <param name="binary">String to validate</param>
        /// <returns>True if valid binary, false otherwise</returns>
        public static bool IsValidBinary(string binary)
        {
            if (string.IsNullOrWhiteSpace(binary))
                return false;
            
            foreach (char c in binary)
            {
                if (c != '0' && c != '1')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Validates if a string is a valid hexadecimal number
        /// </summary>
        /// <param name="hex">String to validate</param>
        /// <returns>True if valid hexadecimal, false otherwise</returns>
        public static bool IsValidHexadecimal(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return false;
            
            return long.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _);
        }

        /// <summary>
        /// Validates if a string is a valid octal number
        /// </summary>
        /// <param name="octal">String to validate</param>
        /// <returns>True if valid octal, false otherwise</returns>
        public static bool IsValidOctal(string octal)
        {
            if (string.IsNullOrWhiteSpace(octal))
                return false;
            
            foreach (char c in octal)
            {
                if (c < '0' || c > '7')
                    return false;
            }
            return true;
        }
    }
}
