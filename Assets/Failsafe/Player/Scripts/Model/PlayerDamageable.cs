using Failsafe.Scripts.Damage;
using Failsafe.Scripts.Damage.Implementation;
using Failsafe.Scripts.Damage.Providers;
using Failsafe.Scripts.Health;
using Failsafe.Player.View;
using VContainer.Unity;

namespace Failsafe.Player.Model
{
    /// <summary>
    /// Получение урона
    /// </summary>
    public class PlayerDamageable : IInitializable
    {
        private readonly IHealth _health;
        private readonly PlayerView _playerView;
        private IDamageService _damageService;
        private DamageableComponent _damageableComponent;

        public PlayerDamageable(IHealth health, PlayerView playerView)
        {
            _health = health;
            _playerView = playerView;
        }

        public void Initialize()
        {
            _damageService = new DamageService(new FlatDamageProvider(_health));
            _damageableComponent = _playerView.Damageable;
            _damageableComponent.OnTakeDamage += OnTakeDamage;
        }

        private void OnTakeDamage(IDamage damage)
        {
            _damageService.Provide(damage);
        }
    }
}
