using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class RowHeadersItemsRepeater : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(RowHeadersItemsRepeater);

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<RowHeaderItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }
    /*public class RowHeadersItemsRepeater : ItemsRepeater
    {
    }*/
}
