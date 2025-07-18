using System;
using System.Collections.Generic;
using System.IO;

namespace Calculator_App.Services
{
    /// <summary>
    /// Manages calculation history including saving and loading from file
    /// </summary>
    public class HistoryManager
    {
        private readonly string historyFilePath;
        private readonly List<string> history;

        /// <summary>
        /// Initializes a new instance of the HistoryManager class
        /// </summary>
        /// <param name="filePath">Path to the history file</param>
        public HistoryManager(string filePath = "history.txt")
        {
            historyFilePath = filePath;
            history = new List<string>();
        }

        /// <summary>
        /// Gets the current history count
        /// </summary>
        public int Count => history.Count;        /// <summary>
        /// Adds an entry to the calculation history
        /// </summary>
        /// <param name="entry">The calculation entry to add</param>
        public void AddEntry(string entry)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entry))
                {
                    throw new ArgumentException("History entry cannot be null or empty.", nameof(entry));
                }
                
                string timestampedEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {entry}";
                history.Add(timestampedEntry);
            }
            catch (Exception ex)
            {
                // Log error but don't throw to avoid breaking the calculation flow
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning: Could not add entry to history: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Gets all history entries
        /// </summary>
        /// <returns>List of all history entries</returns>
        public List<string> GetAllEntries()
        {
            return new List<string>(history);
        }

        /// <summary>
        /// Clears all history entries
        /// </summary>
        public void ClearHistory()
        {
            history.Clear();
        }

        /// <summary>
        /// Displays the calculation history with formatting
        /// </summary>
        public void DisplayHistory()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======= Calculation History =======");
            
            if (history.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No calculations yet.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var entry in history)
                {
                    Console.WriteLine(entry);
                }
                Console.ResetColor();
            }
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================");
            Console.ResetColor();
        }

        /// <summary>
        /// Saves the calculation history to a file
        /// </summary>
        public void SaveHistory()
        {
            try
            {
                File.WriteAllLines(historyFilePath, history);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"History saved to {historyFilePath}.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error saving history: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Loads the calculation history from a file
        /// </summary>
        public void LoadHistory()
        {
            try
            {
                if (File.Exists(historyFilePath))
                {
                    history.Clear();
                    history.AddRange(File.ReadAllLines(historyFilePath));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"History loaded from {historyFilePath}.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"No history file found at {historyFilePath}.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error loading history: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
