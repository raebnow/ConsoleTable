namespace ConsoleTable;

/// <summary>
/// Renders data as a formatted table in the console or as a string.
/// </summary>
/// <example>
/// <code>
/// var table = new ConsoleTable("Name", "Age", "City");
/// table.AddRow("Alice", 30, "Seoul");
/// table.AddRow("Bob", new ConsoleTableCell(25, TextAlign.Center), "Tokyo");
/// table.Print();
/// </code>
/// </example>
public sealed class ConsoleTable
{
    private readonly List<ConsoleTableColumn> _columns = new List<ConsoleTableColumn>();
    private readonly List<ConsoleTableCell[]> _rows = new List<ConsoleTableCell[]>();
    private TableStyle _style = TableStyle.Default;
    private ConsoleColor? _headerColor;

    /// <summary>
    /// Initializes a new <see cref="ConsoleTable"/> with columns defined by header names.
    /// All columns default to <see cref="TextAlign.Left"/>.
    /// </summary>
    /// <param name="headers">The header names for each column.</param>
    public ConsoleTable(params string[] headers)
    {
        if (headers == null) throw new ArgumentNullException(nameof(headers));
        if (headers.Length == 0) throw new ArgumentException("At least one header is required.", nameof(headers));

        foreach (var header in headers)
        {
            _columns.Add(new ConsoleTableColumn(header));
        }
    }

    /// <summary>
    /// Initializes a new <see cref="ConsoleTable"/> with fully configured column definitions.
    /// </summary>
    /// <param name="columns">The column definitions.</param>
    public ConsoleTable(params ConsoleTableColumn[] columns)
    {
        if (columns == null) throw new ArgumentNullException(nameof(columns));
        if (columns.Length == 0) throw new ArgumentException("At least one column is required.", nameof(columns));

        _columns.AddRange(columns);
    }

    /// <summary>
    /// Gets the list of columns in this table.
    /// </summary>
    public IReadOnlyList<ConsoleTableColumn> Columns => _columns;

    /// <summary>
    /// Gets the number of data rows in this table.
    /// </summary>
    public int RowCount => _rows.Count;

    /// <summary>
    /// Adds a row of values to the table.
    /// Values can be plain objects (converted via <see cref="object.ToString"/>)
    /// or <see cref="ConsoleTableCell"/> instances with per-cell alignment overrides.
    /// </summary>
    /// <param name="values">The cell values for the row.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable AddRow(params object?[] values)
    {
        var row = new ConsoleTableCell[_columns.Count];
        for (int i = 0; i < _columns.Count; i++)
        {
            if (i < values.Length && values[i] is ConsoleTableCell cell)
            {
                row[i] = cell;
            }
            else
            {
                var text = i < values.Length ? (values[i]?.ToString() ?? string.Empty) : string.Empty;
                row[i] = new ConsoleTableCell(text);
            }
        }
        _rows.Add(row);
        return this;
    }

    /// <summary>
    /// Adds multiple rows to the table at once.
    /// </summary>
    /// <param name="rows">The collection of row value arrays.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable AddRows(IEnumerable<object?[]> rows)
    {
        if (rows == null) throw new ArgumentNullException(nameof(rows));

        foreach (var row in rows)
        {
            AddRow(row);
        }
        return this;
    }

    /// <summary>
    /// Sets the default text alignment for a specific column.
    /// Individual cells can still override this via <see cref="ConsoleTableCell"/>.
    /// </summary>
    /// <param name="columnIndex">The zero-based column index.</param>
    /// <param name="alignment">The desired alignment.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable SetAlignment(int columnIndex, TextAlign alignment)
    {
        ValidateColumnIndex(columnIndex);
        _columns[columnIndex].Alignment = alignment;
        return this;
    }

    /// <summary>
    /// Sets the table border style.
    /// </summary>
    /// <param name="style">The desired table style.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable SetStyle(TableStyle style)
    {
        _style = style;
        return this;
    }

    /// <summary>
    /// Sets the foreground color for the header row.
    /// Only applies when printing to the console via <see cref="Print()"/>.
    /// </summary>
    /// <param name="color">The console color for the header.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable SetHeaderColor(ConsoleColor color)
    {
        _headerColor = color;
        return this;
    }

    /// <summary>
    /// Sets the foreground color for all cells in a specific column.
    /// Only applies when printing to the console via <see cref="Print()"/>.
    /// </summary>
    /// <param name="columnIndex">The zero-based column index.</param>
    /// <param name="color">The console color for the column.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable SetColumnColor(int columnIndex, ConsoleColor color)
    {
        ValidateColumnIndex(columnIndex);
        _columns[columnIndex].Color = color;
        return this;
    }

    /// <summary>
    /// Sets the minimum width for a specific column.
    /// </summary>
    /// <param name="columnIndex">The zero-based column index.</param>
    /// <param name="minWidth">The minimum width in characters.</param>
    /// <returns>This <see cref="ConsoleTable"/> instance for chaining.</returns>
    public ConsoleTable SetMinWidth(int columnIndex, int minWidth)
    {
        ValidateColumnIndex(columnIndex);
        _columns[columnIndex].MinWidth = minWidth;
        return this;
    }

    /// <summary>
    /// Prints the table to standard output (<see cref="Console.Out"/>) with color support.
    /// </summary>
    public void Print()
    {
        Print(Console.Out);
    }

    /// <summary>
    /// Prints the table to the specified <see cref="TextWriter"/>.
    /// Color support is enabled only when writing to <see cref="Console.Out"/> or <see cref="Console.Error"/>.
    /// </summary>
    /// <param name="writer">The output writer.</param>
    public void Print(TextWriter writer)
    {
        if (writer == null) throw new ArgumentNullException(nameof(writer));

        var border = BorderChars.FromStyle(_style);
        var renderer = new ConsoleTableRenderer(_columns, _rows, border);

        bool supportsColor = writer == Console.Out || writer == Console.Error;
        renderer.RenderTo(writer, _headerColor, supportsColor);
        writer.WriteLine();
    }

    /// <summary>
    /// Returns the table rendered as a plain-text string (no color codes).
    /// </summary>
    public override string ToString()
    {
        var border = BorderChars.FromStyle(_style);
        var renderer = new ConsoleTableRenderer(_columns, _rows, border);
        return renderer.Render();
    }

    private void ValidateColumnIndex(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= _columns.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(columnIndex),
                $"Column index must be between 0 and {_columns.Count - 1}.");
        }
    }
}
