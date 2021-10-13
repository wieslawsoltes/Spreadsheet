using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace Avalonia.Controls.Spreadsheet
{
    public class RowHeader : Decorator
    {
        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var thickness = 1.0;
            var offset = thickness * 0.5;

            context.DrawRectangle(
                Brushes.LightGray, 
                new ImmutablePen(Brushes.DarkGray, thickness), 
                new Rect(new Point(offset, offset), new Size( Bounds.Size.Width + offset,  Bounds.Size.Height + offset)));
        }
    }
}
