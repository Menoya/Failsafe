using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
// Конечная точка
public class PowerEndPoint : PowerNode
{
    public UnityEvent<bool> onPowered; // событие, которое вызывается при питании

    protected override void OnPowered()
    {
        base.OnPowered();
        Debug.Log($"{name} запитан!");
        onPowered?.Invoke(IsPowered);
    }
    protected override void OnPowerLost()
    {
        base.OnPowerLost();
        onPowered?.Invoke(IsPowered);
    }
}
