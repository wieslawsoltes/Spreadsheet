using System.Collections.Generic;

namespace SpreadsheetSample.OpenXml;

public class OpenXmlResult
{
    public string? Name { get; set; }

    public double RowHeadersWidth { get; set; }

    public double ColumnHeadersHeight { get; set; }

    public List<List<object?>>? Items { get; set; }

    public List<Spreadsheet.Column>? Columns { get; set; }

    public List<Spreadsheet.Row>? Rows { get; set; }
}
