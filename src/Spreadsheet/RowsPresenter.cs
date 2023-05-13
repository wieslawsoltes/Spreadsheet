using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace Spreadsheet;

public class RowsPresenter : ListBox, IStyleable
{
    Type IStyleable.StyleKey => typeof(RowsPresenter);

    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return new RowsPresenterItem();
    }

    protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
    {
        return NeedsContainer<RowsPresenterItem>(item, out recycleKey);
    }
}
