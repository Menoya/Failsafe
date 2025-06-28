using System;
using UnityEngine;

public class ChargeStation : MonoBehaviour
{
    [SerializeField] Transform _posForPistolGO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //other.transform.position = _posForPistolGO.position;
        //other.GetComponent<Rigidbody>().isKinematic = true;
        if(other.GetComponent<StasisGunShoot>() != null)
        {
            other.GetComponent<StasisGunShoot>().ChargeAmountCurrent = 100;
        }
    }
    
    
}
