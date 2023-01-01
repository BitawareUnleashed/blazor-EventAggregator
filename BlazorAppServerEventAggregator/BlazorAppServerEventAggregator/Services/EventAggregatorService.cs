using BlazorAppServerEventAggregator.Models.Transport;
using BlazorAppServerEventAggregator.Models;

namespace BlazorAppServerEventAggregator.Services;

public class EventAggregatorService : ISubscriber<TimeValueChanged>
{
    private readonly IEventAggregator eventAggregator;

    public event EventHandler<string>? OnTimeSecondsChanged;

    void ISubscriber<TimeValueChanged>.OnEventRaised(TimeValueChanged e)
    {
        OnTimeSecondsChanged?.Invoke(this, e.Value);
    }

    public EventAggregatorService(IEventAggregator ea)
    {
        eventAggregator = ea;
        eventAggregator.Subscribe(this);
    }
}
