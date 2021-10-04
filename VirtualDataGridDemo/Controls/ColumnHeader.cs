using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace VirtualDataGridDemo.Controls
{
    public class ColumnHeader : Decorator
    {
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            
            context.DrawRectangle(Brushes.LightGray, new ImmutablePen(Brushes.DarkGray, 1D), new Rect(new Point(), Bounds.Size));
        }
    }
}
