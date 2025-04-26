using System;
using Failsafe.Scripts.Infrastructure.EventBus;
using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Services.Input.Character
{
    public class CharacterInputEventBus : EventBus
    {
        public new void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IInputEvent => 
            base.Subscribe(handler);
        
        public new void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IInputEvent => 
            base.Unsubscribe(handler);
        
        public new void Publish<TEvent>(TEvent evt) where TEvent : struct, IInputEvent => 
            base.Publish(evt);
    }
}