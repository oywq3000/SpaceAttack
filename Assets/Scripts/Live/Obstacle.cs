using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Obstacle : LiveEntity
    {
        public GameObject explosionPrefabs;
        private void Start()
        {
            
            //init default Hp
            if (Hp!=0)
            {
                SetHp(Hp);
            }
        }

        protected override void OnDamage()
        {
           
        }

        protected override void OnDead()
        {
            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}