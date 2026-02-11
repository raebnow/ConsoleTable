namespace ConsoleTable;

/// <summary>
/// Defines a column in a <see cref="ConsoleTable"/> with a name, alignment, and optional color.
/// </summary>
public sealed class ConsoleTableColumn
{
    /// <summary>
    /// Gets or sets the header name of the column.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the horizontal text alignment for cells in this column.
    /// Default is <see cref="TextAlign.Left"/>.
    /// </summary>
    public TextAlign Alignment { get; set; }

    /// <summary>
    /// Gets or sets the minimum width of the column in characters.
    /// The actual width will be the greater of this value and the longest cell content.
    /// </summary>
    public int MinWidth { get; set; }

    /// <summary>
    /// Gets or sets the foreground color for cells in this column.
    /// When <c>null</c>, the current console color is used.
    /// </summary>
    public ConsoleColor? Color { get; set; }

    /// <summary>
    /// Initializes a new <see cref="ConsoleTableColumn"/> with the specified header name.
    /// </summary>
    /// <param name="name">The header name.</param>
    /// <param name="alignment">The text alignment. Default is <see cref="TextAlign.Left"/>.</param>
    public ConsoleTableColumn(string name, TextAlign alignment = TextAlign.Left)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Alignment = alignment;
    }
}
