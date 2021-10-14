using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SpreadsheetDemo
{
    public class OpenXmlReader
    {
        private static string? ToString(Cell cell, SharedStringTablePart? stringTable)
        {
            if (cell.DataType is null)
            {
                return cell.CellValue?.Text;
            }

            switch (cell.DataType.Value)
            {
                case CellValues.SharedString:
                {
                    if (stringTable is { })
                    {
                        int index = int.Parse(cell.InnerText);
                        var value = stringTable.SharedStringTable.ElementAt(index).InnerText;
                        return value;
                    }
                }
                    break;
                case CellValues.Boolean:
                {
                    return cell.InnerText switch
                    {
                        "0" => "FALSE",
                        _ => "TRUE",
                    };
                }
                case CellValues.Number:
                    return cell.InnerText;
                case CellValues.Error:
                    return cell.InnerText;
                case CellValues.String:
                    return cell.InnerText;
                case CellValues.InlineString:
                    return cell.InnerText;
                case CellValues.Date:
                    return cell.InnerText;
            }

            return null;
        }

        public static List<OpenXmlResult>? Read(string path, double columnWidth, double rowHeight)
        {
            using var stream = File.OpenRead(path);
            
            var spreadsheetDocument = SpreadsheetDocument.Open(stream, false);

            var workbookPart = spreadsheetDocument.WorkbookPart;
            if (workbookPart is null)
            {
                return null;
            }

            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

            var results = new List<OpenXmlResult>();

            var sheets = workbookPart.Workbook.Sheets?.ToList();

            var i = 0;
            foreach (var worksheetPart in workbookPart.WorksheetParts)
            {
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

                var name = "";
                if (sheets is { })
                {
                    if (sheets[i] is Sheet sheet)
                    {
                        name = sheet.Name;
                    }
                }

                var result = new OpenXmlResult
                {
                    Name = name,
                    RowHeadersWidth = 130,
                    ColumnHeadersHeight = 28,
                    Items = new List<List<object?>>(),
                    Columns = new List<Spreadsheet.Column>(),
                    Rows = new List<Spreadsheet.Row>(),
                };

                var columns = worksheetPart.Worksheet.GetFirstChild<Columns>()?.Select(x => x as Column);
                if (columns is { })
                {
                    var c = 0;
                    foreach (var columnElement in columns)
                    {
                        var column = new Spreadsheet.Column
                        {
                            Header = $"{c}",
                            // TODO:
                            // Width = columnElement?.Width ?? 0.0, 
                            Width = columnWidth,
                            Index = c
                        };

                        result.Columns.Add(column);

                        c++;
                    }
                }

                if (sheetData is { })
                {
                    var r = 0;
                    foreach (var rowElement in sheetData.Elements<Row>())
                    {
                        var row = new Spreadsheet.Row
                        {
                            Header = $"{r}",
                            // TODO:
                            //Height = rowElement.Height ?? 0.0,
                            Height = rowHeight,
                            Index = r
                        };

                        result.Rows.Add(row);

                        var fields = new List<object?>();

                        foreach (var cellElement in rowElement.Elements<Cell>())
                        {
                            var field = ToString(cellElement, stringTable);
                            fields.Add(field);
                        }

                        result.Items.Add(fields);

                        r++;
                    }
                }

                if (columns is null)
                {
                    var fields = result.Items.FirstOrDefault();
                    if (fields is { })
                    {
                        var c = 0;
                        foreach (var field in fields)
                        {
                            var column = new Spreadsheet.Column
                            {
                                Header = $"{c}",
                                Width = columnWidth,
                                Index = c
                            };

                            result.Columns.Add(column);

                            c++;
                        }
                    }
                }
                
                results.Add(result);
                i++;
            }

            spreadsheetDocument.Close();

            return results;
        }
    }
}
