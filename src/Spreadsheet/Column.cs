using Avalonia;

namespace Spreadsheet;

public class Column : AvaloniaObject
{
    public static readonly StyledProperty<object?> HeaderProperty = 
        AvaloniaProperty.Register<Column, object?>(nameof(Header));

    public static readonly StyledProperty<double> WidthProperty = 
        AvaloniaProperty.Register<Column, double>(nameof(Width));

    public static readonly StyledProperty<int> IndexProperty = 
        AvaloniaProperty.Register<Column, int>(nameof(Index));

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public double Width
    {
        get => GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    public int Index
    {
        get => GetValue(IndexProperty);
        set => SetValue(IndexProperty, value);
    }
}