using UnityEngine;

namespace Failsafe.Items
{
    public class StasisGun : IUsable, IUpdatable, IShootable
    {
        StasisGunData _data;
        EnergyContainer _energyContainer;
        private bool _isDefaultMode = true;
        float _fireRateTimer = 0;

        public StasisGun(StasisGunData data)
        {
            _data = data;
            _energyContainer = new EnergyContainer(_data);
        }

        public void Update()
        {
            if (_fireRateTimer > 0)
            {
                _fireRateTimer -= Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _isDefaultMode = !_isDefaultMode;
                Debug.Log("Default mode is " + _isDefaultMode);
            }
            Debug.Log("UpdateInvoked");
        }


        public void Use()
        {
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, transform.up, out hit))
            //{
            //    Debug.DrawRay(transform.position, transform.up * hit.distance, Color.green);
            //    Debug.Log("Object ahead: " + hit.collider.name);
            //
            //}
            //else
            //{
            //    Debug.DrawRay(transform.position, transform.up, Color.red);
            //    Debug.Log("No Object!");
            //}
            //Shoot(hit);
        }


        public void ChangeMode()
        {
            _isDefaultMode = !_isDefaultMode;
            Debug.Log("Default mode is " + _isDefaultMode);
        }


        public void Shoot(RaycastHit hit)
        {
            if (_fireRateTimer <= 0 && !_energyContainer.IsEmpty())
            {
                _fireRateTimer = _data.FireRate;
                _energyContainer.UseChargeAmount();
                if (_isDefaultMode)
                    DefaultMode(hit);
                else
                    AltMode(hit);
            }
        }

        void DefaultMode(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Stasisable>() != null)
            {
                hit.collider.GetComponent<Stasisable>().StartStasis(_data.StasisDuration);
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
                hit.collider.GetComponent<Stasisable>().StartStasisWithInertion(_data.StasisDuration);
            }
            else if (hit.collider.GetComponentInParent<Enemy>() != null)
            {
                hit.collider.GetComponentInParent<Enemy>().DisableState();
            }
        }


    }

}
