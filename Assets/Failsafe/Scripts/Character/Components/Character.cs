using UnityEngine;

namespace Failsafe.Scripts.Character.Components
{
    public class Character : MonoBehaviour
    {
        public CharacterStateMachine StateMachine { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public CharacterMover Mover { get; private set; }

        public void Init(Animator animator)
        {
            StateMachine = new CharacterStateMachine();
            Animator = new CharacterAnimator(animator);
            Mover = new CharacterMover();
        }
    }
}