using BlazorAppServerEventAggregator.Models.Transport;
using BlazorAppServerEventAggregator.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServerEventAggregator.Pages;

public partial class Index
{
    [CascadingParameter] public string DateTime { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
}
