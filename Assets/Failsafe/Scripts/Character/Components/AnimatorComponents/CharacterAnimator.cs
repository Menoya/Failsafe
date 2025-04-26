using System;
using Failsafe.Scripts.Services.Input;
using UnityEngine;

namespace Failsafe.Scripts.Character.Components.AnimatorComponents
{
    public class CharacterAnimator : IDisposable
    { 
        private readonly Animator _animator;
        private readonly CharacterAnimatorHash _animatorHash;
        private readonly CharacterAnimatorParameters _parameters;

        public CharacterAnimator(CharacterEventBus eventBus, Animator animator, IInputService inputService)
        {
            _animator = animator;
            _animatorHash = new CharacterAnimatorHash();
            _parameters = new CharacterAnimatorParameters(eventBus, inputService);
        }

        public void Dispose() => 
            _parameters.Dispose();

        public void Update()
        {
            _parameters.SmoothUpdateValues();
            
            SetAnimatorFloat(_animatorHash.ForwardInputHash, _parameters.CurrentForwardInput * _parameters.CurrentSpeedMultiplier);
            SetAnimatorFloat(_animatorHash.SideInputHash, _parameters.CurrentSideInput * _parameters.CurrentSpeedMultiplier);
            SetAnimatorFloat(_animatorHash.RotateMagnitude, _parameters.CurrentRotateValue);
            SetAnimatorFloat(_animatorHash.MoveHeightHash, _parameters.CurrentMoveHeight);
        }

        private void SetAnimatorFloat(int hash, float value) => 
            _animator.SetFloat(hash, value);
    }
}