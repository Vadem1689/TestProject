using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Events;
using Utility.EventBusSystem.Interfaces;

namespace Utility
{
    public class AsyncUnityEventSubscription : ISubscription
    {
        public readonly UnityEvent @event;
        public readonly Func<Task> action;

        private const int Free = 0;
        private const int Busy = 1;
        private int isBusy;

        public AsyncUnityEventSubscription(UnityEvent @event, Func<Task> action)
        {
            this.@event = @event;
            this.action = action;
            this.@event.AddListener(OnEvent);
        }

        private async void OnEvent()
        {
            if (Interlocked.CompareExchange(ref isBusy, Busy, Free) == Free)
            {
                try
                {
                    await action();
                }
                finally
                {
                    Interlocked.Exchange(ref isBusy, Free);
                }
            }
        }

        public void Unsubscribe()
        {
            @event.RemoveListener(OnEvent);
        }
    }
    
    public class AsyncUnityEventSubscription<T> : ISubscription
    {
        public readonly UnityEvent<T> @event;
        public readonly Func<T, Task> action;

        private const int Free = 0;
        private const int Busy = 1;
        private int isBusy;

        public AsyncUnityEventSubscription(UnityEvent<T> @event, Func<T, Task> action)
        {
            this.@event = @event;
            this.action = action;
            this.@event.AddListener(OnEvent);
        }

        private async void OnEvent(T value)
        {
            if (Interlocked.CompareExchange(ref isBusy, Busy, Free) == Free)
            {
                try
                {
                    await action(value);
                }
                finally
                {
                    Interlocked.Exchange(ref isBusy, Free);
                }
            }
        }

        public void Unsubscribe()
        {
            @event.RemoveListener(OnEvent);
        }
    }
}