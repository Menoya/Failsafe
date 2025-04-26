using System;
using System.Collections.Generic;
using Failsafe.Scripts.Character.States;
using Failsafe.Scripts.Infrastructure.StateMachine;
using Failsafe.Scripts.Infrastructure.StateMachine.States;
using Failsafe.Scripts.Infrastructure.StateMachine.States.Character;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterStateMachine : StateMachine
    {
        private readonly CharacterEventBus _eventBus;

        protected new Dictionary<Type, ICharacterState> States;
        
        public CharacterStateMachine(CharacterEventBus eventBus)
        {
            _eventBus = eventBus;
            
            InitStates();
        }

        private void InitStates()
        {
            States = new Dictionary<Type, ICharacterState>
            {
                [typeof(CharacterIdleState)] = new CharacterIdleState(),
                [typeof(CharacterMoveState)] = new CharacterMoveState(),
            };
        }

        public new TState ChangeStateTo<TState>() where TState : class, ICharacterState => 
            base.ChangeStateTo<TState>();

        public new TState ChangeStateTo<TState, TPayload>(TPayload payload) where TState : class, ICharacterPayloadedState<TPayload> => 
            base.ChangeStateTo<TState, TPayload>(payload);
    }
}