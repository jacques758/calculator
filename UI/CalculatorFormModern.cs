using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Calculator_App.Operations;
using Calculator_App.Services;

namespace Calculator_App.UI
{
    public partial class CalculatorFormModern : Form
    {
        private readonly HistoryManager historyManager;
        private string currentInput = "";
        private string previousInput = "";
        private string operation = "";
        private bool isNewCalculation = true;

        // UI Controls
        private TextBox displayTextBox = null!;
        private TableLayoutPanel mainPanel = null!;
        private TableLayoutPanel buttonPanel = null!;
        private Button[] numberButtons = null!;
        private Button[] operatorButtons = null!;
        private Button equalsButton = null!;
        private Button clearButton = null!;
        private Button percentButton = null!;
        private Button signButton = null!;
        private Button decimalButton = null!;

        // Modern Design Color Palette
        private static class Colors
        {
            // Main theme colors
            public static readonly Color Background = Color.FromArgb(35, 39, 42);      // #23272A - Charcoal background
            public static readonly Color DisplayBackground = Color.FromArgb(24, 26, 27); // #181A1B - Black display
            public static readonly Color DisplayText = Color.White;

            // Button colors
            public static readonly Color NumberBackground = Color.FromArgb(44, 47, 51);  // #2C2F33 - Gray for numbers
            public static readonly Color NumberText = Color.White;
            public static readonly Color OperatorBackground = Color.FromArgb(255, 149, 0); // #FF9500 - Orange for operators
            public static readonly Color OperatorText = Color.White;
            public static readonly Color ClearBackground = Color.FromArgb(0, 122, 255);   // #007AFF - Blue for clear
            public static readonly Color ClearText = Color.White;

            // Hover states (10% lighter)
            public static readonly Color NumberHover = Color.FromArgb(64, 67, 71);
            public static readonly Color OperatorHover = Color.FromArgb(255, 169, 40);
            public static readonly Color ClearHover = Color.FromArgb(40, 142, 255);

            // Active/Pressed states (10% darker)
            public static readonly Color NumberActive = Color.FromArgb(34, 37, 41);
            public static readonly Color OperatorActive = Color.FromArgb(235, 129, 0);
            public static readonly Color ClearActive = Color.FromArgb(0, 102, 235);
        }

        // Modern Typography
        private static class Fonts
        {
            public static readonly Font Display = new Font("Segoe UI", 48, FontStyle.Regular);
            public static readonly Font Button = new Font("Segoe UI", 24, FontStyle.Regular);
            public static readonly Font SmallButton = new Font("Segoe UI", 20, FontStyle.Regular);
        }

        // Layout Constants
        private const int FORM_WIDTH = 400;
        private const int FORM_HEIGHT = 600;
        private const int BUTTON_SIZE = 70;
        private const int BUTTON_SPACING = 12;
        private const int FORM_PADDING = 20;
        private const int DISPLAY_HEIGHT = 120;

        public CalculatorFormModern()
        {
            historyManager = new HistoryManager();
            InitializeComponent();
            SetupEventHandlers();
            SetupAccessibility();
        }

        private void InitializeComponent()
        {
            // Form setup
            this.Text = "Calculator";
            this.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Colors.Background;
            this.DoubleBuffered = true;

            // Main panel
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(FORM_PADDING),
                BackColor = Colors.Background
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, DISPLAY_HEIGHT));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Display area with custom styling
            Panel displayPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Colors.DisplayBackground,
                Padding = new Padding(20, 10, 20, 10)
            };

            displayTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = Fonts.Display,
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true,
                Text = "0",
                BackColor = Colors.DisplayBackground,
                ForeColor = Colors.DisplayText,
                BorderStyle = BorderStyle.None
            };

            displayPanel.Controls.Add(displayTextBox);
            
            // Add rounded corners to display panel
            displayPanel.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRectPath(displayPanel.ClientRectangle, 15))
                {
                    displayPanel.Region = new Region(path);
                }
            };

            mainPanel.Controls.Add(displayPanel, 0, 0);

            // Button panel - 5 rows, 4 columns
            buttonPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 4,
                BackColor = Colors.Background
            };

            // Set up row and column styles
            for (int i = 0; i < 5; i++)
            {
                buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            }
            for (int i = 0; i < 4; i++)
            {
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }

            // Create all buttons
            CreateButtons();

            mainPanel.Controls.Add(buttonPanel, 0, 1);
            this.Controls.Add(mainPanel);
        }

        private void CreateButtons()
        {
            // Row 0: Clear, +/-, %, ÷
            clearButton = CreateCircularButton("C", Colors.ClearBackground, Colors.ClearText, Colors.ClearHover, Colors.ClearActive);
            buttonPanel.Controls.Add(clearButton, 0, 0);

            signButton = CreateCircularButton("+/-", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(signButton, 1, 0);

            percentButton = CreateCircularButton("%", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(percentButton, 2, 0);

            operatorButtons = new Button[4];
            operatorButtons[0] = CreateCircularButton("÷", Colors.OperatorBackground, Colors.OperatorText, Colors.OperatorHover, Colors.OperatorActive);
            buttonPanel.Controls.Add(operatorButtons[0], 3, 0);

            // Row 1: 7, 8, 9, ×
            numberButtons = new Button[10];
            numberButtons[7] = CreateCircularButton("7", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[7], 0, 1);

            numberButtons[8] = CreateCircularButton("8", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[8], 1, 1);

            numberButtons[9] = CreateCircularButton("9", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[9], 2, 1);

            operatorButtons[1] = CreateCircularButton("×", Colors.OperatorBackground, Colors.OperatorText, Colors.OperatorHover, Colors.OperatorActive);
            buttonPanel.Controls.Add(operatorButtons[1], 3, 1);

            // Row 2: 4, 5, 6, -
            numberButtons[4] = CreateCircularButton("4", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[4], 0, 2);

            numberButtons[5] = CreateCircularButton("5", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[5], 1, 2);

            numberButtons[6] = CreateCircularButton("6", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[6], 2, 2);

            operatorButtons[2] = CreateCircularButton("-", Colors.OperatorBackground, Colors.OperatorText, Colors.OperatorHover, Colors.OperatorActive);
            buttonPanel.Controls.Add(operatorButtons[2], 3, 2);

            // Row 3: 1, 2, 3, +
            numberButtons[1] = CreateCircularButton("1", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[1], 0, 3);

            numberButtons[2] = CreateCircularButton("2", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[2], 1, 3);

            numberButtons[3] = CreateCircularButton("3", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[3], 2, 3);

            operatorButtons[3] = CreateCircularButton("+", Colors.OperatorBackground, Colors.OperatorText, Colors.OperatorHover, Colors.OperatorActive);
            buttonPanel.Controls.Add(operatorButtons[3], 3, 3);

            // Row 4: 0 (spanning 2 columns), ., =
            numberButtons[0] = CreateCircularButton("0", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(numberButtons[0], 0, 4);
            buttonPanel.SetColumnSpan(numberButtons[0], 2);

            decimalButton = CreateCircularButton(".", Colors.NumberBackground, Colors.NumberText, Colors.NumberHover, Colors.NumberActive);
            buttonPanel.Controls.Add(decimalButton, 2, 4);

            equalsButton = CreateCircularButton("=", Colors.OperatorBackground, Colors.OperatorText, Colors.OperatorHover, Colors.OperatorActive);
            buttonPanel.Controls.Add(equalsButton, 3, 4);
        }

        private Button CreateCircularButton(string text, Color backColor, Color foreColor, Color hoverColor, Color activeColor)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(BUTTON_SIZE, BUTTON_SIZE),
                BackColor = backColor,
                ForeColor = foreColor,
                Font = Fonts.Button,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.None
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = activeColor;

            // Make button circular
            button.Paint += (s, e) =>
            {
                Button btn = (Button)s!;
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, btn.Width - 1, btn.Height - 1);
                btn.Region = new Region(path);

                // Draw shadow effect
                using (GraphicsPath shadowPath = new GraphicsPath())
                {
                    shadowPath.AddEllipse(2, 2, btn.Width - 3, btn.Height - 3);
                    using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                    {
                        shadowBrush.CenterColor = Color.FromArgb(30, 0, 0, 0);
                        shadowBrush.SurroundColors = new[] { Color.Transparent };
                        e.Graphics.FillPath(shadowBrush, shadowPath);
                    }
                }

                // Draw button background
                using (SolidBrush brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, btn.Width - 1, btn.Height - 1);
                }

                // Draw text
                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            // Special handling for the "0" button to make it pill-shaped
            if (text == "0")
            {
                button.Size = new Size(BUTTON_SIZE * 2 + BUTTON_SPACING, BUTTON_SIZE);
                button.Paint += (s, e) =>
                {
                    Button btn = (Button)s!;
                    GraphicsPath path = new GraphicsPath();
                    int radius = btn.Height / 2;
                    path.AddArc(0, 0, btn.Height, btn.Height, 90, 180);
                    path.AddArc(btn.Width - btn.Height, 0, btn.Height, btn.Height, 270, 180);
                    path.CloseFigure();
                    btn.Region = new Region(path);
                };
            }

            return button;
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void SetupAccessibility()
        {
            // Set tab order
            int tabIndex = 0;
            displayTextBox.TabIndex = tabIndex++;
            clearButton.TabIndex = tabIndex++;
            signButton.TabIndex = tabIndex++;
            percentButton.TabIndex = tabIndex++;

            // Numbers in logical order
            for (int i = 1; i <= 9; i++)
            {
                numberButtons[i].TabIndex = tabIndex++;
            }
            numberButtons[0].TabIndex = tabIndex++;
            decimalButton.TabIndex = tabIndex++;

            // Operators
            foreach (var op in operatorButtons)
            {
                op.TabIndex = tabIndex++;
            }
            equalsButton.TabIndex = tabIndex++;

            // Accessibility names
            displayTextBox.AccessibleName = "Calculator Display";
            clearButton.AccessibleName = "Clear";
            signButton.AccessibleName = "Change Sign";
            percentButton.AccessibleName = "Percent";
            equalsButton.AccessibleName = "Equals";

            for (int i = 0; i < 10; i++)
            {
                numberButtons[i].AccessibleName = $"Number {i}";
            }

            operatorButtons[0].AccessibleName = "Divide";
            operatorButtons[1].AccessibleName = "Multiply";
            operatorButtons[2].AccessibleName = "Subtract";
            operatorButtons[3].AccessibleName = "Add";
        }

        private void SetupEventHandlers()
        {
            // Number buttons
            for (int i = 0; i < 10; i++)
            {
                int number = i;
                numberButtons[i].Click += (s, e) => NumberButton_Click(number.ToString());
            }

            // Operator buttons
            operatorButtons[0].Click += (s, e) => OperatorButton_Click("/");
            operatorButtons[1].Click += (s, e) => OperatorButton_Click("*");
            operatorButtons[2].Click += (s, e) => OperatorButton_Click("-");
            operatorButtons[3].Click += (s, e) => OperatorButton_Click("+");

            // Other buttons
            equalsButton.Click += EqualsButton_Click;
            clearButton.Click += ClearButton_Click;
            signButton.Click += SignButton_Click;
            percentButton.Click += PercentButton_Click;
            decimalButton.Click += DecimalButton_Click;

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

        private void SignButton_Click(object? sender, EventArgs e)
        {
            try
            {
                double number = double.Parse(displayTextBox.Text);
                number = -number;
                displayTextBox.Text = number.ToString();
                currentInput = displayTextBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PercentButton_Click(object? sender, EventArgs e)
        {
            try
            {
                double number = double.Parse(displayTextBox.Text);
                number = number / 100;
                displayTextBox.Text = number.ToString();
                currentInput = displayTextBox.Text;
                isNewCalculation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    break;
                case (char)27: // Escape
                    ClearButton_Click(null, EventArgs.Empty);
                    break;
                case '%':
                    PercentButton_Click(null, EventArgs.Empty);
                    break;
            }
        }

        private void CalculatorForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ClearButton_Click(null, EventArgs.Empty);
            }
        }
    }
}