using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VirtualDataGridDemo.Controls;

namespace VirtualDataGridDemo
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
            var spreadsheet = this.FindControl<Spreadsheet>("Spreadsheet");
            var columnWidth = 130;
            var rowHeight = 28;
            var columns = new List<Column>();
            var rows = new List<Row>();

            for (var c = 0; c < 1_000_000; c++)
            {
                var column = new Column
                {
                    Header = $"{c}",
                    Width = columnWidth,
                    Index = c
                };
                columns.Add(column);
            }

            for (var r = 0; r < 1_000_000; r++)
            {
                var row = new Row
                {
                    Header = $"{r}",
                    Height = rowHeight,
                    Index = r
                };
                rows.Add(row);
            }

            spreadsheet.RowHeadersWidth = 130;
            spreadsheet.ColumnHeadersHeight = 28;

            spreadsheet.Columns.AddRange(columns);
            spreadsheet.Rows.AddRange(rows);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
