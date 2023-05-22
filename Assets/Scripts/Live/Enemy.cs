using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy : LiveEntity
    {
        public float hp = 100;
        
        private void Start()
        {
            if (Hp!=0)
            {
                SetHp(hp);
            }
        }

        protected override void OnDamage()
        {
           
        }

        protected override void OnDead()
        {
          
        }
    }
}