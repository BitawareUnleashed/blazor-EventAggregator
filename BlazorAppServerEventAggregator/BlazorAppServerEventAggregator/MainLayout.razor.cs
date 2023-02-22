using System.Runtime.Intrinsics.X86;

namespace BlazorAppServerEventAggregator;

public partial class MainLayout
{
    /// <summary>
    /// Gets or sets the actual date time.
    /// </summary>
    /// <value>
    /// The actual date time.
    /// </value>
    public string ActualDateTime { get; set; }

    protected override Task OnInitializedAsync()
    {
        eventAggregatorService.OnTimeSecondsChanged += EventAggregatorService_OnTimeSecondsChanged;
        return base.OnInitializedAsync();
    }

    /// <summary>
    /// Events the aggregator service, on time seconds changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void EventAggregatorService_OnTimeSecondsChanged(object? sender, string e)
    {
        ActualDateTime = e;
        InvokeAsync(() => StateHasChanged());
    }
}
