using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public float hp = 100;
    public bool dead;

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        hp -= damage;
        if(hp <= 0 && !dead)
        {
            dead = true;
        }
    }
}
