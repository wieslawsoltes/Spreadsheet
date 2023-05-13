using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Spreadsheet;
using SpreadsheetSample.OpenXml;

namespace SpreadsheetSample;

public partial class MainView : UserControl
{
    public ObservableCollection<OpenXmlResult> Results { get; set; }

    public MainView()
    {
        InitializeComponent();

        Results = new ObservableCollection<OpenXmlResult>();
            
        DemoSpreadsheet();

        DataContext = this;
    }

    public async Task OpenSpreadsheet()
    {
        var dlg = new OpenFileDialog() { Title = "Open" };
        dlg.Filters.Add(new FileDialogFilter() { Name = "XLSX Files", Extensions = { "xlsx" } });
        dlg.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
        var window = this.GetVisualRoot() as Window;
        if (window is null)
        {
            return;
        }
        var paths = await dlg.ShowAsync(window);
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
        if (Application.Current?.ApplicationLifetime is IControlledApplicationLifetime lifetime)
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
            Columns = new List<Column>(),
            Rows = new List<Row>(),
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
}

