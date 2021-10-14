using System.Collections.Generic;
using DocumentFormat.OpenXml;

namespace SpreadsheetDemo
{
    public class OpenXmlResult
    {
        public string? Name { get; set; }

        public List<List<object?>>? Items { get; set; }

        public List<Spreadsheet.Column>? Columns { get; set; }

        public List<Spreadsheet.Row>? Rows { get; set; }
    }
}
