namespace ConsoleTable;

/// <summary>
/// Internal renderer that converts table data into formatted string output.
/// </summary>
internal sealed class ConsoleTableRenderer
{
    private readonly List<ConsoleTableColumn> _columns;
    private readonly List<ConsoleTableCell[]> _rows;
    private readonly BorderChars _border;
    private readonly int[] _columnWidths;

    public ConsoleTableRenderer(
        List<ConsoleTableColumn> columns,
        List<ConsoleTableCell[]> rows,
        BorderChars border)
    {
        _columns = columns;
        _rows = rows;
        _border = border;
        _columnWidths = CalculateColumnWidths();
    }

    /// <summary>
    /// Renders the full table as a string (no color codes).
    /// </summary>
    public string Render()
    {
        var sb = new StringBuilder();
        var separator = BuildSeparatorLine();

        sb.AppendLine(separator);
        sb.AppendLine(BuildHeaderRow());
        sb.AppendLine(separator);

        foreach (var row in _rows)
        {
            sb.AppendLine(BuildCellRow(row));
        }

        sb.Append(separator);
        return sb.ToString();
    }

    /// <summary>
    /// Renders the table directly to a <see cref="TextWriter"/> with optional color support.
    /// </summary>
    public void RenderTo(TextWriter writer, ConsoleColor? headerColor, bool supportsColor)
    {
        var separator = BuildSeparatorLine();

        writer.WriteLine(separator);

        if (supportsColor)
        {
            WriteColoredHeaderRow(writer, headerColor);
        }
        else
        {
            writer.WriteLine(BuildHeaderRow());
        }

        writer.WriteLine(separator);

        foreach (var row in _rows)
        {
            if (supportsColor)
            {
                WriteColoredCellRow(writer, row);
            }
            else
            {
                writer.WriteLine(BuildCellRow(row));
            }
        }

        writer.Write(separator);
    }

    private int[] CalculateColumnWidths()
    {
        var widths = new int[_columns.Count];

        for (int i = 0; i < _columns.Count; i++)
        {
            // Start with header length + 2 (padding on each side)
            widths[i] = _columns[i].Name.Length;

            // Check min width
            if (_columns[i].MinWidth > 0 && _columns[i].MinWidth > widths[i])
            {
                widths[i] = _columns[i].MinWidth;
            }

            // Check all row values
            foreach (var row in _rows)
            {
                if (i < row.Length)
                {
                    var cellLen = row[i].Text.Length;
                    if (cellLen > widths[i])
                    {
                        widths[i] = cellLen;
                    }
                }
            }
        }

        return widths;
    }

    private string BuildSeparatorLine()
    {
        var sb = new StringBuilder();
        // Full width separator: just repeat the horizontal char across the entire line width
        int totalWidth = 0;
        for (int i = 0; i < _columnWidths.Length; i++)
        {
            totalWidth += _columnWidths[i] + 2; // padding
        }
        totalWidth += _columnWidths.Length + 1; // separators: | col | col | = count + 1

        sb.Append(_border.Horizontal, totalWidth);
        return sb.ToString();
    }

    private string BuildHeaderRow()
    {
        var sb = new StringBuilder();
        sb.Append(_border.Vertical);

        for (int i = 0; i < _columns.Count; i++)
        {
            sb.Append(' ');
            var aligned = AlignText(_columns[i].Name, _columnWidths[i], _columns[i].Alignment);
            sb.Append(aligned);
            sb.Append(' ');
            sb.Append(_border.Vertical);
        }

        return sb.ToString();
    }

    private string BuildCellRow(ConsoleTableCell[] cells)
    {
        var sb = new StringBuilder();
        sb.Append(_border.Vertical);

        for (int i = 0; i < _columns.Count; i++)
        {
            sb.Append(' ');
            var cell = i < cells.Length ? cells[i] : new ConsoleTableCell(string.Empty);
            var alignment = cell.Alignment ?? _columns[i].Alignment;
            var aligned = AlignText(cell.Text, _columnWidths[i], alignment);
            sb.Append(aligned);
            sb.Append(' ');
            sb.Append(_border.Vertical);
        }

        return sb.ToString();
    }

    private void WriteColoredHeaderRow(TextWriter writer, ConsoleColor? headerColor)
    {
        var isConsole = writer == Console.Out || writer == Console.Error;
        var originalColor = ConsoleColor.Gray;

        if (isConsole)
        {
            try { originalColor = Console.ForegroundColor; }
            catch { /* ignore */ }
        }

        var sb = new StringBuilder();
        sb.Append(_border.Vertical);

        for (int i = 0; i < _columns.Count; i++)
        {
            sb.Append(' ');
            var aligned = AlignText(_columns[i].Name, _columnWidths[i], _columns[i].Alignment);

            if (isConsole && headerColor.HasValue)
            {
                writer.Write(sb.ToString());
                sb.Clear();

                try { Console.ForegroundColor = headerColor.Value; }
                catch { /* ignore */ }

                writer.Write(aligned);

                try { Console.ForegroundColor = originalColor; }
                catch { /* ignore */ }
            }
            else
            {
                sb.Append(aligned);
            }

            sb.Append(' ');
            sb.Append(_border.Vertical);
        }

        writer.WriteLine(sb.ToString());
    }

    private void WriteColoredCellRow(TextWriter writer, ConsoleTableCell[] cells)
    {
        var isConsole = writer == Console.Out || writer == Console.Error;
        var originalColor = ConsoleColor.Gray;

        if (isConsole)
        {
            try { originalColor = Console.ForegroundColor; }
            catch { /* ignore */ }
        }

        var sb = new StringBuilder();
        sb.Append(_border.Vertical);

        for (int i = 0; i < _columns.Count; i++)
        {
            sb.Append(' ');

            ConsoleColor? cellColor = _columns[i].Color;
            var cell = i < cells.Length ? cells[i] : new ConsoleTableCell(string.Empty);
            var alignment = cell.Alignment ?? _columns[i].Alignment;
            var aligned = AlignText(cell.Text, _columnWidths[i], alignment);

            if (isConsole && cellColor.HasValue)
            {
                writer.Write(sb.ToString());
                sb.Clear();

                try { Console.ForegroundColor = cellColor.Value; }
                catch { /* ignore */ }

                writer.Write(aligned);

                try { Console.ForegroundColor = originalColor; }
                catch { /* ignore */ }
            }
            else
            {
                sb.Append(aligned);
            }

            sb.Append(' ');
            sb.Append(_border.Vertical);
        }

        writer.WriteLine(sb.ToString());
    }

    internal static string AlignText(string text, int width, TextAlign alignment)
    {
        if (text.Length >= width)
            return text;

        switch (alignment)
        {
            case TextAlign.Right:
                return text.PadLeft(width);

            case TextAlign.Center:
                int totalPadding = width - text.Length;
                int leftPad = totalPadding / 2;
                int rightPad = totalPadding - leftPad;
                return new string(' ', leftPad) + text + new string(' ', rightPad);

            case TextAlign.Left:
            default:
                return text.PadRight(width);
        }
    }
}
