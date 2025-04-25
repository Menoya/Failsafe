using Failsafe.Scripts.Infrastructure.StateMachine.States;

namespace Failsafe.Scripts.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        public TState ChangeStateTo<TState>() where TState : class, IState;
    }
}