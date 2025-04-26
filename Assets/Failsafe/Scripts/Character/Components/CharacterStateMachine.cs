using System;
using System.Collections.Generic;
using Failsafe.Scripts.Character.States;
using Failsafe.Scripts.Infrastructure.StateMachine;
using Failsafe.Scripts.Infrastructure.StateMachine.States;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterStateMachine : StateMachine
    {
        private readonly CharacterEventBus _eventBus;

        public CharacterStateMachine(CharacterEventBus eventBus)
        {
            _eventBus = eventBus;
            
            InitStates();
        }

        private void InitStates()
        {
            States = new Dictionary<Type, IExitableState>
            {
                [typeof(CharacterIdleState)] = new CharacterIdleState(),
                [typeof(CharacterMoveState)] = new CharacterMoveState(),
            };
        }
    }
}