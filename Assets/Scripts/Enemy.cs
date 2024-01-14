using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : Character
{
    IDamageable target;
    RaycastHit hit;
    EnemyAI ai;

    Transform one;
    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public Animator anim;

    [SerializeField] int enemyType;

    float distance = 20;
    float detectAngle = 180;
    int layer = 1 << 7;
    int lay = ~ ( 1 << 6 );

    bool detected;
    bool fired;

    [SerializeField] ParticleSystem fireP;
    AudioSource audioS;

    void Start()
    {
        gameObject.Ragdoll(false);
        one = transform.GetChild(0).GetComponent<Transform>();
        rigid = one.GetChild(1).GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        anim.SetInteger("Enemy Type", enemyType);
        audioS = gameObject.GetComponent<AudioSource>();
        ai = gameObject.GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (dead) return;
        Detecting();
        if(detected)
        {
            Attack();
            if(ai != null)
            {
                ai.nav.Stop();
            }
        }
        else
        {
            anim.SetBool("Detect", false);
            StopAllCoroutines();
            if (ai != null)
            {
                ai.nav.Resume();
            }
        }
    }

    void Detecting()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, distance, layer);
        if(coll.Length > 0 )
        {
            Vector3 vec = (coll[0].transform.position - transform.position).normalized;
            float angle = Vector3.Angle(vec, transform.forward);
            if(angle < detectAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position, vec, out hit, distance, lay))
                {
                    Debug.DrawRay(transform.position, vec * hit.distance, Color.red);
                    //Debug.Log(hit.transform.name);
                    if (hit.collider.tag == "Player")
                    {
                        
                        target = hit.collider.GetComponent<IDamageable>();
                        detected = true;
                    }
                    else
                    {
                        detected = false;
                        fired = false;
                    }
                }
                else
                {
                    detected = false;
                    fired = false;
                }
            }
        }
    }
    void Attack()
    {
        transform.LookAt(hit.transform);
        anim.SetBool("Detect", true);
        if(!fired)
        {
            StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        fired = true;
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Fire");
        target.OnDamage(25);
        audioS.Play();
        fireP.Play();
        fired = false;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if (hp <= 0 && !dead)
        {
            StopAllCoroutines();
            gameObject.Ragdoll(true);
            rigid.AddForce(-transform.forward * 10000);
            dead = true;
            GameManager.instance.currentPoint += 100;
        }
        distance *= 2;
    }
}
