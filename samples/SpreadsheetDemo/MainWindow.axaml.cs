using System.Collections.Generic;
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
            InitializeSpreadsheet();
        }

        private void InitializeSpreadsheet()
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
