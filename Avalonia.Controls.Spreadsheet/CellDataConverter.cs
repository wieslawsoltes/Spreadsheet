using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.Controls.Spreadsheet
{
    public class CellDataConverter : IMultiValueConverter
    {
        public static CellDataConverter Instance = new();

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count == 3 
                && values[0] is List<List<object>> items
                && values[1] is int columnIndex
                && values[2] is int rowIndex)
            {
                return items[rowIndex][columnIndex];
            }
            return AvaloniaProperty.UnsetValue;
        }
    }
}
