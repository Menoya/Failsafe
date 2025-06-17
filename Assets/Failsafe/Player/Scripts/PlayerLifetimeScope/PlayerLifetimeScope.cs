using Failsafe.Player.Model;
using Failsafe.Player.View;
using Failsafe.PlayerMovements;
using Failsafe.Scripts.Health;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Failsafe.Player
{
    /// <summary>
    /// Регистрация компонентов игрового персонажа
    /// <para/>Дочерний скоуп к <see cref="Failsafe.GameSceneServices.GameSceneLifetimeScope"/>
    /// </summary>
    public class PlayerLifetimeScope : LifetimeScope
    {
        [SerializeReference] private PlayerModelParameters _playerModelParameters;
        [SerializeReference] private PlayerMovementParameters _playerMovementParameters;
        [SerializeReference] private PlayerNoiseParameters _playerNoiseParameters;

        [SerializeField] private PlayerView _playerView;
        [SerializeField] private InputActionAsset _inputActionAsset;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerModelParameters);
            builder.RegisterInstance(_playerMovementParameters);
            builder.RegisterInstance(_playerNoiseParameters);
            builder.RegisterComponent(_playerView);
            builder.RegisterComponent(_inputActionAsset);

            builder.Register<InputHandler>(Lifetime.Scoped);

            builder.Register<SimpleHealth>(Lifetime.Singleton).As<IHealth>().WithParameter(_playerModelParameters.MaxHealth);
            builder.Register<PlayerStamina>(Lifetime.Singleton).As<IStamina>().WithParameter(_playerModelParameters.MaxStamina);
            builder.RegisterEntryPoint<PlayerDamageable>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PlayerStaminaController>(Lifetime.Scoped).AsSelf();

            builder.RegisterEntryPoint<PlayerController>(Lifetime.Scoped);
        }
    }
}
