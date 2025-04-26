namespace Failsafe.Scripts.Character.Components
{
    public class CharacterMover
    {
        private readonly CharacterEventBus _eventBus;

        public CharacterMover(CharacterEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}