using Failsafe.Scripts.Infrastructure.EventBus.Events;

namespace Failsafe.Scripts.Character.Events
{
    public struct OnCharacterMove : ICharacterEvent
    {
        public float ForwardMagnitude;
        public float SideMagnitude;
    }
}