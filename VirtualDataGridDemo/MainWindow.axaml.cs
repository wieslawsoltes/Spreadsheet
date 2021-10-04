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
            //var columnHeadersItemsRepeater = this.FindControl<ColumnHeadersItemsRepeater>("ColumnHeadersItemsRepeater");
            var rowHeadersItemsRepeater = this.FindControl<RowHeadersItemsRepeater>("RowHeadersItemsRepeater");
            var columnHeadersScrollViewer = this.FindControl<ScrollViewer>("ColumnHeadersScrollViewer");
            //var rowHeadersScrollViewer = this.FindControl<ScrollViewer>("RowHeadersScrollViewer");

            var columnWidth = 130;
            var rowHeight = 28;

            var columns = new List<Column>();
            var rows = new List<Row>();
            
            for (var c = 0; c < 1_000; c++)
            {
                var column = new Column()
                {
                    Header = $"{c}",
                    Width = columnWidth,
                    Index = c
                };
                columns.Add(column);
            }

            for (var r = 0; r < 1_000; r++)
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
                        //columnHeadersItemsRepeater.Scroll.Offset = new Vector(x, 0);
                        rowHeadersItemsRepeater.Scroll.Offset = new Vector(0, y);
                        columnHeadersScrollViewer.Offset = new Vector(x, 0);
                        //rowHeadersScrollViewer.Offset = new Vector(0, y);
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
