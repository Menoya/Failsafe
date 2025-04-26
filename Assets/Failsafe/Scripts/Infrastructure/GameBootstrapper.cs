using Failsafe.Scripts.Character.Components;
using Failsafe.Scripts.Services.Input;
using UnityEngine;

namespace Failsafe.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private CharacterFacade _characterPrefab;
        
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = new InputService();

            InitCharacter();
        }

        private void InitCharacter()
        {
            var character = Instantiate(_characterPrefab);
            var animator = character.GetComponentInChildren<Animator>();
            
            character.Init(animator, _inputService);
        }
    }
}