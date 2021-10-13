using System;
using Avalonia.Styling;

namespace Avalonia.Controls.Spreadsheet
{
    public class RowHeadersPresenterItem : ListBoxItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBoxItem);
    }
}
