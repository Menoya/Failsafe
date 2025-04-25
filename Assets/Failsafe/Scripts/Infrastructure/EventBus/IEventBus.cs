using System;
using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Infrastructure.EventBus
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IEvent;
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IEvent;
        void Publish<TEvent>(TEvent evt) where TEvent : struct, IEvent;
    }
}