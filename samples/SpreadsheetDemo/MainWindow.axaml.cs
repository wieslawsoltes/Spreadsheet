using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Spreadsheet;

namespace SpreadsheetDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            Renderer.DrawFps = true;
            DemoSpreadsheet();
        }

        public async Task OpenSpreadsheet()
        {
            var dlg = new OpenFileDialog() { Title = "Open" };
            dlg.Filters.Add(new FileDialogFilter() { Name = "XLSX Files", Extensions = { "xlsx" } });
            dlg.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
            var paths = await dlg.ShowAsync(this);
            var path = paths?.FirstOrDefault();
            if (path is { })
            {
                var results = OpenXmlReader.Read(path, 130, 28);
                if (results is { })
                {
                    var result = results.FirstOrDefault();
                    if (result is { })
                    {
                        var spreadsheet = this.FindControl<SpreadsheetControl>("Spreadsheet");

                        spreadsheet.Columns.Clear();
                        spreadsheet.Rows.Clear();
                        spreadsheet.Items = null;

                        spreadsheet.RowHeadersWidth = 130;
                        spreadsheet.ColumnHeadersHeight = 28;

                        spreadsheet.Columns.AddRange(result.Columns);
                        spreadsheet.Rows.AddRange(result.Rows);
                        spreadsheet.Items = result.Items;
                    }
                }
            }
        }
        
        private void DemoSpreadsheet()
        {
            var spreadsheet = this.FindControl<SpreadsheetControl>("Spreadsheet");
            var columnWidth = 130;
            var rowHeight = 28;
            var columns = new List<Column>();
            var rows = new List<Row>();
            var items = new List<List<object?>>();

            var columnsCount = 100;
            var rowsCount = 10_000;

            for (var r = 0; r < rowsCount; r++)
            {
                var row = new Row
                {
                    Header = $"{r}",
                    Height = rowHeight,
                    Index = r
                };
                rows.Add(row);
            }

            for (var c = 0; c < columnsCount; c++)
            {
                var column = new Column
                {
                    Header = $"{c}",
                    Width = columnWidth,
                    Index = c
                };
                columns.Add(column);
            }

            for (var r = 0; r < rowsCount; r++)
            {
                items.Insert(r, new List<object?>());

                for (var c = 0; c < columnsCount; c++)
                {
                    items[r].Insert(c, $"Items[{r}][{c}]");
                }
            }

            spreadsheet.RowHeadersWidth = 130;
            spreadsheet.ColumnHeadersHeight = 28;

            spreadsheet.Columns.AddRange(columns);
            spreadsheet.Rows.AddRange(rows);

            spreadsheet.Items = items;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
