using System;
using System.Collections.Generic;
using System.Linq;
using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Infrastructure.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IEvent
        {
            var type = typeof(TEvent);

            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Delegate>();

            _subscribers[type].Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IEvent
        {
            var type = typeof(TEvent);
            
            if (!_subscribers.TryGetValue(type, out var handlers)) return;
            
            handlers.Remove(handler);
            if (handlers.Count == 0)
                _subscribers.Remove(type);
        }

        public void Publish<TEvent>(TEvent evt) where TEvent : struct, IEvent
        {
            var type = typeof(TEvent);
            
            if (!_subscribers.TryGetValue(type, out var handlers)) return;
            
            foreach (var handler in handlers.Cast<Action<TEvent>>()) 
                handler.Invoke(evt);
        }
    }
}