using Failsafe.Scripts.Services.Input.Character;

namespace Failsafe.Scripts.Services.Input
{
    public class InputService : IInputService
    {
        public CharacterInput Character { get; private set; }

        public InputService()
        {
            Character = new CharacterInput();
        }
    }
}