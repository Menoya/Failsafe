using System;
using Failsafe.Scripts.Character.Events;
using Failsafe.Scripts.Services.Input;
using Failsafe.Scripts.Services.Input.Character.Events;
using UnityEngine;

namespace Failsafe.Scripts.Character.Components.AnimatorComponents
{ 
    public class CharacterAnimatorParameters : IDisposable
    {
        private enum CharacterAnimationHeightType { Stand = 2, Crouch = 1, Crawl = 0}
        
        private const float LERP_SPEED = 5f;
        private const float TOLERANCE = 0.04f;

        private readonly CharacterEventBus _eventBus;
        private readonly IInputService _inputService;
        
        public float CurrentMoveHeight;
        public float CurrentForwardInput;
        public float CurrentSideInput;
        public float CurrentRotateValue;
        public float CurrentSpeedMultiplier;

        private CharacterAnimationHeightType _animationHeightType = CharacterAnimationHeightType.Stand;
        private float _targetMoveHeight = 2f;
        private float _targetForwardInput;
        private float _targetSideInput;
        private float _targetRotateValue;
        private float _targetSpeedMultiplier = 1;

        public CharacterAnimatorParameters(CharacterEventBus eventBus, IInputService inputService)
        {
            _eventBus = eventBus;
            _inputService = inputService;
            
            AddSubscriptions();
        }

        public void Dispose() => 
            RemoveSubscriptions();

        private void AddSubscriptions()
        {
            _eventBus.Subscribe<OnCharacterMove>(OnMove);
            _eventBus.Subscribe<OnCharacterRotate>(OnRotate);
            _inputService.Character.EventBus.Subscribe<OnCharacterCrouchButton>(OnCrouchButton);
            _inputService.Character.EventBus.Subscribe<OnCharacterCrawlButton>(OnCrawlButton);
            _inputService.Character.EventBus.Subscribe<OnCharacterWalkButton>(OnWalkButton);
            _inputService.Character.EventBus.Subscribe<OnCharacterRunButton>(OnRunButton);
        }

        private void RemoveSubscriptions()
        {
            _eventBus.Unsubscribe<OnCharacterMove>(OnMove);
            _eventBus.Unsubscribe<OnCharacterRotate>(OnRotate);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterCrouchButton>(OnCrouchButton);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterCrawlButton>(OnCrawlButton);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterWalkButton>(OnWalkButton);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterRunButton>(OnRunButton);
        }

        public void SmoothUpdateValues()
        {
            CurrentMoveHeight = SmoothChange(CurrentMoveHeight, _targetMoveHeight);
            CurrentForwardInput = SmoothChange(CurrentForwardInput, _targetForwardInput);
            CurrentSideInput = SmoothChange(CurrentSideInput, _targetSideInput);
            CurrentRotateValue = SmoothChange(CurrentRotateValue, _targetRotateValue);
            CurrentSpeedMultiplier = SmoothChange(CurrentSpeedMultiplier, _targetSpeedMultiplier);
        }

        private void OnMove(OnCharacterMove movementInput)
        {
            _targetForwardInput = movementInput.ForwardMagnitude;
            _targetSideInput = movementInput.SideMagnitude;
        }

        private void OnRotate(OnCharacterRotate rotateMagnitude) => 
            _targetRotateValue = rotateMagnitude.Horizontal;

        private void OnRunButton(OnCharacterRunButton state) => 
            _targetSpeedMultiplier = state.IsPressed ? 2f : 1f;

        private void OnWalkButton(OnCharacterWalkButton state) => 
            _targetSpeedMultiplier = state.IsPressed ? 0.5f : 1f;

        private void OnCrouchButton(OnCharacterCrouchButton _) => 
            ToggleAnimationHeightTo(CharacterAnimationHeightType.Crouch);

        private void OnCrawlButton(OnCharacterCrawlButton _) => 
            ToggleAnimationHeightTo(CharacterAnimationHeightType.Crawl);

        private void ChangeHeightState(CharacterAnimationHeightType type)
        {
            _targetMoveHeight = (int)type;
            _animationHeightType = type;
        }

        private void ToggleAnimationHeightTo(CharacterAnimationHeightType targetHeightType)
        {
            switch (_animationHeightType)
            {
                case CharacterAnimationHeightType.Stand 
                  or CharacterAnimationHeightType.Crawl when targetHeightType is CharacterAnimationHeightType.Crouch:
                    ChangeHeightState(CharacterAnimationHeightType.Crouch);
                    break;
                case CharacterAnimationHeightType.Crouch 
                  or CharacterAnimationHeightType.Stand when targetHeightType is CharacterAnimationHeightType.Crawl:
                    ChangeHeightState(CharacterAnimationHeightType.Crawl);
                    break;
                case CharacterAnimationHeightType.Crawl when targetHeightType is CharacterAnimationHeightType.Crawl:
                case CharacterAnimationHeightType.Crouch when targetHeightType is CharacterAnimationHeightType.Crouch:
                    ChangeHeightState(CharacterAnimationHeightType.Stand);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float SmoothChange(float current, float target) => 
            Math.Abs(current - target) > TOLERANCE ? Mathf.Lerp(current, target, LERP_SPEED * Time.deltaTime) : target;
    }
}