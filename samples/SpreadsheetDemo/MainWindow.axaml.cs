using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Spreadsheet;

namespace SpreadsheetDemo
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<OpenXmlResult> Results { get; set; }

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            Renderer.DrawFps = true;

            Results = new ObservableCollection<OpenXmlResult>();
            
            DemoSpreadsheet();

            DataContext = this;
        }

        public async Task OpenSpreadsheet()
        {
            var dlg = new OpenFileDialog() { Title = "Open" };
            dlg.Filters.Add(new FileDialogFilter() { Name = "XLSX Files", Extensions = { "xlsx" } });
            dlg.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
            var paths = await dlg.ShowAsync(this);
            var path = paths?.FirstOrDefault();
            if (path is { })
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    var results = OpenXmlReader.Read(path, 130, 28);
                    if (results is { })
                    {
                        Results.Clear();

                        foreach (var result in results)
                        {
                            Results.Add(result);
                        }
                    }
                });
            }
        }

        public void CloseSpreadsheet()
        {
            Results.Clear();
        }

        public void Exit()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime.Shutdown();
            }
        }

        private void DemoSpreadsheet()
        {
            var result = new OpenXmlResult
            {
                Name = "Demo",
                RowHeadersWidth = 130,
                ColumnHeadersHeight = 28,
                Items = new List<List<object?>>(),
                Columns = new List<Spreadsheet.Column>(),
                Rows = new List<Spreadsheet.Row>(),
            };

            var columnWidth = 130;
            var rowHeight = 28;

            var columnsCount = 100;
            var rowsCount = 10_000;

            for (var r = 0; r < rowsCount; r++)
            {
                var row = new Row
                {
                    Header = $"{r}",
                    Height = rowHeight,
                    Index = r
                };
                result.Rows.Add(row);
            }

            for (var c = 0; c < columnsCount; c++)
            {
                var column = new Column
                {
                    Header = $"{c}",
                    Width = columnWidth,
                    Index = c
                };
                result.Columns.Add(column);
            }

            for (var r = 0; r < rowsCount; r++)
            {
                result.Items.Insert(r, new List<object?>());

                for (var c = 0; c < columnsCount; c++)
                {
                    result.Items[r].Insert(c, $"Items[{r}][{c}]");
                }
            }
            
            Results.Add(result);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
