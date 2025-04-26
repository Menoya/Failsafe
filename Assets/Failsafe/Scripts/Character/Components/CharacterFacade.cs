using Failsafe.Scripts.Character.Components.AnimatorComponents;
using Failsafe.Scripts.Services.Input;
using UnityEngine;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterFacade : MonoBehaviour
    {
        public CharacterStateMachine StateMachine { get; private set; }
        public CharacterEventBus EventBus { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public CharacterMover Mover { get; private set; }

        public void Init(Animator animator, IInputService inputService)
        {
            EventBus = new CharacterEventBus();
            StateMachine = new CharacterStateMachine(EventBus);

            Mover = new CharacterMover(EventBus, inputService, transform);
            Animator = new CharacterAnimator(EventBus, animator, inputService);
        }

        private void OnDestroy()
        {
            Animator.Dispose();
            Mover.Dispose();
        }
    }
}