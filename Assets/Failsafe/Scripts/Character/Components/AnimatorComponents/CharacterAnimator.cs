using UnityEngine;

namespace Failsafe.Scripts.Character.Components.AnimatorComponents
{
    public class CharacterAnimator
    {
        private readonly CharacterEventBus _eventBus;
        private readonly Animator _animator;
        private readonly CharacterAnimatorParameters _animatorParameters;

        public CharacterAnimator(CharacterEventBus eventBus, Animator animator)
        {
            _eventBus = eventBus;
            _animator = animator;
            _animatorParameters = new CharacterAnimatorParameters();
        }
    }
}