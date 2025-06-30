using UnityEngine;
using Failsafe.Scripts.Health;
using VContainer;

public class Healer : MonoBehaviour
{
    public HealData Data;
    [Inject] private IHealth _health;

    public void Heal() =>
        _health.AddHealth(Data.HealAmount);
}
