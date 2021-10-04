using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo.Controls
{
    public class CellsItemsRepeater :  ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(CellsItemsRepeater);
        
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<CellItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }
    }
}
