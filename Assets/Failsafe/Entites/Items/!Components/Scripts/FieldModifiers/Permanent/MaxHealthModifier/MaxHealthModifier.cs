using Failsafe.Scripts.Health;
using Failsafe.Scripts.Modifiebles;
using UnityEngine;
using VContainer;

public class MaxHealthModifier : MonoBehaviour
{
    public MaxHealthModifierData Data;
    public AdderFloat MaxHealthModificator;
    [Inject] private IHealth _health;

    void Start() =>
        MaxHealthModificator = new AdderFloat(Data.MaxHealthDelta, 0);

    public void ChangeMaxHealth()
    {
        if (_health is PlayerHealth playerHealth)
            playerHealth.ModifyMaxHealth(MaxHealthModificator); 
        else
            Debug.Log("Не удалось преобразовать к PlayerHealth");
    }
}
