using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Services.Input.Character.Events
{
    public struct OnCharacterRunButton : IInputEvent
    {
        public bool IsPressed;
    }
}