using System;
using UnityEngine;

public class ChargeStation : MonoBehaviour
{
    [SerializeField] Transform _posForPistolGO;
    [SerializeField]int _containedChargeAmount= 2000;
    
    private void OnTriggerEnter(Collider other)
    {
        ChargeAmount chargeAmoount = other.GetComponent<ChargeAmount>();
        //other.transform.position = _posForPistolGO.position;
        //other.GetComponent<Rigidbody>().isKinematic = true;
        if(chargeAmoount != null)
        {
            int _chargeAmount = chargeAmoount.GetAmountForMax();
            if (_containedChargeAmount >= _chargeAmount && !chargeAmoount.IsFull() )
            {
                chargeAmoount.Reload(_chargeAmount);
                _containedChargeAmount -= _chargeAmount;
                if(_containedChargeAmount == 0)
                    Destroy(this.gameObject);
            }
            
        }
    }
    
    
}
