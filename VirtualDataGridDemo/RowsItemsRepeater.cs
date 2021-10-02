using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo
{
    public class RowsItemsRepeater : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBox);

        public static readonly DirectProperty<RowsItemsRepeater, AvaloniaList<Column>> ColumnsProperty = 
            AvaloniaProperty.RegisterDirect<RowsItemsRepeater, AvaloniaList<Column>>(
                "Columns", 
                o => o.Columns, 
                (o, v) => o.Columns = v);

        public static readonly DirectProperty<RowsItemsRepeater, AvaloniaList<Row>> RowsProperty = 
            AvaloniaProperty.RegisterDirect<RowsItemsRepeater, AvaloniaList<Row>>(
                "Rows", 
                o => o.Rows, 
                (o, v) => o.Rows = v);

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

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<RowItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }
}