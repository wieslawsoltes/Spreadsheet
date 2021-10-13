using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class RowsItemsRepeater : ListBox, IStyleable
    {
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
        
        Type IStyleable.StyleKey => typeof(RowsItemsRepeater);

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<RowsPresenterItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }
}
