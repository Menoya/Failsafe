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
        private readonly Transform _transform;

        public CharacterMover(CharacterEventBus eventBus, IInputService inputService, Transform transform)
        {
            _eventBus = eventBus;
            _inputService = inputService;
            _transform = transform;

            AddSubscriptions();
        }

        public void Dispose() => 
            RemoveSubscriptions();
        
        private void AddSubscriptions()
        {
            _inputService.Character.EventBus.Subscribe<OnMoveDirectionChange>(Move);
            _inputService.Character.EventBus.Subscribe<OnLookDirectionChange>(Rotate);
        }

        private void RemoveSubscriptions()
        {
            _inputService.Character.EventBus.Unsubscribe<OnMoveDirectionChange>(Move);
            _inputService.Character.EventBus.Unsubscribe<OnLookDirectionChange>(Rotate);
        }

        private void Move(OnMoveDirectionChange magnitude)
        {
           _eventBus.Publish(new OnCharacterMove { ForwardMagnitude = magnitude.Vertical, SideMagnitude = magnitude.Horizontal });
        }

        private void Rotate(OnLookDirectionChange lookDirection) => 
            _transform.rotation *= Quaternion.Euler(new Vector2(0, lookDirection.Horizontal));
    }
}