using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class RowItem : ListBoxItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBoxItem);
    }
}
