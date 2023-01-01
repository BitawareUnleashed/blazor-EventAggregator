using Microsoft.AspNetCore.SignalR;

namespace BlazorEventAggregator.Server.Models;

public class CommunicationHub:Hub<IHubClient>
{

}
