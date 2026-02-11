# ConsoleTable

A lightweight .NET library for rendering beautiful tables in the console with customizable headers, cell alignment, colors, and multiple border styles.

## Features

- **Customizable Headers** — Define column names and configure each column independently
- **Cell Alignment** — Left, Center, or Right alignment per column
- **Color Support** — Set header and column foreground colors
- **Multiple Styles** — Default (ASCII), Unicode (box-drawing), and Minimal
- **ToString()** — Get the table as a plain-text string for logging or file output
- **Multi-target** — Supports `netstandard2.0`, `net8.0`, and `net10.0`

## Installation

```bash
dotnet add package ConsoleTable
```

## Quick Start

```csharp
using ConsoleTable;

var table = new ConsoleTable("Name", "Age", "City");
table.AddRow("Alice", 30, "Seoul");
table.AddRow("Bob", 25, "Tokyo");
table.SetAlignment(1, TextAlign.Right);
table.Print();
```

Output:
```
------------------------------
| Name  | Age | City  |
------------------------------
| Alice |  30 | Seoul |
| Bob   |  25 | Tokyo |
------------------------------
```

## Advanced Usage

### Column Definitions with Alignment

```csharp
var table = new ConsoleTable(
    new ConsoleTableColumn("Bot Name", TextAlign.Left),
    new ConsoleTableColumn("Date Time", TextAlign.Right),
    new ConsoleTableColumn("Count", TextAlign.Right),
    new ConsoleTableColumn("Progress", TextAlign.Right)
);

table.AddRow("Bot [00]", "02/11/2026 02:29:50", 256, "85%");
table.AddRow("Bot [01]", "02/11/2026 02:29:46", 152, "49%");
table.Print();
```

### Table Styles

```csharp
// Unicode box-drawing style
table.SetStyle(TableStyle.Unicode);
table.Print();

// Minimal style (no horizontal rules)
table.SetStyle(TableStyle.Minimal);
table.Print();
```

### Colors

```csharp
table.SetHeaderColor(ConsoleColor.Cyan);
table.SetColumnColor(0, ConsoleColor.Yellow);
table.Print();
```

### ToString()

```csharp
string output = table.ToString();
File.WriteAllText("report.txt", output);
```

### Method Chaining

All configuration methods return the table instance, so you can chain calls:

```csharp
new ConsoleTable("A", "B", "C")
    .AddRow(1, 2, 3)
    .AddRow(4, 5, 6)
    .SetAlignment(0, TextAlign.Right)
    .SetStyle(TableStyle.Unicode)
    .SetHeaderColor(ConsoleColor.Green)
    .Print();
```

## API Reference

### `ConsoleTable`

| Method | Description |
|--------|-------------|
| `ConsoleTable(params string[] headers)` | Create table with simple header names |
| `ConsoleTable(params ConsoleTableColumn[] columns)` | Create table with full column config |
| `AddRow(params object?[] values)` | Add a data row |
| `AddRows(IEnumerable<object?[]> rows)` | Add multiple rows |
| `SetAlignment(int columnIndex, TextAlign align)` | Set column alignment |
| `SetStyle(TableStyle style)` | Set border style |
| `SetHeaderColor(ConsoleColor color)` | Set header row color |
| `SetColumnColor(int columnIndex, ConsoleColor color)` | Set column color |
| `SetMinWidth(int columnIndex, int minWidth)` | Set minimum column width |
| `Print()` | Print to console |
| `Print(TextWriter writer)` | Print to writer |
| `ToString()` | Get as string |

### `TextAlign`

`Left` | `Center` | `Right`

### `TableStyle`

`Default` | `Unicode` | `Minimal`

## License

MIT
