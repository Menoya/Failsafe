using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Services.Input.Character.Events
{
    public struct OnMoveDirectionChange : IInputEvent
    {
        public float Horizontal;
        public float Vertical;
    }
}