using Failsafe.Scripts.Infrastructure.StateMachine.States;

namespace Failsafe.Scripts.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        public TState ChangeState<TState>() where TState : class, IExitableState;
        public TState GetState<TState>() where TState : class, IExitableState;
    }
}