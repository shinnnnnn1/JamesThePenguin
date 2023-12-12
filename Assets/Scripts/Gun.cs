using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunState
    {
        Ready, Empty, Reload
    }
    public GunState state;

    public Transform firePos;
    PlayerAnimation anim;
    PlayerCtrl player;

    public int oneMagazine = 30;
    public int currentFullAmmo;
    public int currentAmmo;

    public float fireDelay;
    public float fireDamage;

    float lastFire;

    public int layer = 1 << 6;

    void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        player = GetComponent<PlayerCtrl>();
        currentFullAmmo = oneMagazine * 3;
        currentAmmo = oneMagazine;
        state = GunState.Ready;
    }
    void Update()
    {
        if(GameManager.instance.state != GameManager.State.Default)
        {
            return;
        }
        Fire();
        DrawR();
        Reload();
    }
    void Fire()
    {
        if (Input.GetButton("Fire") && state == GunState.Ready && Time.time >= lastFire + fireDelay)
        {
            lastFire = Time.time;
            RaycastHit hit;
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.point);
                IDamageable target = hit.collider.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.OnDamage(fireDamage, hit.point, hit.normal);
                }
            }
            anim.Trig("Fire");
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                state = GunState.Empty;
            }
            UIManager.instance.Ammo(currentAmmo, currentFullAmmo);
            player.Recoil();
        }
    }
    void DrawR()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePos.position, firePos.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(firePos.position, firePos.forward * hit.distance, Color.green);
        }
    }

    void Reload()
    {
        if(Input.GetButton("Reload") && state != GunState.Reload &&
            currentAmmo != oneMagazine && currentFullAmmo > 0)
        {
            StartCoroutine(Reloding());
            anim.Trig("Reload");
        }
    }
    IEnumerator Reloding()
    {
        state = GunState.Reload;
        yield return new WaitForSeconds(2f);
        int reloadAmmo = oneMagazine - currentAmmo;
        currentFullAmmo -= reloadAmmo;
        if(currentFullAmmo < 0)
        {
            currentAmmo += -currentFullAmmo;
            currentFullAmmo = 0;
        }
        else
        {
            currentAmmo = oneMagazine;
        }
        UIManager.instance.Ammo(currentAmmo, currentFullAmmo);
        yield return new WaitForSeconds(1f);
        state = GunState.Ready;
    }
}
