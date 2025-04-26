using System;
using Failsafe.Scripts.Character.Events;
using Failsafe.Scripts.Services.Input;
using Failsafe.Scripts.Services.Input.Character.Events;
using UnityEngine;

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
            _inputService.Character.EventBus.Subscribe<OnMoveDirectionChange>(Move);

        private void RemoveSubscriptions() => 
            _inputService.Character.EventBus.Subscribe<OnMoveDirectionChange>(Move);

        private void Move(OnMoveDirectionChange magnitude)
        {
           //move character
           _eventBus.Publish(new OnCharacterMove { ForwardMagnitude = magnitude.Vertical, SideMagnitude = magnitude.Horizontal });
        }
    }
}