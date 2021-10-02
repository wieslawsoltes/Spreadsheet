using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Styling;

namespace VirtualDataGridDemo
{
    public class CellsItemsRepeater : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBox);
        
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<CellsItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            availableSize = base.MeasureOverride(availableSize.WithWidth(double.PositiveInfinity));

            return availableSize;
        }
    }
}