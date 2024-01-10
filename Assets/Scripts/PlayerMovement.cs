using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Transform head;
    [SerializeField] float moveSpd;

    Transform center;
    float savedSpd;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        center = Camera.main.transform;
        savedSpd = moveSpd;
    }
    void Update()
    {
        if(GameManager.instance.dead) return;
        RunAndCrouch();
        Action();
    }
    void FixedUpdate()
    {
        if (GameManager.instance.dead) return;
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
            head.DOLocalMoveY(0.64f, 0.3f);
        }
        else if(Input.GetButton("Run"))
        {
            moveSpd = savedSpd * 1.5f;
            head.DOLocalMoveY(1.64f, 0.3f);
        }
        else
        {
            moveSpd = savedSpd;
            head.DOLocalMoveY(1.64f, 0.3f);
        }
    }

    void Action()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Action") && Physics.Raycast(center.position, center.forward, out hit, 4))
        {
            IAction target = hit.collider.GetComponent<IAction>();
            if(target != null)
            {
                target.OnAction();
            }
        }
    }
}
