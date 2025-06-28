using System;
using UnityEngine;

public class StasisGunShoot : MonoBehaviour
{
    public StasisGunData Data;

    float _fireRateTimer = 0;
    public float ChargeAmountCurrent;

    private void Start()
    {
        ChargeAmountCurrent = Data.ChargeAmountMax;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_fireRateTimer > 0)
        {
            _fireRateTimer -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        Debug.DrawRay(transform.position, transform.up, Color.green);
        if (_fireRateTimer <= 0 && ChargeAmountCurrent > 0)
        {
            _fireRateTimer = Data.FireRate;
            Debug.Log("Shoot");
            ChargeAmountCurrent -= 7;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit))
            {
                Debug.DrawRay(transform.position, transform.up * hit.distance, Color.red);
                Debug.Log("Object ahead: " + hit.collider.name);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.up, Color.yellow);
                Debug.Log("Object ahead: " );
            }
        }
        else
        {
            Debug.Log("Not yet!");
        }
    }
}
