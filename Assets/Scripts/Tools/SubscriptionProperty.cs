using System;

namespace Tools
{
    public class SubscriptionProperty<T> : IReadOnlySubscriptionProperty<T>
    {
        private T _value;
        private Action<T> OnChange;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnChange?.Invoke(_value);
            }
        }

        public void Subscribe(Action<T> action)
        {
            OnChange += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            OnChange -= action;

        }
    }
}