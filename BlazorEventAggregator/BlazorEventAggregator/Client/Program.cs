using BlazorEventAggregator.Client;
using BlazorEventAggregator.Client.Services;
using BlazorEventAggregator.Models;
using BlazorEventAggregator.Shared.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorEventAggregator.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorEventAggregator.ServerAPI"));

builder.Services.AddSingleton<IEventAggregator, EventAggregator>();
builder.Services.AddSingleton<EventAggregatorService>();

await builder.Build().RunAsync();
