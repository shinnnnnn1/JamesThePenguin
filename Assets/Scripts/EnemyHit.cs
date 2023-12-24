using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour, IDamageable
{
    public bool head;

    public void OnDamage(float damage)
    {
        Enemy enemy = GetComponentInParent<Enemy>();
        int oneTap = head ? 3 : 1;
        enemy.OnDamage(damage * oneTap);
    }
}
