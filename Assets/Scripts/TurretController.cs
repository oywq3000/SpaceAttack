using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TurretController : MonoBehaviour
    {
        public GameObject weaponPrefab;
        public GameObject[] barrelHardpoints;
        public float shotSpeed;
        public float attackSpeed = 5;
        private int _barrelIndex = 0;

        private float _timer = 0;
        
        

        private void Update()
        {
            if (_timer<1/attackSpeed)
            {
                _timer += Time.deltaTime;
                return;
            }
            else
            {
                _timer = 0;
            }
            
            
            if (barrelHardpoints != null)
            {
                GameObject bullet = Instantiate(weaponPrefab, barrelHardpoints[_barrelIndex].transform.position,
                    transform.rotation);
               // bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shotSpeed);
                bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;

                bullet.transform.localScale = Vector3.one*5;

                //cycle shooting 
                _barrelIndex++;
                if (_barrelIndex >= barrelHardpoints.Length)
                    _barrelIndex = 0;
            }
        }
    }
}