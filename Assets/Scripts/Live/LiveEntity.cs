using System.Collections;
using System.Collections.Generic;using DefaultNamespace.Live;
using UnityEngine;

public abstract class LiveEntity :MonoBehaviour,ICanDamageable
{
    public float Hp = 100;

    public void Damage(float damage)
    {
        Hp -= damage;
        if (Hp<=0)
        {
            Hp = 0;
            OnDead();
            return;
        }
        OnDamage();
    }

    protected void SetHp(float hp)
    {
        Hp = hp;
    }
    

    //event abstract method
    protected abstract void OnDamage();
    protected abstract void OnDead();
}
