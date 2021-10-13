using System;
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
            
            var rowsItemsRepeater = this.FindControl<RowsItemsRepeater>("RowsItemsRepeater");
            var rowHeadersItemsRepeater = this.FindControl<RowHeadersItemsRepeater>("RowHeadersItemsRepeater");
            var columnHeadersScrollViewer = this.FindControl<ScrollViewer>("ColumnHeadersScrollViewer");

            var columnWidth = 130;
            var rowHeight = 28;

            var columns = new List<Column>();
            var rows = new List<Row>();
            
            for (var c = 0; c < 1_000_000; c++)
            {
                var column = new Column()
                {
                    Header = $"{c}",
                    Width = columnWidth,
                    Index = c
                };
                columns.Add(column);
            }

            for (var r = 0; r < 1_000_000; r++)
            {
                var row = new Row()
                {
                    Header = $"{r}",
                    Height = rowHeight,
                    Index = r
                };
                rows.Add(row);
            }
 
            rowsItemsRepeater.Columns.AddRange(columns);
            rowsItemsRepeater.Rows.AddRange(rows);

            rowsItemsRepeater.TemplateApplied += (_, _) =>
            {
                if (rowsItemsRepeater.Scroll is ScrollViewer scrollViewer)
                {
                    scrollViewer.ScrollChanged += (_, _) =>
                    {
                        var (x, y) = rowsItemsRepeater.Scroll.Offset;

                        var columnsCount = (double)columns.Count;
                        var rowsCount = (double)rows.Count;

                        var columnIndex = (int)Math.Round(x / (rowsItemsRepeater.Scroll.Extent.Width / columnsCount), 0);
                        var ox = columnIndex * (rowsItemsRepeater.Scroll.Extent.Width / columnsCount);

                        var rowIndex = (int)Math.Round(y / (rowsItemsRepeater.Scroll.Extent.Height / rowsCount), 0);
                        var oy = rowIndex * (rowsItemsRepeater.Scroll.Extent.Height / rowsCount);

                        rowsItemsRepeater.Scroll.Offset = new Vector(ox, oy);
                        rowHeadersItemsRepeater.Scroll.Offset = new Vector(0, oy);
                        columnHeadersScrollViewer.Offset = new Vector(ox, 0);
                    };
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
