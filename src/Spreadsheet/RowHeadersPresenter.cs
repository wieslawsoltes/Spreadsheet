using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Spreadsheet;

public class RowHeadersPresenter : ListBox, IStyleable
{
    Type IStyleable.StyleKey => typeof(RowHeadersPresenter);

    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return new RowHeadersPresenterItem();
    }

    protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
    {
        return NeedsContainer<RowHeadersPresenterItem>(item, out recycleKey);
    }
}
