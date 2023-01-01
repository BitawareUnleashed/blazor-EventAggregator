namespace BlazorAppServerEventAggregator.Models;

public class EventAggregator : IEventAggregator
{
    private readonly Dictionary<Type, List<WeakReference>> subscribersListByType = new Dictionary<Type, List<WeakReference>>();

    private readonly object lockSubscriberDictionary = new object();


    #region IEventAggregator

    public void Publish<TEventType>(TEventType eventToPublish)
    {
        var subsriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));

        var subscribers = GetSubscriberList(subsriberType);

        List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

        foreach (var weakSubsriber in subscribers)
        {
            if (weakSubsriber.IsAlive)
            {
                var subscriber = (ISubscriber<TEventType>)weakSubsriber.Target;

                InvokeSubscriberEvent<TEventType>(eventToPublish, subscriber);
            }
            else
            {
                subsribersToBeRemoved.Add(weakSubsriber);
            }
        }


        if (subsribersToBeRemoved.Any())
        {
            lock (lockSubscriberDictionary)
            {
                foreach (var remove in subsribersToBeRemoved)
                {
                    subscribers.Remove(remove);
                }
            }
        }
    }

    public void Subscribe(object subscriber)
    {
        lock (lockSubscriberDictionary)
        {
            var subsriberTypes = subscriber.GetType().GetInterfaces()
                                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

            WeakReference weakReference = new WeakReference(subscriber);

            foreach (var subsriberType in subsriberTypes)
            {
                List<WeakReference> subscribers = GetSubscriberList(subsriberType);

                subscribers.Add(weakReference);
            }
        }
    }

    #endregion


    private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber)
    {
        //Synchronize the invocation of method 
        SynchronizationContext syncContext;
        if (SynchronizationContext.Current is null)
        {
            syncContext = new SynchronizationContext();
        }
        else
        {
            syncContext = SynchronizationContext.Current;
        }

        syncContext.Post(s => subscriber.OnEventRaised(eventToPublish), null);
    }

    private List<WeakReference> GetSubscriberList(Type subsriberType)
    {
        List<WeakReference>? subsribersList = null;

        lock (lockSubscriberDictionary)
        {
            bool found = subscribersListByType.TryGetValue(subsriberType, out subsribersList);

            if (!found)
            {
                //First time create the list.
                subsribersList = new List<WeakReference>();
                subscribersListByType.Add(subsriberType, subsribersList);
            }
        }
        return subsribersList;
    }
}
