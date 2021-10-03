using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace VirtualDataGridDemo.Controls
{
    public class Cell : Decorator
    {
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            
            context.DrawRectangle(Brushes.Transparent, new ImmutablePen(Brushes.Black, 1D), new Rect(new Point(), Bounds.Size));
        }
    }
}
