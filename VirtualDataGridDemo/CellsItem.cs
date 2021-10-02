using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace VirtualDataGridDemo
{
    public class CellsItem : ListBoxItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBoxItem);
    }
}