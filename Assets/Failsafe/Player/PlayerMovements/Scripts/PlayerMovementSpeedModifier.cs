using Failsafe.Scripts.Modifiebles;
using System;
using UnityEngine;

namespace Failsafe.PlayerMovements
{
    /// <summary>
    /// Изменение параметров движения в зависимости от действий игрока
    /// </summary>
    public class PlayerMovementSpeedModifier : IDisposable
    {
        private PlayerMovementParameters _playerMovementParameters;
        private IModificator<float> _slowModificator;
        private float _slowApplyedAt;
        private bool _slowApplyed;

        public PlayerMovementSpeedModifier(PlayerMovementParameters playerMovementParameters)
        {
            _playerMovementParameters = playerMovementParameters;
            _slowModificator = new MultiplierFloat(_playerMovementParameters.SlowMultiplierOnLanding, priority: 100);
        }
        
        public void Update()
        {
            if (_slowApplyed && _slowApplyedAt + _playerMovementParameters.SlowDurationOnLanding < Time.time)
            {
                RemoveSlowOnLanding();
            }
        }

        /// <summary>
        /// Замедлить при приземлении
        /// </summary>
        public void ApplySlowOnLanding()
        {
            _playerMovementParameters.WalkSpeed.AddModificator(_slowModificator);
            _playerMovementParameters.RunSpeed.AddModificator(_slowModificator);
            _playerMovementParameters.CrouchSpeed.AddModificator(_slowModificator);
            _slowApplyed = true;
            _slowApplyedAt = Time.time;
        }

        /// <summary>
        /// Убрать замедление
        /// </summary>
        private void RemoveSlowOnLanding()
        {
            _playerMovementParameters.WalkSpeed.RemoveModificator(_slowModificator);
            _playerMovementParameters.RunSpeed.RemoveModificator(_slowModificator);
            _playerMovementParameters.CrouchSpeed.RemoveModificator(_slowModificator);
            _slowApplyed = false;
        }

        public void Dispose()
        {
            RemoveSlowOnLanding();
        }
    }
}
