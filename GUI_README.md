# Calculator GUI - Windows Forms Version

## Overview
This is a Windows Forms GUI version of the Calculator application, providing a modern and user-friendly interface for performing calculations.

## Features

### Calculator Interface
- **Number Buttons**: 0-9 arranged in a standard calculator layout
- **Operator Buttons**: Addition (+), Subtraction (-), Multiplication (×), Division (÷)
- **Special Functions**: Square Root (√), Power of 2 (x²)
- **Control Buttons**: 
  - Clear (C) - Clears all
  - Clear Entry (CE) - Clears current entry
  - Backspace (⌫) - Removes last digit
  - Decimal (.) - Adds decimal point

### Advanced Features
- **History Panel**: View calculation history (expandable/collapsible)
- **Keyboard Support**: Full keyboard input support
- **Error Handling**: Proper validation and error messages
- **Modern UI**: Color-coded buttons with flat design

## How to Run

### GUI Mode (Default)
```bash
dotnet run
```

### Console Mode
```bash
dotnet run -- --console
```

## Keyboard Shortcuts
- **Numbers**: 0-9 keys
- **Operators**: +, -, *, / keys
- **Decimal**: . (period) key
- **Calculate**: = or Enter key
- **Clear**: Escape key
- **Backspace**: Backspace key
- **Clear Entry**: Delete key

## Button Layout
```
┌─────────────────────────────────┐
│ [    Display (0)              ] │
├─────────────────────────────────┤
│ [7] [8] [9]                [+] │
│ [4] [5] [6]                [-] │
│ [1] [2] [3]                [×] │
│ [0]     [.]                [÷] │
│                            [=] │
├─────────────────────────────────┤
│ [C] [CE] [⌫] [√] [x²]          │
│ [History]                       │
└─────────────────────────────────┘
```

## Color Scheme
- **Number Buttons**: White background with gray border
- **Operator Buttons**: Orange background with white text
- **Function Buttons**: Green background (√, x²)
- **Control Buttons**: Red background (C, CE), Gray background (⌫)
- **History Button**: Blue background

## History Feature
- Click the "History" button to expand/collapse the history panel
- View all previous calculations with timestamps
- History is automatically saved and persists between calculations
- History panel can be closed using the "Close" button

## Error Handling
- Division by zero protection
- Invalid input validation
- Negative square root prevention
- User-friendly error messages

## Requirements
- .NET 8.0 or later
- Windows operating system
- Windows Forms support

## Architecture
The GUI calculator integrates with the existing calculator engine:
- **Operations**: Uses `BasicOperations` and `AdvancedOperations` classes
- **History**: Uses `HistoryManager` service
- **UI**: Separate Windows Forms interface in `Calculator_App.UI` namespace

This maintains separation of concerns and code reusability between console and GUI versions.
