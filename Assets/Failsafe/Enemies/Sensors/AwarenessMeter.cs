using System.Linq;
using UnityEngine;

namespace Failsafe.Enemies.Sensors
{
    public class AwarenessMeter
    {
        [Header("Sensors")] private Sensor[] _sensors;

        [Header("Настройки")]
        private float _fillSpeed = 80f;
        private float _decaySpeed = 10f;
        private float _decayDelay = 2f;

        [Header("Пороги")] [SerializeField]
        private float _alertThreshold = 30f;
        private float _chaseThreshold = 100f;
        private float _chaseExitThreshold = 30f;
        
        [SerializeField, Range(0, 100)] private float _alertness;

        private float _decayDelayTimer;

        private bool _hasEverChased = false;
        private bool _hasLostPlayer = false;

        public float AlertnessValue => _alertness;

        public AwarenessMeter(Sensor[] sensors)
        {
            _sensors = sensors;
        }

        public bool IsChasing()
        {
                if (_alertness >= _chaseThreshold)
                {
                    _hasEverChased = true;
                    return true;
                }

                return false;
        }

        public bool IsPlayerLost()
        {
            if (_hasEverChased && !_hasLostPlayer && Mathf.Approximately(_alertness, _chaseExitThreshold))
            {
                _hasLostPlayer = true;
                return true;
            }

            return false;
        }

        public bool IsAlerted()
        {
            return _alertness >= _alertThreshold;
        }

        public bool IsCalm()
        {
            // Если враг однажды достиг состояния погони — он не может вернуться в "спокойствие"
            if (_hasEverChased) return false;
            return _alertness < _alertThreshold;
        }

        public void Update()
        {
            if (_alertness >= _chaseThreshold)
            {
                _hasEverChased = true;
                _hasLostPlayer = false; // сбрасываем флаг "игрок потерян"
            }
            float maxSignal = _sensors.Max(s => s.SignalStrength);

            if (maxSignal > 0f)
            {
                _alertness += maxSignal * _fillSpeed * Time.deltaTime;
                _decayDelayTimer = _decayDelay;
            }
            else
            {
                if (_decayDelayTimer > 0f)
                    _decayDelayTimer -= Time.deltaTime;
                else
                    _alertness -= _decaySpeed * Time.deltaTime;
            }

            _alertness = Mathf.Clamp(_alertness, 0f, 100f);

            // Если враг однажды достиг состояния погони — ограничиваем минимальное значение
            if (_hasEverChased)
                _alertness = Mathf.Max(_alertness, _alertThreshold);
        }
    }
    
}
