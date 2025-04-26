using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Character.Events
{
    public struct OnCharacterRotate : ICharacterEvent
    {
        public float Horizontal;
    }
}