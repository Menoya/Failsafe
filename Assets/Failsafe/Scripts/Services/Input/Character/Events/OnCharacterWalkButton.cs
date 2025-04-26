using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Services.Input.Character.Events
{
    public struct OnCharacterWalkButton : IInputEvent
    {
        public bool IsPressed;
    }
}