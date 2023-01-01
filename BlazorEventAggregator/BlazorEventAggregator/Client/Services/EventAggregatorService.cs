using BlazorEventAggregator.Models;
using BlazorEventAggregator.Shared.Models.Transport;

namespace BlazorEventAggregator.Client.Services;

public class EventAggregatorService : ISubscriber<TimeValueChanged>, ISubscriber<CounterValueChanged>
{
    private readonly IEventAggregator eventAggregator;

    public event EventHandler<string> OnTimeSecondsChanged;
    public event EventHandler<string> OnCounterValueChanged;



    void ISubscriber<TimeValueChanged>.OnEventRaised(TimeValueChanged e)
    {
        OnTimeSecondsChanged?.Invoke(this, e.Value);
    }

    void ISubscriber<CounterValueChanged>.OnEventRaised(CounterValueChanged e)
    {
        OnCounterValueChanged?.Invoke(this, e.Value.ToString());
    }



    public EventAggregatorService(IEventAggregator ea)
    {
        eventAggregator = ea;
        eventAggregator.Subscribe(this);
    }



}
