using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SpreadsheetDemo
{
    public class OpenXmlResult
    {
        public List<List<object?>> Items;
        public List<Spreadsheet.Column> Columns;
        public List<Spreadsheet.Row> Rows;
    }
    
    public class OpenXmlReader
    {
        private static string? ToString(Cell c, SharedStringTablePart? stringTable)
        {
            if (c.DataType is null)
            {
                return c.CellValue?.Text;
            }

            switch (c.DataType.Value)
            {
                case CellValues.SharedString:
                {
                    if (stringTable is { })
                    {
                        int index = int.Parse(c.InnerText);
                        var value = stringTable.SharedStringTable.ElementAt(index).InnerText;
                        return value;
                    }
                }
                    break;
                case CellValues.Boolean:
                {
                    return c.InnerText switch
                    {
                        "0" => "FALSE",
                        _ => "TRUE",
                    };
                }
                case CellValues.Number:
                    return c.InnerText;
                case CellValues.Error:
                    return c.InnerText;
                case CellValues.String:
                    return c.InnerText;
                case CellValues.InlineString:
                    return c.InnerText;
                case CellValues.Date:
                    return c.InnerText;
            }

            return null;
        }

        public static OpenXmlResult? Read(string path)
        {
            using var stream = File.OpenRead(path);
            
            var spreadsheetDocument = SpreadsheetDocument.Open(stream, false);

            var workbookPart = spreadsheetDocument.WorkbookPart;
            if (workbookPart is null)
            {
                return null;
            }

            var worksheetPart = workbookPart.WorksheetParts.First();

            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

            var result = new OpenXmlResult
            {
                Items = new List<List<object?>>(),
                Columns = new List<Spreadsheet.Column>(),
                Rows = new List<Spreadsheet.Row>(),
            };

            var columns = worksheetPart.Worksheet?.GetFirstChild<Columns>()?.Select(x => x as Column).ToList();
            
            if (columns is { })
            {
                for (var c = 0; c < columns.Count; c++)
                {
                    var column = new Spreadsheet.Column
                    {
                        Header = $"{c}",
                        // TODO:
                        // Width = columns[c]?.Width ?? 0.0, 
                        Width = 130, 
                        Index = c
                    };
                    result.Columns.Add(column);
                }
            }

            foreach (var row in sheetData.Elements<Row>())
            {
                var fields = new List<object?>();

                foreach (var c in row.Elements<Cell>())
                {
                    var field = ToString(c, stringTable);
                    fields.Add(field);
                }

                result.Items.Add(fields);
            }

            for (var r = 0; r < result.Items.Count; r++)
            {
                var row = new Spreadsheet.Row
                {
                    Header = $"{r}",
                    // TODO:
                    Height = 28,
                    Index = r
                };
                result.Rows.Add(row);
            }

            spreadsheetDocument.Close();

            return result;
        }
    }
}
