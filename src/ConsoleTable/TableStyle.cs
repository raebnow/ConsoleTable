namespace ConsoleTable;

/// <summary>
/// Specifies the visual style of the table borders.
/// </summary>
public enum TableStyle
{
    /// <summary>Default ASCII style using |, -, and + characters.</summary>
    Default,

    /// <summary>Unicode box-drawing style using ─, │, ┌, ┐, └, ┘, etc.</summary>
    Unicode,

    /// <summary>Minimal style with column separators only, no horizontal rules between rows.</summary>
    Minimal
}

/// <summary>
/// Defines the border characters used for rendering a table.
/// </summary>
internal sealed class BorderChars
{
    public char Horizontal { get; }
    public char Vertical { get; }
    public char TopLeft { get; }
    public char TopRight { get; }
    public char BottomLeft { get; }
    public char BottomRight { get; }
    public char LeftJunction { get; }
    public char RightJunction { get; }
    public char TopJunction { get; }
    public char BottomJunction { get; }
    public char CrossJunction { get; }

    private BorderChars(
        char horizontal, char vertical,
        char topLeft, char topRight, char bottomLeft, char bottomRight,
        char leftJunction, char rightJunction,
        char topJunction, char bottomJunction, char crossJunction)
    {
        Horizontal = horizontal;
        Vertical = vertical;
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
        LeftJunction = leftJunction;
        RightJunction = rightJunction;
        TopJunction = topJunction;
        BottomJunction = bottomJunction;
        CrossJunction = crossJunction;
    }

    /// <summary>
    /// Gets the <see cref="BorderChars"/> instance for the specified <see cref="TableStyle"/>.
    /// </summary>
    public static BorderChars FromStyle(TableStyle style) => style switch
    {
        TableStyle.Unicode => new BorderChars(
            '─', '│',
            '┌', '┐', '└', '┘',
            '├', '┤',
            '┬', '┴', '┼'),

        TableStyle.Minimal => new BorderChars(
            ' ', '|',
            ' ', ' ', ' ', ' ',
            ' ', ' ',
            ' ', ' ', ' '),

        // Default
        _ => new BorderChars(
            '-', '|',
            '-', '-', '-', '-',
            '-', '-',
            '-', '-', '-'),
    };
}
