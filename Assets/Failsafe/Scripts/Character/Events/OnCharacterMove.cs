using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Character.Events
{
    public struct OnCharacterMove : IEvent
    {
        public float ForwardMagnitude;
        public float SideMagnitude;
    }
}