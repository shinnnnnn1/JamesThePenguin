using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Transform one;
    public Rigidbody rigid;
    public Animator anim;

    [SerializeField] int enemyType;

    [SerializeField] float distance;
    float detectAngle = 180;

    int layer = 1 << 7;
    [SerializeField] LayerMask laaa;
    //int myLayer = 1 << 0 | 1 << 7;

    void Start()
    {
        gameObject.Ragdoll(false);
        one = transform.GetChild(0).GetComponent<Transform>();
        rigid = one.GetChild(1).GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        anim.SetInteger("Enemy Type", enemyType);
    }

    void Update()
    {
        if (dead) return;
        Detecting();
    }

    void Detecting()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, distance, layer);
        if(coll.Length > 0 )
        {
            Vector3 vec = (coll[0].transform.position - transform.position).normalized;
            //Debug.Log(coll[0].transform.name);
            float angle = Vector3.Angle(vec, transform.forward);
            if(angle < detectAngle * 0.5f)
            {
                Debug.Log("In");
                if (Physics.Raycast(transform.position + Vector3.up, vec + Vector3.up, Mathf.Infinity, laaa))
                {
                    Debug.DrawRay(transform.position + Vector3.up, (vec * distance) + Vector3.up, Color.red);
                    //Debug.Log(hit.transform.name);
                }
                else
                {
                    Debug.DrawRay(transform.position + Vector3.up, (vec * distance) + Vector3.up, Color.green);
                }
            }
        }
    }

    public override void OnDamage(float damage)
    {
        distance *= 2;
        base.OnDamage(damage);
        if (hp <= 0 && !dead)
        {
            gameObject.Ragdoll(true);
            rigid.AddForce(-transform.forward * 10000);
            dead = true;
        }
    }
}
// Physics.Raycast(Vector3 origin, Vector3 direction, float distance)
// Debug.DrawRay(Vector3 origin, Vector3 direction * float distance)