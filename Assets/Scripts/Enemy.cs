using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public  Rigidbody rigid;
    bool action;

    void Start()
    {
        gameObject.Ragdoll(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            print("up");
            rigid.AddForce(Vector3.up * 10000);
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if(dead && !action)
        {
            gameObject.Ragdoll(true);
            rigid.AddForce(-transform.forward * 10000);
            action = true;
        }
    }
}
