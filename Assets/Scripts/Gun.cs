using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunState
    {
        Default, Fire, Reload
    }
    public GunState state;

    float oneMagazine = 30;
    float ammo = 120;
    float currentAmmo;

    void Update()
    {
        if(Input.GetButtonDown("Fire") && GameManager.instance.state == GameManager.State.Default)
        {

        }
    }

}
