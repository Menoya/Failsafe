using UnityEngine;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterAnimator
    {
        private readonly Animator _animator;

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
        }
    }
}