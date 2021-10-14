using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SpreadsheetDemo
{
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

        public static List<List<object?>>? Read(Stream stream)
        {
            var spreadsheetDocument = SpreadsheetDocument.Open(stream, false);

            var workbookPart = spreadsheetDocument.WorkbookPart;
            if (workbookPart is null)
            {
                return null;
            }

            var items = new List<List<object?>>();

            var worksheetPart = workbookPart.WorksheetParts.First();

            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

            foreach (var row in sheetData.Elements<Row>())
            {
                var fields = row.Elements<Cell>().Select(c => (object?)ToString(c, stringTable)).ToList();
                items.Add(fields);
            }

            spreadsheetDocument.Close();

            return items;
        }

        public static List<List<object?>>? Read(string path)
        {
            using var stream = File.OpenRead(path);
            var items = Read(stream)?.ToList();
            return items;
        }
    }
}
