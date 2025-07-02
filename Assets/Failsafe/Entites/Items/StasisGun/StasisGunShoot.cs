using System;
using UnityEngine;

public class StasisGunShoot : MonoBehaviour
{
    public StasisGunData Data;

    float _fireRateTimer = 0;
    public float ChargeAmountCurrent;
    private bool _isMDefaultMode = true;

    private void Start()
    {
        ChargeAmountCurrent = Data.ChargeAmountMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            _isMDefaultMode = !_isMDefaultMode;
            Debug.Log("Default mode is " + _isMDefaultMode);
        }
           
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
                if(_isMDefaultMode)
                    DefaultMode(hit);
                else
                    AltMode(hit);
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
    
    void DefaultMode(RaycastHit hit)
    {
        if (hit.collider.GetComponent<Stasisable>() != null)
        {
            hit.collider.GetComponent<Stasisable>().StartStasis(Data.StasisDuration);
        }
        else if (hit.collider.GetComponentInParent<Enemy>() != null)
        {
            hit.collider.GetComponentInParent<Enemy>().DisableState();
        }
    }

    void AltMode(RaycastHit hit)
    {
        if (hit.collider.GetComponent<Stasisable>() != null)
        {
            hit.collider.GetComponent<Stasisable>().StartStasisWithInertion(Data.StasisDuration);
        }
        else if (hit.collider.GetComponentInParent<Enemy>() != null)
        {
            hit.collider.GetComponentInParent<Enemy>().DisableState();
        }
    }
}
