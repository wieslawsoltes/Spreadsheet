namespace Avalonia.Controls.Spreadsheet
{
    public class Row : AvaloniaObject
    {
        public static readonly StyledProperty<object?> HeaderProperty = 
            AvaloniaProperty.Register<Row, object?>(nameof(Header));

        public static readonly StyledProperty<double> HeightProperty = 
            AvaloniaProperty.Register<Row, double>(nameof(Height));

        public static readonly StyledProperty<int> IndexProperty = 
            AvaloniaProperty.Register<Row, int>(nameof(Index));

        public object? Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public double Height
        {
            get => GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public int Index
        {
            get => GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }
    }
}
