using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class RowHeadersPresenter : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(RowHeadersPresenter);

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<RowHeadersPresenterItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }
}
