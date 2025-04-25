using System;
using System.Collections.Generic;
using Failsafe.Scripts.Infrastructure.StateMachine.States;

namespace Failsafe.Scripts.Infrastructure.StateMachine
{
    public class StateMachine : IStateMachine
    {
        protected Dictionary<Type, IExitableState> States;
        
        private IExitableState _activeState;

        public TState ChangeStateTo<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            TState state = GetStateByType<TState>();
            state.Enter();
            _activeState = state;
            return state;
        }
        
        public TState ChangeStateTo<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            TState state = GetStateByType<TState>();
            state.Enter(payload);
            _activeState = state;
            return state;
        }

        private TState GetStateByType<TState>() where TState : class, IExitableState => 
            States[typeof(TState)] as TState;
    }
}