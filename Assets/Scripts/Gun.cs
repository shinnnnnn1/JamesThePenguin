using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    public enum GunState
    {
        Ready, Empty, Reload
    }
    GunState state;

    [SerializeField] Animator anim;
    [SerializeField] Transform head;
    [SerializeField] Transform recoil;
    [SerializeField] Transform recoilY;
    [SerializeField] Transform fireFront;
    Transform firePos;

    AudioSource audioPlayer;
    [SerializeField] AudioClip fire;
    [SerializeField] AudioClip reload;

    [SerializeField] ParticleSystem fireEff;
    [SerializeField] ParticleSystem hitEff;
    LineRenderer line;

    int oneMagazine = 30;
    int currentFullAmmo;
    int currentAmmo;
    int fireDamage = 40;
    float fireDelay = 0.1f;
    float lastFire;

    void Start()
    {
        state = GunState.Ready;
        currentFullAmmo = oneMagazine * 3;
        currentAmmo = oneMagazine;
        firePos = Camera.main.transform;
        audioPlayer = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
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
        Origin();
    }

    void Fire()
    {
        if (Input.GetButton("Fire") && state == GunState.Ready && Time.time >= lastFire + fireDelay)
        {
            lastFire = Time.time;
            RaycastHit hit;
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, Mathf.Infinity))
            {
                //print(hit.collider.name);
                IDamageable target = hit.collider.GetComponent<IDamageable>();
                if (target != null)
                {
                    //Debug.Log("hit");
                    target.OnDamage(fireDamage);
                }
            }
            Instantiate(hitEff.gameObject, hit.point, head.localRotation * Quaternion.Euler(0, 180, 0));
            Recoil();
            anim.SetTrigger("Fire");
            audioPlayer.PlayOneShot(fire);
            fireEff.Play();
            StopCoroutine(Line(hit.point));
            StartCoroutine(Line(hit.point));
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                state = GunState.Empty;
            }
            UIManager.instance.Ammo(currentAmmo, currentFullAmmo);
        }
    }
    IEnumerator Line(Vector3 hitPoint)
    {
        line.enabled = true;
        line.SetPosition(0, fireFront.position);
        line.SetPosition(1, hitPoint);
        yield return new WaitForSeconds(0.01f);
        line.enabled = false;
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
            anim.SetTrigger("Reload");
            audioPlayer.PlayOneShot(reload);
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
        yield return new WaitForSeconds(0.5f);
        state = GunState.Ready;
    }

    void Origin()
    {
        recoil.DOLocalRotate(Vector3.zero, 2f);
        recoilY.DOLocalRotate(Vector3.zero, 2f);
    }

    void Recoil()
    {
        float rec = Random.Range(-1, 1);
        recoil.DOPunchRotation(new Vector3(-3 * 3, 0, 0), 0.5f);
        recoilY.DOPunchRotation(new Vector3(0, rec * 10, 0), 0.5f);
    }
}
