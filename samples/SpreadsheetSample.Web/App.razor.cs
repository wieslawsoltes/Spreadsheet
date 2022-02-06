using Avalonia.ReactiveUI;
using Avalonia.Web.Blazor;

namespace SpreadsheetSample.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<SpreadsheetSample.App>()
            .UseReactiveUI()
            .SetupWithSingleViewLifetime();
    }
}
