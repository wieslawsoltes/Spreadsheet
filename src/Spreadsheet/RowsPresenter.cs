using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace Spreadsheet
{
    public class RowsPresenter : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(RowsPresenter);

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<RowsPresenterItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }
}
