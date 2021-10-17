using System;

public interface IReadOnlySubscriptionProperty<T>
{
    void Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}
