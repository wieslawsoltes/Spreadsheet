using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Spreadsheet
{
    public class SpreadsheetControl : TemplatedControl
    {
        public static readonly DirectProperty<SpreadsheetControl, AvaloniaList<Column>> ColumnsProperty = 
            AvaloniaProperty.RegisterDirect<SpreadsheetControl, AvaloniaList<Column>>(
                "Columns", 
                o => o.Columns, 
                (o, v) => o.Columns = v);

        public static readonly DirectProperty<SpreadsheetControl, AvaloniaList<Row>> RowsProperty = 
            AvaloniaProperty.RegisterDirect<SpreadsheetControl, AvaloniaList<Row>>(
                "Rows", 
                o => o.Rows, 
                (o, v) => o.Rows = v);

        public static readonly StyledProperty<double> RowHeadersWidthProperty =
            AvaloniaProperty.Register<SpreadsheetControl, double>(nameof(RowHeadersWidth), 32);

        public static readonly StyledProperty<double> ColumnHeadersHeightProperty = 
            AvaloniaProperty.Register<SpreadsheetControl, double>(nameof(ColumnHeadersHeight), 32);

        public static readonly StyledProperty<List<List<object?>>?> ItemsProperty = 
            AvaloniaProperty.Register<SpreadsheetControl, List<List<object?>>?>(nameof(Items));

        private AvaloniaList<Column> _columns = new();
        private AvaloniaList<Row> _rows = new();
        private RowsPresenter? _rowsItemsRepeater;
        private RowHeadersPresenter? _rowHeadersItemsRepeater;
        private ScrollViewer? _columnHeadersScrollViewer;

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

        public List<List<object?>>? Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            _rowsItemsRepeater = e.NameScope.Find<RowsPresenter>("PART_RowsItemsRepeater");
            _rowHeadersItemsRepeater = e.NameScope.Find<RowHeadersPresenter>("PART_RowHeadersItemsRepeater");
            _columnHeadersScrollViewer = e.NameScope.Find<ScrollViewer>("PART_ColumnHeadersScrollViewer");

            _rowsItemsRepeater.TemplateApplied += (_, _) =>
            {
                if (_rowsItemsRepeater.Scroll is ScrollViewer scrollViewer)
                {
                    scrollViewer.ScrollChanged += (_, _) =>
                    {
                        var (x, y) = _rowsItemsRepeater.Scroll.Offset;

                        var columnsCount = (double)Columns.Count;
                        var rowsCount = (double)Rows.Count;

                        var columnIndex = (int)Math.Round(x / (_rowsItemsRepeater.Scroll.Extent.Width / columnsCount), 0);
                        var ox = columnIndex * (_rowsItemsRepeater.Scroll.Extent.Width / columnsCount);

                        var rowIndex = (int)Math.Round(y / (_rowsItemsRepeater.Scroll.Extent.Height / rowsCount), 0);
                        var oy = rowIndex * (_rowsItemsRepeater.Scroll.Extent.Height / rowsCount);

                        _rowsItemsRepeater.Scroll.Offset = new Vector(ox, oy);
                        _rowHeadersItemsRepeater.Scroll.Offset = new Vector(0, oy);
                        _columnHeadersScrollViewer.Offset = new Vector(ox, 0);
                    };
                }
            };     
        }
    }
}
