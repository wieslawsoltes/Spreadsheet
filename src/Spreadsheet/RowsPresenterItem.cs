using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Spreadsheet;

public class RowsPresenterItem : ListBoxItem, IStyleable
{
    Type IStyleable.StyleKey => typeof(ListBoxItem);
}