using Failsafe.Scripts.Damage.Implementation;
using FMODUnity;
using UnityEngine;

namespace Failsafe.Player.View
{
    /// <summary>
    /// Представление персонажа
    /// </summary>
    /// <remarks>
    /// Должен содержать компоненты специфичные для движка Unity: Рендер, Анимации, Звук.
    /// Логика должна быть вынесена в отдельные Модели и Контроллеры
    /// </remarks>
    public class PlayerView : MonoBehaviour
    {
        /// <summary>
        /// Камера (голова) персонажа
        /// </summary>
        public Transform PlayerCamera;
        /// <summary>
        /// Тело персонажа
        /// </summary>
        public Transform Body;

        public Animator Animator;

        public DamageableComponent Damageable;
        /// <summary>
        /// Точка захвата
        /// </summary>
        public Transform PlayerGrabPoint;

        public CharacterController CharacterController;

        public EventReference FootstepEvent;


        void OnValidate()
        {
            if (PlayerCamera == null)
                Debug.LogWarning($"Не задан компонент {nameof(PlayerView)}.{nameof(PlayerCamera)}");
            if (Animator == null)
                Debug.LogWarning($"Не задан компонент {nameof(PlayerView)}.{nameof(Animator)}");
            if (PlayerGrabPoint == null)
                Debug.LogWarning($"Не задан компонент {nameof(PlayerView)}.{nameof(PlayerGrabPoint)}");
            if (CharacterController == null)
                Debug.LogWarning($"Не задан компонент {nameof(PlayerView)}.{nameof(CharacterController)}");
            if (FootstepEvent.IsNull)
                Debug.LogWarning($"Не задан компонент {nameof(PlayerView)}.{nameof(FootstepEvent)}");
        }
    }

}
