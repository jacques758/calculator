using System;
using System.Windows.Forms;
using Calculator_App.Controllers;
using Calculator_App.UI;

namespace Calculator_App
{
    /// <summary>
    /// Main entry point for the Calculator Application
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that starts the calculator application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        [STAThread]
        static void Main(string[] args)
        {
            // Check if console mode is requested
            if (args.Length > 0 && args[0].ToLower() == "--console")
            {
                // Run in console mode
                var calculator = new CalculatorController();
                calculator.Run();
            }
            else
            {
                // Run in GUI mode
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CalculatorFormRedesigned());
            }
        }
    }
}
