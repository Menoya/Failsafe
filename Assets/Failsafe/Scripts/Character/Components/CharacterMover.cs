using System;
using Failsafe.Scripts.Character.Events;
using Failsafe.Scripts.Services.Input;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterMover : IDisposable
    {
        private readonly CharacterEventBus _eventBus;
        private readonly IInputService _inputService;

        public CharacterMover(CharacterEventBus eventBus, IInputService inputService)
        {
            _eventBus = eventBus;
            _inputService = inputService;

            AddSubscriptions();
        }

        public void Dispose() => 
            RemoveSubscriptions();
        
        private void AddSubscriptions() => 
            _inputService.Character.EventBus.Subscribe<OnCharacterMove>(Move);

        private void RemoveSubscriptions() => 
            _inputService.Character.EventBus.Subscribe<OnCharacterMove>(Move);

        private void Move(OnCharacterMove magnitude)
        {
           //move character

           _eventBus.Publish(new OnCharacterMove { ForwardMagnitude = magnitude.ForwardMagnitude, SideMagnitude = magnitude.SideMagnitude });
        }
    }
}