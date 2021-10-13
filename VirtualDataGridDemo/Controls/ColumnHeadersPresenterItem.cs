using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class ColumnHeaderItem : ListBoxItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBoxItem);
    }
}
