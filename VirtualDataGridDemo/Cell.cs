using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace VirtualDataGridDemo
{
    public class Cell : Control
    {
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            
            context.DrawRectangle(null, new ImmutablePen(Brushes.Black, 1D), new Rect(new Point(), Bounds.Size));
        }
    }
}