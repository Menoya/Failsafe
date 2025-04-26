using System;
using Failsafe.Scripts.Character.Events;
using Failsafe.Scripts.Services.Input;
using Failsafe.Scripts.Services.Input.Character.Events;
using UnityEngine;

namespace Failsafe.Scripts.Character.Components.AnimatorComponents
{
    public class CharacterAnimator : IDisposable
    {
        private enum CharacterAnimationHeightType { Stand, Crouch, Crawl}
        
        private readonly CharacterEventBus _eventBus;
        private readonly Animator _animator;
        private readonly IInputService _inputService;
        private readonly CharacterAnimatorParameters _animatorParameters;

        private CharacterAnimationHeightType _currentAnimationHeightType;
        private Vector2 _currentMagnitude;
        private float _speedMultiplier = 1;
        
        public CharacterAnimator(CharacterEventBus eventBus, Animator animator, IInputService inputService)
        {
            _eventBus = eventBus;
            _animator = animator;
            _inputService = inputService;
            _animatorParameters = new CharacterAnimatorParameters();

            Init();
            
            AddSubscriptions();
        }

        private void Init()
        {
            ChangeHeightState(CharacterAnimationHeightType.Stand);
        }

        public void Dispose() => 
            RemoveSubscriptions();

        private void AddSubscriptions()
        {
            _eventBus.Subscribe<OnCharacterMove>(OnMove);
            _inputService.Character.EventBus.Subscribe<OnCharacterCrouchButton>(OnCrouchButton);
            _inputService.Character.EventBus.Subscribe<OnCharacterCrawlButton>(OnCrawlButton);
            _inputService.Character.EventBus.Subscribe<OnCharacterWalkButton>(OnWalkButton);
            _inputService.Character.EventBus.Subscribe<OnCharacterRunButton>(OnRunButton);
        }

        private void RemoveSubscriptions()
        {
            _eventBus.Unsubscribe<OnCharacterMove>(OnMove);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterCrouchButton>(OnCrouchButton);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterCrawlButton>(OnCrawlButton);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterWalkButton>(OnWalkButton);
            _inputService.Character.EventBus.Unsubscribe<OnCharacterRunButton>(OnRunButton);
        }

        private void OnMove(OnCharacterMove movementInput)
        {
            _currentMagnitude = new Vector2 { x = movementInput.ForwardMagnitude, y = movementInput.SideMagnitude };
            
            SetAnimatorFloat(_animatorParameters.ForwardInputHash, _currentMagnitude.x * _speedMultiplier);
            SetAnimatorFloat(_animatorParameters.SideInputHash, _currentMagnitude.y * _speedMultiplier);
        }

        private void OnRunButton(OnCharacterRunButton state)
        {
            _speedMultiplier = state.IsPressed ? 2f : 1f;
            
            OnMove(new OnCharacterMove { ForwardMagnitude = _currentMagnitude.x, SideMagnitude = _currentMagnitude.y });
        }

        private void OnWalkButton(OnCharacterWalkButton state)
        {
            _speedMultiplier = state.IsPressed ? 0.5f : 1f;
            
            OnMove(new OnCharacterMove { ForwardMagnitude = _currentMagnitude.x, SideMagnitude = _currentMagnitude.y });
        }

        private void OnCrouchButton(OnCharacterCrouchButton _) => 
            ToggleAnimationHeightTo(CharacterAnimationHeightType.Crouch);

        private void OnCrawlButton(OnCharacterCrawlButton _) => 
            ToggleAnimationHeightTo(CharacterAnimationHeightType.Crawl);

        private void ToggleAnimationHeightTo(CharacterAnimationHeightType targetHeightType)
        {
            switch (_currentAnimationHeightType)
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

        private void ChangeHeightState(CharacterAnimationHeightType type)
        {
            switch (type)
            {
                case CharacterAnimationHeightType.Stand:
                    SetAnimationHeight(CharacterAnimationHeightType.Stand, _animatorParameters.StandHeightValue);
                    break;
                case CharacterAnimationHeightType.Crouch:
                    SetAnimationHeight(CharacterAnimationHeightType.Crouch, _animatorParameters.CrouchHeightValue);
                    break;
                case CharacterAnimationHeightType.Crawl:
                    SetAnimationHeight(CharacterAnimationHeightType.Crawl, _animatorParameters.CrawlHeightValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }           
        }

        private void SetAnimationHeight(CharacterAnimationHeightType type, float moveHeightValue)
        {
            SetAnimatorFloat(_animatorParameters.MoveHeightHash, moveHeightValue);
            _currentAnimationHeightType = type;
        }

        private void SetAnimatorFloat(int hash, float value) => 
            _animator.SetFloat(hash, value);
    }
}