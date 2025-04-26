using System;
using Failsafe.Scripts.Infrastructure.EventBus;
using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterEventBus : EventBus
    {
        public new void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, ICharacterEvent => 
            base.Subscribe(handler);
        
        public new void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, ICharacterEvent => 
            base.Unsubscribe(handler);
        
        public new void Publish<TEvent>(TEvent evt) where TEvent : struct, ICharacterEvent => 
            base.Publish(evt);
    }
}