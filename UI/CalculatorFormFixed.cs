using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Calculator_App.Operations;
using Calculator_App.Services;

namespace Calculator_App.UI
{
    public partial class CalculatorForm : Form
    {
        private readonly HistoryManager historyManager;
        private string currentInput = "";
        private string previousInput = "";
        private string operation = "";
        private bool isNewCalculation = true;

        // UI Controls
        private TextBox displayTextBox = null!;
        private Button[] numberButtons = null!;
        private Button[] operatorButtons = null!;
        private Button equalsButton = null!;
        private Button clearButton = null!;
        private Button clearEntryButton = null!;
        private Button backspaceButton = null!;
        private Button decimalButton = null!;
        private Button squareRootButton = null!;
        private Button powerButton = null!;
        private Button historyButton = null!;
        private ListBox historyListBox = null!;
        private Panel historyPanel = null!;
        private Button closeHistoryButton = null!;

        public CalculatorForm()
        {
            historyManager = new HistoryManager();
            InitializeComponent();
            SetupEventHandlers();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculator";
            this.Size = new Size(350, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Display
            displayTextBox = new TextBox
            {
                Size = new Size(300, 50),
                Location = new Point(20, 20),
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true,
                Text = "0",
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };
            this.Controls.Add(displayTextBox);

            // Number buttons (0-9)
            numberButtons = new Button[10];
            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            
            // Layout: 7,8,9 in row 1; 4,5,6 in row 2; 1,2,3 in row 3; 0 in row 4
            int[] positions = { 9, 6, 7, 8, 3, 4, 5, 0, 1, 2 }; // index corresponds to number, value is position
            
            for (int i = 0; i < 10; i++)
            {
                int row = positions[i] / 3;
                int col = positions[i] % 3;
                
                if (i == 0) // Special case for 0 button
                {
                    row = 3;
                    col = 1;
                }
                
                numberButtons[i] = new Button
                {
                    Size = new Size(60, 60),
                    Location = new Point(20 + col * 65, 90 + row * 65),
                    Text = numbers[i],
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    UseVisualStyleBackColor = false
                };
                numberButtons[i].FlatAppearance.BorderColor = Color.Gray;
                this.Controls.Add(numberButtons[i]);
            }

            // Operator buttons
            string[] operators = { "+", "-", "×", "÷" };
            operatorButtons = new Button[4];
            
            for (int i = 0; i < 4; i++)
            {
                operatorButtons[i] = new Button
                {
                    Size = new Size(60, 60),
                    Location = new Point(215, 90 + i * 65),
                    Text = operators[i],
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    BackColor = Color.FromArgb(255, 165, 0),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    UseVisualStyleBackColor = false
                };
                operatorButtons[i].FlatAppearance.BorderColor = Color.DarkOrange;
                this.Controls.Add(operatorButtons[i]);
            }

            // Equals button
            equalsButton = new Button
            {
                Size = new Size(60, 60),
                Location = new Point(280, 285),
                Text = "=",
                Font = new Font("Arial", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 165, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            equalsButton.FlatAppearance.BorderColor = Color.DarkOrange;
            this.Controls.Add(equalsButton);

            // Decimal button
            decimalButton = new Button
            {
                Size = new Size(60, 60),
                Location = new Point(150, 285),
                Text = ".",
                Font = new Font("Arial", 14, FontStyle.Bold),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            decimalButton.FlatAppearance.BorderColor = Color.Gray;
            this.Controls.Add(decimalButton);

            // Clear button
            clearButton = new Button
            {
                Size = new Size(60, 40),
                Location = new Point(20, 350),
                Text = "C",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            clearButton.FlatAppearance.BorderColor = Color.DarkRed;
            this.Controls.Add(clearButton);

            // Clear Entry button
            clearEntryButton = new Button
            {
                Size = new Size(60, 40),
                Location = new Point(85, 350),
                Text = "CE",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            clearEntryButton.FlatAppearance.BorderColor = Color.DarkRed;
            this.Controls.Add(clearEntryButton);

            // Backspace button
            backspaceButton = new Button
            {
                Size = new Size(60, 40),
                Location = new Point(150, 350),
                Text = "⌫",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            backspaceButton.FlatAppearance.BorderColor = Color.Gray;
            this.Controls.Add(backspaceButton);

            // Square Root button
            squareRootButton = new Button
            {
                Size = new Size(60, 40),
                Location = new Point(215, 350),
                Text = "√",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            squareRootButton.FlatAppearance.BorderColor = Color.DarkGreen;
            this.Controls.Add(squareRootButton);

            // Power button
            powerButton = new Button
            {
                Size = new Size(60, 40),
                Location = new Point(280, 350),
                Text = "x²",
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            powerButton.FlatAppearance.BorderColor = Color.DarkGreen;
            this.Controls.Add(powerButton);

            // History button
            historyButton = new Button
            {
                Size = new Size(125, 40),
                Location = new Point(20, 400),
                Text = "History",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(23, 162, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            historyButton.FlatAppearance.BorderColor = Color.DarkCyan;
            this.Controls.Add(historyButton);

            // History Panel (initially hidden)
            historyPanel = new Panel
            {
                Size = new Size(300, 200),
                Location = new Point(20, 450),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };
            this.Controls.Add(historyPanel);

            historyListBox = new ListBox
            {
                Size = new Size(270, 160),
                Location = new Point(10, 10),
                Font = new Font("Arial", 10),
                BackColor = Color.White
            };
            historyPanel.Controls.Add(historyListBox);

            closeHistoryButton = new Button
            {
                Size = new Size(60, 25),
                Location = new Point(225, 175),
                Text = "Close",
                Font = new Font("Arial", 8),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            historyPanel.Controls.Add(closeHistoryButton);
        }

        private void SetupEventHandlers()
        {
            // Number buttons
            for (int i = 0; i < 10; i++)
            {
                int number = i; // Capture for lambda
                numberButtons[i].Click += (s, e) => NumberButton_Click(number.ToString());
            }

            // Operator buttons
            operatorButtons[0].Click += (s, e) => OperatorButton_Click("+");
            operatorButtons[1].Click += (s, e) => OperatorButton_Click("-");
            operatorButtons[2].Click += (s, e) => OperatorButton_Click("*");
            operatorButtons[3].Click += (s, e) => OperatorButton_Click("/");

            // Other buttons
            equalsButton.Click += EqualsButton_Click;
            clearButton.Click += ClearButton_Click;
            clearEntryButton.Click += ClearEntryButton_Click;
            backspaceButton.Click += BackspaceButton_Click;
            decimalButton.Click += DecimalButton_Click;
            squareRootButton.Click += SquareRootButton_Click;
            powerButton.Click += PowerButton_Click;
            historyButton.Click += HistoryButton_Click;
            closeHistoryButton.Click += CloseHistoryButton_Click;

            // Keyboard support
            this.KeyPreview = true;
            this.KeyPress += CalculatorForm_KeyPress;
            this.KeyDown += CalculatorForm_KeyDown;
        }

        private void NumberButton_Click(string number)
        {
            if (isNewCalculation || displayTextBox.Text == "0")
            {
                displayTextBox.Text = number;
                isNewCalculation = false;
            }
            else
            {
                displayTextBox.Text += number;
            }
            currentInput = displayTextBox.Text;
        }

        private void OperatorButton_Click(string op)
        {
            if (!string.IsNullOrEmpty(previousInput) && !string.IsNullOrEmpty(operation) && !isNewCalculation)
            {
                CalculateResult();
            }

            previousInput = displayTextBox.Text;
            operation = op;
            isNewCalculation = true;
        }

        private void EqualsButton_Click(object? sender, EventArgs e)
        {
            CalculateResult();
        }

        private void CalculateResult()
        {
            if (string.IsNullOrEmpty(previousInput) || string.IsNullOrEmpty(operation))
                return;

            try
            {
                double num1 = double.Parse(previousInput);
                double num2 = double.Parse(displayTextBox.Text);
                double result = 0;
                string operationText = "";

                switch (operation)
                {
                    case "+":
                        result = BasicOperations.Addition(num1, num2);
                        operationText = $"{num1} + {num2} = {result}";
                        break;
                    case "-":
                        result = BasicOperations.Subtraction(num1, num2);
                        operationText = $"{num1} - {num2} = {result}";
                        break;
                    case "*":
                        result = BasicOperations.Multiplication(num1, num2);
                        operationText = $"{num1} × {num2} = {result}";
                        break;
                    case "/":
                        if (num2 == 0)
                        {
                            MessageBox.Show("Cannot divide by zero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        result = BasicOperations.Division(num1, num2);
                        operationText = $"{num1} ÷ {num2} = {result}";
                        break;
                }

                displayTextBox.Text = result.ToString();
                historyManager.AddEntry(operationText);
                
                previousInput = "";
                operation = "";
                isNewCalculation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearButton_Click(null, EventArgs.Empty);
            }
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            displayTextBox.Text = "0";
            currentInput = "";
            previousInput = "";
            operation = "";
            isNewCalculation = true;
        }

        private void ClearEntryButton_Click(object? sender, EventArgs e)
        {
            displayTextBox.Text = "0";
            currentInput = "";
            isNewCalculation = true;
        }

        private void BackspaceButton_Click(object? sender, EventArgs e)
        {
            if (displayTextBox.Text.Length > 1)
            {
                displayTextBox.Text = displayTextBox.Text.Substring(0, displayTextBox.Text.Length - 1);
            }
            else
            {
                displayTextBox.Text = "0";
                isNewCalculation = true;
            }
            currentInput = displayTextBox.Text;
        }

        private void DecimalButton_Click(object? sender, EventArgs e)
        {
            if (!displayTextBox.Text.Contains("."))
            {
                if (isNewCalculation)
                {
                    displayTextBox.Text = "0.";
                    isNewCalculation = false;
                }
                else
                {
                    displayTextBox.Text += ".";
                }
                currentInput = displayTextBox.Text;
            }
        }

        private void SquareRootButton_Click(object? sender, EventArgs e)
        {
            try
            {
                double number = double.Parse(displayTextBox.Text);
                if (number < 0)
                {
                    MessageBox.Show("Cannot calculate square root of negative number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                double result = AdvancedOperations.SquareRoot(number);
                displayTextBox.Text = result.ToString();
                
                string operationText = $"√{number} = {result}";
                historyManager.AddEntry(operationText);
                
                isNewCalculation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PowerButton_Click(object? sender, EventArgs e)
        {
            try
            {
                double number = double.Parse(displayTextBox.Text);
                double result = AdvancedOperations.Exponentiation(number, 2);
                displayTextBox.Text = result.ToString();
                
                string operationText = $"{number}² = {result}";
                historyManager.AddEntry(operationText);
                
                isNewCalculation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HistoryButton_Click(object? sender, EventArgs e)
        {
            if (historyPanel.Visible)
            {
                historyPanel.Visible = false;
                this.Size = new Size(350, 500);
            }
            else
            {
                RefreshHistory();
                historyPanel.Visible = true;
                this.Size = new Size(350, 700);
            }
        }

        private void CloseHistoryButton_Click(object? sender, EventArgs e)
        {
            historyPanel.Visible = false;
            this.Size = new Size(350, 500);
        }

        private void RefreshHistory()
        {
            historyListBox.Items.Clear();
            var history = historyManager.GetAllEntries();
            foreach (var item in history)
            {
                historyListBox.Items.Add(item);
            }
        }

        private void CalculatorForm_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Handle number keys
            if (char.IsDigit(e.KeyChar))
            {
                NumberButton_Click(e.KeyChar.ToString());
            }

            // Handle operators
            switch (e.KeyChar)
            {
                case '+':
                    OperatorButton_Click("+");
                    break;
                case '-':
                    OperatorButton_Click("-");
                    break;
                case '*':
                    OperatorButton_Click("*");
                    break;
                case '/':
                    OperatorButton_Click("/");
                    break;
                case '.':
                    DecimalButton_Click(null, EventArgs.Empty);
                    break;
                case '=':
                case '\r': // Enter key
                    EqualsButton_Click(null, EventArgs.Empty);
                    break;
                case '\b': // Backspace
                    BackspaceButton_Click(null, EventArgs.Empty);
                    break;
                case (char)27: // Escape
                    ClearButton_Click(null, EventArgs.Empty);
                    break;
            }
        }

        private void CalculatorForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ClearEntryButton_Click(null, EventArgs.Empty);
            }
        }
    }
}
