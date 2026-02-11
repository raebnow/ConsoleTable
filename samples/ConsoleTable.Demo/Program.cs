using ConsoleTable;

// ============================================================
// Demo 1: Basic table with the user's example data
// ============================================================
Console.WriteLine("=== Demo 1: Bot Status Table (Default Style) ===\n");

var table = new ConsoleTable.ConsoleTable(
    new ConsoleTableColumn("Bot Name", TextAlign.Left),
    new ConsoleTableColumn("Date Time", TextAlign.Center),
    new ConsoleTableColumn("Remaining Count", TextAlign.Right),
    new ConsoleTableColumn("Total Count", TextAlign.Right),
    new ConsoleTableColumn("Progress", TextAlign.Right),
    new ConsoleTableColumn("Filename", TextAlign.Right),
    new ConsoleTableColumn("Stopped", TextAlign.Right)
);

var rows = new (string bot, string dateTime, int remaining, int total, string filename)[]
{
    ("Bot [00]", "02/11/2026 02:29:50",  256,  303, "profiledata/prd/tokyo-1/profiledata_20260205T193513_game-server-000-qpzb6_50514163206362881.zip"),
    ("Bot [01]", "02/11/2026 02:29:46",  152,  310, "profiledata/prd/tokyo-1/profiledata_20260205T193815_game-server-000-bftuz_50507424033576705.zip"),
    ("Bot [02]", "02/11/2026 02:29:51", 1305, 1405, "profiledata/prd/tokyo-1/profiledata_20260205T193150_game-server-000-7sxql_50675390708119105.zip"),
    ("Bot [03]", "02/11/2026 02:29:32",   93,  138, "profiledata/prd/tokyo-2/profiledata_20260205T105723_game-server-030-femt7_50685586121961473.zip"),
    ("Bot [04]", "02/11/2026 02:29:46",  156,  255, "profiledata/prd/tokyo-1/profiledata_20260205T193937_game-server-000-if7hw_50569651280868929.zip"),
    ("Bot [05]", "02/11/2026 02:29:46",  224,  310, "profiledata/prd/tokyo-1/profiledata_20260205T193523_game-server-000-53ph3_50507308807384513.zip"),
    ("Bot [06]", "02/11/2026 02:29:52",  839,  910, "profiledata/prd/tokyo-2/profiledata_20260205T102203_game-server-030-djnu3_50686534739331393.zip"),
    ("Bot [07]", "02/11/2026 02:29:53",   12,   14, "profiledata/prd/tokyo-2/profiledata_20260205T110119_game-server-031-bppat_50828563200903489.zip"),
    ("Bot [08]", "02/11/2026 02:29:52",    4,   20, "profiledata/prd/tokyo-2/profiledata_20260205T110129_game-server-030-3q77l_50821670667107137.zip"),
    ("Bot [09]", "02/11/2026 02:29:45",  222,  309, "profiledata/prd/tokyo-1/profiledata_20260205T193925_game-server-000-s4vj4_50507481612982017.zip"),
    ("Bot [10]", "02/11/2026 02:29:47",  239,  306, "profiledata/prd/tokyo-1/profiledata_20260205T192536_game-server-000-ckj3p_50511513681012289.zip"),
    ("Bot [11]", "02/11/2026 02:29:53",  265,  322, "profiledata/prd/tokyo-1/profiledata_20260205T190548_game-server-000-rqqxl_50492371263450561.zip"),
    ("Bot [12]", "02/11/2026 02:29:36",  222,  322, "profiledata/prd/tokyo-1/profiledata_20260205T193522_game-server-000-mscmy_50493504010516033.zip"),
    ("Bot [13]", "02/11/2026 02:29:31",  280,  306, "profiledata/prd/tokyo-1/profiledata_20260205T190957_game-server-000-h2m7c_50510937669146369.zip"),
    ("Bot [14]", "02/11/2026 02:29:32",  267,  306, "profiledata/prd/tokyo-1/profiledata_20260205T191234_game-server-000-bcazj_50511033634321985.zip"),
    ("Bot [15]", "02/11/2026 02:29:45",  283,  307, "profiledata/prd/tokyo-1/profiledata_20260205T190436_game-server-000-nhqio_50508422445428161.zip"),
};

foreach (var r in rows)
{
    int pct = (int)((double)r.remaining / r.total * 100);
    table.AddRow(
        r.bot,
        r.dateTime,
        r.remaining,
        r.total,
        $"{pct}%",
        r.filename,
        "Stopped = False"
    );
}

table.Print();

// ============================================================
// Demo 2: Simple table with string headers
// ============================================================
Console.WriteLine("\n\n=== Demo 2: Simple Table ===\n");

var simple = new ConsoleTable.ConsoleTable("Name", "Age", "City");
simple.AddRow("Alice", 30, "Seoul");
simple.AddRow("Bob", 25, "Tokyo");
simple.AddRow("Charlie", 35, "New York");
simple.SetAlignment(1, TextAlign.Right);
simple.SetAlignment(2, TextAlign.Center);
simple.Print();

// ============================================================
// Demo 3: Unicode style
// ============================================================
Console.WriteLine("\n\n=== Demo 3: Unicode Style ===\n");

var unicode = new ConsoleTable.ConsoleTable("ID", "Product", "Price", "Stock");
unicode.SetStyle(TableStyle.Unicode);
unicode.SetAlignment(0, TextAlign.Right);
unicode.SetAlignment(2, TextAlign.Right);
unicode.SetAlignment(3, TextAlign.Right);
unicode.AddRow(1, "Keyboard", "$49.99", 150);
unicode.AddRow(2, "Mouse", "$29.99", 300);
unicode.AddRow(3, "Monitor", "$399.99", 45);
unicode.AddRow(4, "USB Cable", "$9.99", 1200);
unicode.Print();

// ============================================================
// Demo 4: Center alignment
// ============================================================
Console.WriteLine("\n\n=== Demo 4: Center Alignment ===\n");

var centered = new ConsoleTable.ConsoleTable(
    new ConsoleTableColumn("Status", TextAlign.Center),
    new ConsoleTableColumn("Count", TextAlign.Center),
    new ConsoleTableColumn("Percentage", TextAlign.Center)
);
centered.AddRow("Success", 1234, "82.3%");
centered.AddRow("Failed", 156, "10.4%");
centered.AddRow("Pending", 110, "7.3%");
centered.Print();

// ============================================================
// Demo 5: ToString() usage
// ============================================================
Console.WriteLine("\n\n=== Demo 5: ToString() ===\n");

var toStr = new ConsoleTable.ConsoleTable("Key", "Value");
toStr.AddRow("Version", "1.0.0");
toStr.AddRow("Author", "ConsoleTable");

string tableString = toStr.ToString();
Console.WriteLine("Table as string variable:");
Console.WriteLine(tableString);

// ============================================================
// Demo 6: Color support
// ============================================================
Console.WriteLine("\n\n=== Demo 6: Color Support ===\n");

var colorTable = new ConsoleTable.ConsoleTable("Level", "Message", "Count");
colorTable.SetHeaderColor(ConsoleColor.Cyan);
colorTable.SetColumnColor(0, ConsoleColor.Yellow);
colorTable.SetColumnColor(2, ConsoleColor.Green);
colorTable.SetAlignment(2, TextAlign.Right);
colorTable.AddRow("INFO", "Application started", 42);
colorTable.AddRow("WARN", "Memory usage high", 7);
colorTable.AddRow("ERROR", "Connection timeout", 3);
colorTable.Print();

// ============================================================
// Demo 7: Per-cell alignment override
// ============================================================
Console.WriteLine("\n\n=== Demo 7: Per-Cell Alignment Override ===\n");

var cellAlign = new ConsoleTable.ConsoleTable(
    new ConsoleTableColumn("Name", TextAlign.Left),
    new ConsoleTableColumn("Score", TextAlign.Right),
    new ConsoleTableColumn("Grade", TextAlign.Right),
    new ConsoleTableColumn("Note", TextAlign.Left)
);
cellAlign.AddRow("Alice", 95, "A+", "Excellent");
cellAlign.AddRow("Bob", new ConsoleTableCell(72, TextAlign.Center), "B", new ConsoleTableCell("Needs review", TextAlign.Center));
cellAlign.AddRow("Charlie", 88, new ConsoleTableCell("A", TextAlign.Left), "Good");
cellAlign.AddRow(new ConsoleTableCell("Diana", TextAlign.Center), 91, "A", new ConsoleTableCell("Great", TextAlign.Right));
cellAlign.Print();
