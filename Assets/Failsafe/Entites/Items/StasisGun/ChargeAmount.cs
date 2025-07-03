using UnityEngine;

public class ChargeAmount : MonoBehaviour
{
    public StasisGunData Data;
    [SerializeField]int _chargeAmountCurrent;
    
    private void Start()
    {
        _chargeAmountCurrent = Data.ChargeAmountMax;
    }
    
    public void Reload(int amount)
    {
        _chargeAmountCurrent +=  amount;
        _chargeAmountCurrent = Mathf.Clamp(_chargeAmountCurrent, 0, Data.ChargeAmountMax);
    }

    public void UseChargeAmount()
    {
        _chargeAmountCurrent -= 1;
        _chargeAmountCurrent = Mathf.Clamp(_chargeAmountCurrent, 0, Data.ChargeAmountMax);
    }

    public bool IsFull()
    {
        return _chargeAmountCurrent == Data.ChargeAmountMax;
    }

    public bool IsEmpty()
    {
        return _chargeAmountCurrent == 0;
    }

    public int GetAmountForMax()
    {
        return Data.ChargeAmountMax - _chargeAmountCurrent;
    }
}
