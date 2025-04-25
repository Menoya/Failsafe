namespace Failsafe.Scripts.Infrastructure.StateMachine.States
{
    public interface IPayloadedState<TPayload>: IExitableState
    {
        public void Enter(TPayload payload);
    }
}