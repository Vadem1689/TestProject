using System;
using System.Threading.Tasks;
using UnityEngine.Events;
using Utility.EventBusSystem.Interfaces;

namespace Utility
{
    public static class UnityEventExtension
    {
        public static ISubscription Subscribe(this UnityEvent @event, Action action)
        {
            return new UnityEventSubscription(@event, action);
        }
        
        public static ISubscription SubscribeOnce(this UnityEvent @event, UnityAction action)
        {
            return new UnityEventOnceSubscription(@event, action);
        }
        
        public static ISubscription Subscribe<T>(this UnityEvent<T> @event, Action<T> action)
        {
            return new UnityEventSubscription<T>(@event, action);
        }
        
        public static ISubscription Subscribe<T>(this UnityEvent<T> @event, Action action)
        {
            return new UnityEventSubscription<T>(@event, arg => action());
        }
        
        public static ISubscription Subscribe(this UnityEvent @event, Func<Task> action)
        {
            return new AsyncUnityEventSubscription(@event, action);
        }
        
        public static ISubscription Subscribe<T>(this UnityEvent<T> @event, Func<T, Task> action)
        {
            return new AsyncUnityEventSubscription<T>(@event, action);
        }
    }
}