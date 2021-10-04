using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    /*public class ColumnHeadersItemsRepeater : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ColumnHeadersItemsRepeater);

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<ColumnHeaderItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }*/
    public class ColumnHeadersItemsRepeater : ItemsRepeater
    {
    }
}
