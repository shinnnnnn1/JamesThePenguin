using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerCtrl player;

    [SerializeField] Transform bodyT;
    [SerializeField] Animator body;
    [SerializeField] Animator leg;

    void Start()
    {
        player = GetComponent<PlayerCtrl>();
    }

    void Update()
    {
        body.SetBool("Crouch", player.crouch);
        leg.SetBool("Crouch", player.crouch);
        float nor = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical");
        nor = Mathf.Clamp(nor, -1, 1);
        leg.SetFloat("Move", nor);
    }
}
