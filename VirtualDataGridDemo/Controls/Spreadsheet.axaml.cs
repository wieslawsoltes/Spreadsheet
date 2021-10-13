using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace VirtualDataGridDemo.Controls
{
    public class Spreadsheet : TemplatedControl
    {
        public static readonly DirectProperty<Spreadsheet, AvaloniaList<Column>> ColumnsProperty = 
            AvaloniaProperty.RegisterDirect<Spreadsheet, AvaloniaList<Column>>(
                "Columns", 
                o => o.Columns, 
                (o, v) => o.Columns = v);

        public static readonly DirectProperty<Spreadsheet, AvaloniaList<Row>> RowsProperty = 
            AvaloniaProperty.RegisterDirect<Spreadsheet, AvaloniaList<Row>>(
                "Rows", 
                o => o.Rows, 
                (o, v) => o.Rows = v);

        public static readonly StyledProperty<double> RowHeadersWidthProperty =
            AvaloniaProperty.Register<Spreadsheet, double>(nameof(RowHeadersWidth), 32);

        public static readonly StyledProperty<double> ColumnHeadersHeightProperty = 
            AvaloniaProperty.Register<Spreadsheet, double>(nameof(ColumnHeadersHeight), 32);

        private AvaloniaList<Column> _columns = new();
        private AvaloniaList<Row> _rows = new();
        
        public AvaloniaList<Column> Columns
        {
            get => _columns;
            set => SetAndRaise(ColumnsProperty, ref _columns, value);
        }

        public AvaloniaList<Row> Rows
        {
            get => _rows;
            set => SetAndRaise(RowsProperty, ref _rows, value);
        }

        public double RowHeadersWidth
        {
            get => GetValue(RowHeadersWidthProperty);
            set => SetValue(RowHeadersWidthProperty, value);
        }

        public double ColumnHeadersHeight
        {
            get => GetValue(ColumnHeadersHeightProperty);
            set => SetValue(ColumnHeadersHeightProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            var rowsItemsRepeater = e.NameScope.Find<RowsPresenter>("RowsItemsRepeater");
            var rowHeadersItemsRepeater = e.NameScope.Find<RowHeadersPresenter>("RowHeadersItemsRepeater");
            var columnHeadersScrollViewer = e.NameScope.Find<ScrollViewer>("ColumnHeadersScrollViewer");

            rowsItemsRepeater.TemplateApplied += (_, _) =>
            {
                if (rowsItemsRepeater.Scroll is ScrollViewer scrollViewer)
                {
                    scrollViewer.ScrollChanged += (_, _) =>
                    {
                        var (x, y) = rowsItemsRepeater.Scroll.Offset;

                        var columnsCount = (double)Columns.Count;
                        var rowsCount = (double)Rows.Count;

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
    }
}

