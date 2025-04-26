using Failsafe.Scripts.Character.Components.AnimatorComponents;
using UnityEngine;

namespace Failsafe.Scripts.Character.Components
{
    public class Character : MonoBehaviour
    {
        public CharacterStateMachine StateMachine { get; private set; }
        public CharacterEventBus EventBus { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public CharacterMover Mover { get; private set; }

        public void Init(Animator animator)
        {
            EventBus = new CharacterEventBus();
            StateMachine = new CharacterStateMachine(EventBus);
            
            Animator = new CharacterAnimator(EventBus, animator);
            Mover = new CharacterMover(EventBus);
        }
    }
}