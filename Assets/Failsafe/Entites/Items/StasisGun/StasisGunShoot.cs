using System;
using UnityEngine;

public class StasisGunShoot : MonoBehaviour
{
    public StasisGunData Data;

    float _fireRateTimer = 0;
    [SerializeField]int ChargeAmountCurrent;
    private bool _isMDefaultMode = true;

    private void Start()
    {
        ChargeAmountCurrent = Data.ChargeAmountMax;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up*10, Color.yellow);
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
        
        if (_fireRateTimer <= 0 && ChargeAmountCurrent > 0)
        {
            _fireRateTimer = Data.FireRate;
            ChargeAmountCurrent -= 7;
            ChargeAmountCurrent = Mathf.Clamp(ChargeAmountCurrent, 0, Data.ChargeAmountMax);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit))
            {
                Debug.DrawRay(transform.position, transform.up * hit.distance, Color.green);
                Debug.Log("Object ahead: " + hit.collider.name);
                if(_isMDefaultMode)
                    DefaultMode(hit);
                else
                    AltMode(hit);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.up, Color.red);
                Debug.Log("No Object!" );
            }
        }
        else
        {
            Debug.Log("Not yet!");
        }
    }

    public void Reload(int amount)
    {
        ChargeAmountCurrent +=  amount;
        ChargeAmountCurrent = Mathf.Clamp(ChargeAmountCurrent, 0, Data.ChargeAmountMax);
    }

    public bool IsFull()
    {
        return ChargeAmountCurrent == Data.ChargeAmountMax;
    }

    public int GetAmountForMax()
    {
        return Data.ChargeAmountMax - ChargeAmountCurrent;
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
