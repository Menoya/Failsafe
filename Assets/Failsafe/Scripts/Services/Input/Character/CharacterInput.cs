using Failsafe.Scripts.Services.Input.Character.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Failsafe.Scripts.Services.Input.Character
{
    public class CharacterInput
    {
        private readonly CharacterControl _characterControl;
        
        public readonly CharacterInputEventBus EventBus;

        public CharacterInput()
        {
            EventBus = new CharacterInputEventBus();
            _characterControl = new CharacterControl();
            
            AddSubscriptions();
        }

        public void Destroy()
        {
            RemoveSubscriptions();
        }
        
        private void AddSubscriptions()
        {
            _characterControl.Move.MoveDirection.performed += OnMovePerformed;
            _characterControl.Move.MoveDirection.canceled += OnMoveCanceled;

            _characterControl.Move.Walk.started += OnWalkButtonDown;
            _characterControl.Move.Walk.canceled += OnWalkButtonUp;
            
            _characterControl.Move.Run.started += OnRunButtonDown;
            _characterControl.Move.Run.canceled += OnRunButtonUp;
            
            _characterControl.Move.Crouch.started += OnCrouchButtonDown;
            _characterControl.Move.Crawl.started += OnCrawlButtonDown;
            _characterControl.Move.Jump.started += OnJumButtonDown;
            
            _characterControl.Enable();
        }

        private void RemoveSubscriptions()
        {
            _characterControl.Move.MoveDirection.performed -= OnMovePerformed;
            _characterControl.Move.MoveDirection.canceled -= OnMoveCanceled;
            
            _characterControl.Move.Walk.started -= OnWalkButtonDown;
            _characterControl.Move.Walk.canceled -= OnWalkButtonUp;
            
            _characterControl.Move.Run.started -= OnRunButtonDown;
            _characterControl.Move.Run.canceled -= OnRunButtonUp;
            
            _characterControl.Move.Crouch.started -= OnCrouchButtonDown;
            _characterControl.Move.Crawl.started -= OnCrawlButtonDown;
            _characterControl.Move.Jump.started -= OnJumButtonDown;
            
            _characterControl.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            var moveEvent = new OnMoveDirectionChange { Horizontal = direction.x, Vertical = direction.y };

            EventBus.Publish(moveEvent);
        }

        private void OnMoveCanceled(InputAction.CallbackContext _)
        {
            var moveEvent = new OnMoveDirectionChange { Horizontal = 0, Vertical = 0 };
            
            EventBus.Publish(moveEvent);
        }

        private void OnWalkButtonDown(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterWalkButton { IsPressed = true });

        private void OnWalkButtonUp(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterWalkButton { IsPressed = false });

        private void OnRunButtonDown(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterRunButton { IsPressed = true });

        private void OnRunButtonUp(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterRunButton { IsPressed = false });

        private void OnCrouchButtonDown(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterCrouchButton { IsPressed = true });

        private void OnCrawlButtonDown(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterCrawlButton { IsPressed = true });

        private void OnJumButtonDown(InputAction.CallbackContext _) => 
            EventBus.Publish(new OnCharacterJumpButton { IsPressed = true });
    }
}