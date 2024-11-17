using System;
using UnityEngine.Events;
using Utility.EventBusSystem.Interfaces;

namespace Utility
{
    public class UnityEventSubscription : ISubscription
    {
        public readonly UnityEvent @event;
        public readonly UnityAction action;

        public UnityEventSubscription(UnityEvent @event, Action action)
        {
            this.@event = @event;
            this.action = new UnityAction(action);
            this.@event.AddListener(this.action);
        }

        public void Unsubscribe()
        {
            @event.RemoveListener(action);
        }
    }

    public class UnityEventSubscription<T> : ISubscription
    {
        public readonly UnityEvent<T> @event;
        public readonly UnityAction<T> action;

        public UnityEventSubscription(UnityEvent<T> @event, Action<T> action)
        {
            this.@event = @event;
            this.action = new UnityAction<T>(action);
            this.@event.AddListener(this.action);
        }

        public void Unsubscribe()
        {
            @event.RemoveListener(action);
        }
    }
}