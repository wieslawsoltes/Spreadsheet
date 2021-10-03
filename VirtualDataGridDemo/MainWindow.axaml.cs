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
            var rowsItemsRepeater = this.FindControl<RowsItemsRepeater>("RowsItemsRepeater");

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
            
            var columnHeadersScrollViewer = this.FindControl<ScrollViewer>("ColumnHeadersScrollViewer");
            var rowHeadersScrollViewer = this.FindControl<ScrollViewer>("RowHeadersScrollViewer");
            var rowsItemsScrollViewer = this.FindControl<ScrollViewer>("RowsItemsScrollViewer");

            rowsItemsScrollViewer.ScrollChanged += (_, _) =>
            {
                var offset = rowsItemsScrollViewer.Offset;

                columnHeadersScrollViewer.Offset = new Vector(offset.X, 0);
                rowHeadersScrollViewer.Offset = new Vector(0, offset.Y);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
