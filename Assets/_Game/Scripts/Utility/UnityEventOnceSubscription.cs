using UnityEngine.Events;
using Utility.EventBusSystem.Interfaces;

namespace Utility
{
    public class UnityEventOnceSubscription : ISubscription
    {
        private readonly UnityEvent @event;
        private readonly UnityAction action;

        public UnityEventOnceSubscription(UnityEvent @event, UnityAction action)
        {
            this.@event = @event;
            this.action = action;
            this.@event.AddListener(Handler);
        }

        private void Handler()
        {
            try
            {
                action();
            }
            finally
            {
                Unsubscribe();
            }
        }

        public void Unsubscribe()
        {
            @event.RemoveListener(action);
        }
    }
}