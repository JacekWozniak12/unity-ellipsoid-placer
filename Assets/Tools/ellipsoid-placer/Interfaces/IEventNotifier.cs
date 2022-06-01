using UnityEngine.Events;

public interface IEventNotifier<T>
{
    UnityEvent<T> EventHappened { get; }
}