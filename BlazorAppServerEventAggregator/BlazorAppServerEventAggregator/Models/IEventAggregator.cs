namespace BlazorAppServerEventAggregator.Models;


/// <summary>
/// Interfaccia di implementazione per EventAggregator
/// </summary>
public interface IEventAggregator
{
    /// <summary>
    /// Publishes the specified event.
    /// </summary>
    /// <typeparam name="TEventType">The type of the event type.</typeparam>
    /// <param name="eventToPublish">The event to publish.</param>
    void Publish<TEventType>(TEventType eventToPublish);

    /// <summary>
    /// Register a subscribe to specified events.
    /// </summary>
    /// <param name="subscriber">The subscriber.</param>
    void Subscribe(Object subscriber);
}
