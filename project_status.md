
# Suggestions for Improving the Calculator

- [x] Add more mathematical functions: exponentiation, square root
- [ ] Add more mathematical functions: modulus, trigonometric functions (sin, cos, tan), logarithms
- [ ] Input validation & error handling: use TryParse for all number inputs, handle exceptions gracefully, provide user-friendly error messages
- [x] History feature: store and display a history of calculations performed during the session
- [ ] Support for multiple numbers: allow operations (like sum, average, etc.) on a list of numbers, not just two or three
- [ ] User interface improvements: add color to the console output, use clearer menus and prompts
- [ ] Modularize the code: move each operation into its own class or file for better organization and scalability
- [ ] Save/load functionality: allow users to save their calculation history to a file and load it later
- [ ] Unit tests: add a test project to ensure all operations work as expected
- [ ] Scientific/programmer modes: add modes for scientific or programmer calculations (binary, hex, bitwise operations)
- [ ] GUI version: create a simple graphical user interface using Windows Forms or WPF

# Project Status: Calculator Console App

## Features Implemented
- Basic arithmetic operations: Addition, Subtraction, Multiplication, Division
- Apply all four operations at once
- Calculate average of three numbers
- Calculation history (in-memory, viewable in menu)
- Advanced operations: Square root, Exponentiation
- Save calculation history to a file
- Load calculation history from a file
- User menu with options for all features

## Current Menu Options
1. Addition
2. Subtraction
3. Multiplication
4. Division
5. Apply four operations
6. Calculate average
7. Square root
8. Exponentiation
9. View history
10. Save history
11. Load history
12. Exit

## Current Issue (Build Error)

When running `dotnet run`, the following build errors occur:

```
C:\Users\jacqu\OneDrive\Bureau\personal project\calculator\Program.cs(10,5): error CS1519: Invalid token '{' in class, record, struct, or interface member declaration
...
C:\Users\jacqu\OneDrive\Bureau\personal project\calculator\Program.cs(207,13): error CS1022: Type or namespace definition, or end-of-file expected
...
```

### Error Summary
- There is a misplaced `{` after the class declaration:
  ```csharp
  class Program
      // File path for saving/loading history
      static string historyFilePath = "history.txt";
  {
      // List to store calculation history
      ...
  }
  ```
- This causes the compiler to see invalid tokens and misinterpret the class structure.
- There are also duplicate or misplaced `default:` and `else` statements at the end of the file.

## Next Steps

### To Fix the Build Error
- Fix the class declaration and member placement in `Program.cs`.
- Ensure all methods and fields are inside the `class Program { ... }` block.
- Rebuild the project after correcting the structure.

### To Improve the Calculator
- Add input validation and error handling for all user inputs.
- Expand the set of mathematical operations (modulus, trigonometric, logarithmic, etc.).
- Allow operations on multiple numbers (not just two or three).
- Refactor code for modularity and maintainability.
- Add unit tests for all operations.
- Enhance the user interface (console or GUI).
- Add scientific/programmer modes.

---

**Last updated:** May 26, 2025
