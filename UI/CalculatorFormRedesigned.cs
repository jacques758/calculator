using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Calculator_App.Operations;
using Calculator_App.Services;

namespace Calculator_App.UI
{
    public partial class CalculatorFormRedesigned : Form
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
        private Button clearEntryButton = null!;
        private Button backspaceButton = null!;
        private Button decimalButton = null!;
        private Button squareRootButton = null!;
        private Button powerButton = null!;
        private Button historyButton = null!;
        private ListBox historyListBox = null!;
        private Panel historyPanel = null!;
        private Button closeHistoryButton = null!;

        // Design Constants - Color Palette
        private static class Colors
        {
            // Numbers and Decimal
            public static readonly Color NumberBackground = Color.FromArgb(255, 255, 255);
            public static readonly Color NumberForeground = Color.FromArgb(34, 34, 34);
            public static readonly Color NumberBorder = Color.FromArgb(176, 176, 176);

            // Operators and Equals
            public static readonly Color OperatorBackground = Color.FromArgb(255, 152, 0);
            public static readonly Color OperatorForeground = Color.FromArgb(255, 255, 255);
            public static readonly Color OperatorBorder = Color.FromArgb(245, 124, 0);

            // Control (C, CE)
            public static readonly Color ControlBackground = Color.FromArgb(244, 67, 54);
            public static readonly Color ControlForeground = Color.FromArgb(255, 255, 255);
            public static readonly Color ControlBorder = Color.FromArgb(183, 28, 28);

            // Backspace
            public static readonly Color BackspaceBackground = Color.FromArgb(158, 158, 158);
            public static readonly Color BackspaceForeground = Color.FromArgb(255, 255, 255);
            public static readonly Color BackspaceBorder = Color.FromArgb(97, 97, 97);

            // Functions (Square Root, Power)
            public static readonly Color FunctionBackground = Color.FromArgb(76, 175, 80);
            public static readonly Color FunctionForeground = Color.FromArgb(255, 255, 255);
            public static readonly Color FunctionBorder = Color.FromArgb(27, 94, 32);

            // History
            public static readonly Color HistoryBackground = Color.FromArgb(33, 150, 243);
            public static readonly Color HistoryForeground = Color.FromArgb(255, 255, 255);
            public static readonly Color HistoryBorder = Color.FromArgb(21, 101, 192);

            // Form and Display
            public static readonly Color FormBackground = Color.FromArgb(240, 240, 240);
            public static readonly Color DisplayBackground = Color.FromArgb(255, 255, 255);
        }

        // Design Constants - Typography
        private static class Fonts
        {
            public static readonly Font Display = new Font("Arial", 22, FontStyle.Bold);
            public static readonly Font MainButton = new Font("Arial", 16, FontStyle.Bold);
            public static readonly Font ControlButton = new Font("Arial", 14, FontStyle.Bold);
            public static readonly Font HistoryList = new Font("Arial", 12, FontStyle.Regular);
        }

        // Design Constants - Dimensions
        private const int BUTTON_SIZE = 64;
        private const int BUTTON_SPACING = 8;
        private const int FORM_WIDTH = 350;
        private const int FORM_HEIGHT_NORMAL = 520;  // Increased to accommodate the fixed button panel height
        private const int FORM_HEIGHT_WITH_HISTORY = 700;

        public CalculatorFormRedesigned()
        {
            historyManager = new HistoryManager();
            InitializeComponent();
            SetupEventHandlers();
            SetupAccessibility();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculator";
            this.Size = new Size(FORM_WIDTH, FORM_HEIGHT_NORMAL);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Colors.FormBackground;

            // Main panel for overall layout
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(10)
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Display
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 424)); // Buttons - Fixed to accommodate all 6 rows
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // History (expandable)

            // Display
            displayTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = Fonts.Display,
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true,
                Text = "0",
                BackColor = Colors.DisplayBackground,
                BorderStyle = BorderStyle.Fixed3D,
                Margin = new Padding(0, 0, 0, 10)
            };
            mainPanel.Controls.Add(displayTextBox, 0, 0);

            // Button panel with TableLayoutPanel for perfect grid
            buttonPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 6,
                ColumnCount = 4,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            // Set up row and column styles for consistent sizing
            for (int i = 0; i < 5; i++)
            {
                buttonPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, BUTTON_SIZE + BUTTON_SPACING));
            }
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, BUTTON_SIZE)); // Last row without extra spacing

            for (int i = 0; i < 4; i++)
            {
                buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }

            // Create all buttons
            CreateNumberButtons();
            CreateOperatorButtons();
            CreateControlButtons();
            CreateFunctionButtons();
            CreateHistoryButton();

            mainPanel.Controls.Add(buttonPanel, 0, 1);

            // History Panel (initially hidden)
            CreateHistoryPanel();

            this.Controls.Add(mainPanel);
        }

        private void CreateNumberButtons()
        {
            numberButtons = new Button[10];
            
            // Layout positions for numbers (row, col)
            var positions = new (int row, int col)[]
            {
                (4, 1), // 0 (special case - double width)
                (3, 0), // 1
                (3, 1), // 2
                (3, 2), // 3
                (2, 0), // 4
                (2, 1), // 5
                (2, 2), // 6
                (1, 0), // 7
                (1, 1), // 8
                (1, 2)  // 9
            };

            for (int i = 0; i < 10; i++)
            {
                numberButtons[i] = CreateButton(
                    i.ToString(),
                    Colors.NumberBackground,
                    Colors.NumberForeground,
                    Colors.NumberBorder,
                    Fonts.MainButton
                );

                if (i == 0)
                {
                    // Special case for 0 button - double width
                    buttonPanel.Controls.Add(numberButtons[i], 0, positions[i].row);
                    buttonPanel.SetColumnSpan(numberButtons[i], 2);
                }
                else
                {
                    buttonPanel.Controls.Add(numberButtons[i], positions[i].col, positions[i].row);
                }
            }

            // Decimal button
            decimalButton = CreateButton(
                ".",
                Colors.NumberBackground,
                Colors.NumberForeground,
                Colors.NumberBorder,
                Fonts.MainButton
            );
            buttonPanel.Controls.Add(decimalButton, 2, 4);
        }

        private void CreateOperatorButtons()
        {
            string[] operators = { "÷", "×", "-", "+" };
            operatorButtons = new Button[4];

            for (int i = 0; i < 4; i++)
            {
                operatorButtons[i] = CreateButton(
                    operators[i],
                    Colors.OperatorBackground,
                    Colors.OperatorForeground,
                    Colors.OperatorBorder,
                    Fonts.MainButton
                );
                buttonPanel.Controls.Add(operatorButtons[i], 3, i + 1);
            }

            // Equals button
            equalsButton = CreateButton(
                "=",
                Colors.OperatorBackground,
                Colors.OperatorForeground,
                Colors.OperatorBorder,
                Fonts.MainButton
            );
            buttonPanel.Controls.Add(equalsButton, 3, 4);
        }

        private void CreateControlButtons()
        {
            // Clear button
            clearButton = CreateButton(
                "C",
                Colors.ControlBackground,
                Colors.ControlForeground,
                Colors.ControlBorder,
                Fonts.ControlButton
            );
            buttonPanel.Controls.Add(clearButton, 0, 0);

            // Clear Entry button
            clearEntryButton = CreateButton(
                "CE",
                Colors.ControlBackground,
                Colors.ControlForeground,
                Colors.ControlBorder,
                Fonts.ControlButton
            );
            buttonPanel.Controls.Add(clearEntryButton, 1, 0);

            // Backspace button
            backspaceButton = CreateButton(
                "⌫",
                Colors.BackspaceBackground,
                Colors.BackspaceForeground,
                Colors.BackspaceBorder,
                Fonts.ControlButton
            );
            buttonPanel.Controls.Add(backspaceButton, 2, 0);
        }

        private void CreateFunctionButtons()
        {
            // Square Root button
            squareRootButton = CreateButton(
                "√",
                Colors.FunctionBackground,
                Colors.FunctionForeground,
                Colors.FunctionBorder,
                Fonts.ControlButton
            );
            buttonPanel.Controls.Add(squareRootButton, 3, 0);

            // Power button
            powerButton = CreateButton(
                "x²",
                Colors.FunctionBackground,
                Colors.FunctionForeground,
                Colors.FunctionBorder,
                Fonts.ControlButton
            );
            buttonPanel.Controls.Add(powerButton, 0, 5);
        }

        private void CreateHistoryButton()
        {
            historyButton = CreateButton(
                "History",
                Colors.HistoryBackground,
                Colors.HistoryForeground,
                Colors.HistoryBorder,
                Fonts.ControlButton
            );
            buttonPanel.Controls.Add(historyButton, 1, 5);
            buttonPanel.SetColumnSpan(historyButton, 3);
        }

        private void CreateHistoryPanel()
        {
            historyPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Colors.DisplayBackground,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Padding = new Padding(10)
            };

            historyListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = Fonts.HistoryList,
                BackColor = Colors.DisplayBackground,
                BorderStyle = BorderStyle.None,
                SelectionMode = SelectionMode.None
            };

            closeHistoryButton = new Button
            {
                Text = "Close History",
                Size = new Size(100, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                BackColor = Colors.BackspaceBackground,
                ForeColor = Colors.BackspaceForeground,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            closeHistoryButton.FlatAppearance.BorderColor = Colors.BackspaceBorder;

            historyPanel.Controls.Add(historyListBox);
            historyPanel.Controls.Add(closeHistoryButton);
            closeHistoryButton.BringToFront();

            mainPanel.Controls.Add(historyPanel, 0, 2);
        }

        private Button CreateButton(string text, Color backColor, Color foreColor, Color borderColor, Font font)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                BackColor = backColor,
                ForeColor = foreColor,
                Font = font,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                Margin = new Padding(BUTTON_SPACING / 2),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderColor = borderColor;
            button.FlatAppearance.BorderSize = 1;
            
            // Add hover effect
            button.MouseEnter += (s, e) => button.BackColor = ControlPaint.Light(backColor, 0.1f);
            button.MouseLeave += (s, e) => button.BackColor = backColor;
            
            return button;
        }

        private void SetupAccessibility()
        {
            // Set tab order for logical navigation
            int tabIndex = 0;
            displayTextBox.TabIndex = tabIndex++;
            
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
            
            // Control buttons
            clearButton.TabIndex = tabIndex++;
            clearEntryButton.TabIndex = tabIndex++;
            backspaceButton.TabIndex = tabIndex++;
            
            // Function buttons
            squareRootButton.TabIndex = tabIndex++;
            powerButton.TabIndex = tabIndex++;
            historyButton.TabIndex = tabIndex++;
            
            // Accessibility names for screen readers
            displayTextBox.AccessibleName = "Calculator Display";
            clearButton.AccessibleName = "Clear All";
            clearEntryButton.AccessibleName = "Clear Entry";
            backspaceButton.AccessibleName = "Backspace";
            equalsButton.AccessibleName = "Equals";
            historyButton.AccessibleName = "Show History";
            
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
                int number = i; // Capture for lambda
                numberButtons[i].Click += (s, e) => NumberButton_Click(number.ToString());
            }

            // Operator buttons - map to internal operations
            operatorButtons[0].Click += (s, e) => OperatorButton_Click("/"); // ÷
            operatorButtons[1].Click += (s, e) => OperatorButton_Click("*"); // ×
            operatorButtons[2].Click += (s, e) => OperatorButton_Click("-"); // -
            operatorButtons[3].Click += (s, e) => OperatorButton_Click("+"); // +

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
                this.Size = new Size(FORM_WIDTH, FORM_HEIGHT_NORMAL);
                historyButton.Text = "History";
            }
            else
            {
                RefreshHistory();
                historyPanel.Visible = true;
                this.Size = new Size(FORM_WIDTH, FORM_HEIGHT_WITH_HISTORY);
                historyButton.Text = "Hide History";
            }
        }

        private void CloseHistoryButton_Click(object? sender, EventArgs e)
        {
            historyPanel.Visible = false;
            this.Size = new Size(FORM_WIDTH, FORM_HEIGHT_NORMAL);
            historyButton.Text = "History";
        }

        private void RefreshHistory()
        {
            historyListBox.Items.Clear();
            var history = historyManager.GetAllEntries();
            foreach (var item in history)
            {
                historyListBox.Items.Add(item);
            }
            
            // Scroll to bottom to show most recent
            if (historyListBox.Items.Count > 0)
            {
                historyListBox.SelectedIndex = historyListBox.Items.Count - 1;
                historyListBox.SelectedIndex = -1;
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