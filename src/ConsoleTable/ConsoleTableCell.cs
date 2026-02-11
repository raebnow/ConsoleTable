namespace ConsoleTable;

/// <summary>
/// Represents a single cell value in a <see cref="ConsoleTable"/> row,
/// with an optional per-cell alignment override.
/// </summary>
/// <remarks>
/// When <see cref="Alignment"/> is <c>null</c>, the column's default alignment is used.
/// Plain values passed to <see cref="ConsoleTable.AddRow"/> are automatically wrapped
/// in a <see cref="ConsoleTableCell"/> with no alignment override.
/// </remarks>
/// <example>
/// <code>
/// table.AddRow("Name", new ConsoleTableCell(42, TextAlign.Center), "Info");
/// </code>
/// </example>
public sealed class ConsoleTableCell
{
    /// <summary>
    /// Gets the display text of this cell.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets the per-cell alignment override.
    /// When <c>null</c>, the column's <see cref="ConsoleTableColumn.Alignment"/> is used.
    /// </summary>
    public TextAlign? Alignment { get; }

    /// <summary>
    /// Creates a cell with a value and an optional alignment override.
    /// </summary>
    /// <param name="value">The cell value. Converted to string via <see cref="object.ToString"/>.</param>
    /// <param name="alignment">
    /// The alignment for this specific cell, or <c>null</c> to use the column default.
    /// </param>
    public ConsoleTableCell(object? value, TextAlign? alignment = null)
    {
        Text = value?.ToString() ?? string.Empty;
        Alignment = alignment;
    }

    /// <summary>
    /// Implicitly converts a string to a <see cref="ConsoleTableCell"/> with no alignment override.
    /// </summary>
    public static implicit operator ConsoleTableCell(string value) => new ConsoleTableCell(value);

    /// <summary>
    /// Implicitly converts an int to a <see cref="ConsoleTableCell"/> with no alignment override.
    /// </summary>
    public static implicit operator ConsoleTableCell(int value) => new ConsoleTableCell(value);
}
