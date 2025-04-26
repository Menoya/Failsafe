using Failsafe.Scripts.Services.Input.Character;

namespace Failsafe.Scripts.Services.Input
{
    public interface IInputService : IService
    {
        CharacterInput Character { get; }
    }
}