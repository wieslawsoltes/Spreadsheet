using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class RowsPresenterItem : ListBoxItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBoxItem);
    }
}
