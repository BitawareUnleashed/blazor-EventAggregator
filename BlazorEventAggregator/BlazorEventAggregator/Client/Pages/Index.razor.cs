using Microsoft.AspNetCore.Components;

namespace BlazorEventAggregator.Client.Pages;

public partial class Index
{
    [CascadingParameter] public string DateTime { get; set; }


    protected override void OnInitialized()
    {
        
        base.OnInitialized();
    }

    private void Decrement()
    {

    }

    private void Increment()
    {

    }
}
