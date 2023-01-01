namespace BlazorEventAggregator.Models;

public interface ISubscriber<TEventType>
{
    void OnEventRaised(TEventType e);
}