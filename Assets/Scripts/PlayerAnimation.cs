using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerCtrl player;
    Gun gun;

    [SerializeField] Animator body;
    [SerializeField] Animator leg;

    void Start()
    {
        player = GetComponent<PlayerCtrl>();
        gun = GetComponent<Gun>();
    }

    void Update()
    {
        body.SetBool("Crouch", player.crouch);
        leg.SetBool("Crouch", player.crouch);
        float nor = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");
        nor = Mathf.Clamp(nor, -1, 1);
        leg.SetFloat("Move", nor);
    }
    public void Trig(string trigger)
    {
        body.SetTrigger(trigger);
    }
}
