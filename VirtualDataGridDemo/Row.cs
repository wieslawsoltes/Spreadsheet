using Avalonia;

namespace VirtualDataGridDemo
{
    public class Row : AvaloniaObject
    {
        public static readonly StyledProperty<object?> HeaderProperty = 
            AvaloniaProperty.Register<Row, object?>(nameof(Header));

        public static readonly StyledProperty<double> HeightProperty = 
            AvaloniaProperty.Register<Row, double>(nameof(Height));

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
    }
}