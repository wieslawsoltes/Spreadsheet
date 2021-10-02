using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VirtualDataGridDemo.Controls;

namespace VirtualDataGridDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var rowsItemsRepeater = this.FindControl<RowsItemsRepeater>("RowsItemsRepeater");

            for (var c = 0; c < 1_000_000; c++)
            {
                var column = new Column()
                {
                    Header = $"{c}",
                    Width = 50
                };
                rowsItemsRepeater.Columns.Add(column);
            }

            for (var r = 0; r < 1_000_000; r++)
            {
                var row = new Row()
                {
                    Header = $"{r}",
                    Height = 24
                };
                rowsItemsRepeater.Rows.Add(row);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
