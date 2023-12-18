using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour, IDamageable
{

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        
    }
}
