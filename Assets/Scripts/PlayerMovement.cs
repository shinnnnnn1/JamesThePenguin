using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Transform head;
    [SerializeField] float moveSpd;
    float savedSpd;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        savedSpd = moveSpd;
    }
    void Update()
    {
        RunAndCrouch();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 hor = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 ver = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 dir = (hor + ver).normalized * moveSpd;
        rigid.MovePosition(rigid.position + dir * Time.deltaTime);
    }
    void RunAndCrouch()
    {
        if (Input.GetButton("Crouch"))
        {
            moveSpd = savedSpd * 0.5f;
            head.DOLocalMoveY(1.64f, 0.3f);
        }
        else if(Input.GetButton("Run"))
        {
            moveSpd = savedSpd * 1.5f;
            head.DOLocalMoveY(2.64f, 0.3f);
        }
        else
        {
            moveSpd = savedSpd;
            head.DOLocalMoveY(2.64f, 0.3f);
        }
    }
}
