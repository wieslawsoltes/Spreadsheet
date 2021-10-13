using System;
using Avalonia.Styling;

namespace Avalonia.Controls.Spreadsheet
{
    public class RowsPresenterItem : ListBoxItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBoxItem);
    }
}
