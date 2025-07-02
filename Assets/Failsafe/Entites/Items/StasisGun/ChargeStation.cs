using System;
using UnityEngine;

public class ChargeStation : MonoBehaviour
{
    [SerializeField] Transform _posForPistolGO;
    [SerializeField]int _containedChargeAmount= 2000;
    
    private void OnTriggerEnter(Collider other)
    {
        StasisGunShoot stasisGun = other.GetComponent<StasisGunShoot>();
        //other.transform.position = _posForPistolGO.position;
        //other.GetComponent<Rigidbody>().isKinematic = true;
        if(stasisGun != null)
        {
            int _chargeAmount = stasisGun.GetAmountForMax();
            if (_containedChargeAmount >= _chargeAmount && !stasisGun.IsFull() )
            {
                stasisGun.Reload(_chargeAmount);
                _containedChargeAmount -= _chargeAmount;
                if(_containedChargeAmount == 0)
                    Destroy(this.gameObject);
            }
            
        }
    }
    
    
}
