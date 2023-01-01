using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEventAggregator.Server.Models;

public interface IHubClient
{
    /// <summary>
    /// Sends the message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    Task SendMessage(object message);
}
