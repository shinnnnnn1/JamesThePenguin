using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Transform body;
    [SerializeField][Range(1f, 10f)] float moveSpd = 10;
    [SerializeField][Range(0.1f, 5f)] float sensitivity;
    float rotX;
    float savedSpd;
    float top = 2.462f;
    float bottom = 1.55f;
    float target;

    [HideInInspector] public bool crouch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigid = GetComponent<Rigidbody>();
        savedSpd = moveSpd;
    }
    void Update()
    {
        Crouch();
    }
    void FixedUpdate()
    {
        Move();
        RotationY();
    }
    void LateUpdate()
    {
        RotationX();
    }
    void Move()
    {
        Vector3 hor = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 ver = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 veloc = (hor + ver).normalized * moveSpd;
        rigid.MovePosition(transform.position + veloc * Time.deltaTime);
    }
    void RotationY()
    {
        Vector3 rotY = Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rotY));
    }
    void RotationX()
    {
        rotX -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        rotX = Mathf.Clamp(rotX, -90, 90);
        body.localEulerAngles = Vector3.right * rotX;
    }
    void Crouch()
    {
        crouch = Input.GetButton("Crouch");
        if(crouch)
        {
            moveSpd = savedSpd * 0.5f;
            target = Mathf.Lerp(body.position.y, bottom, 0.25f);
            body.position = new Vector3(body.position.x, target, body.position.z);
        }
        else
        {
            moveSpd = savedSpd * 1;
            target = Mathf.Lerp(body.position.y, top, 0.25f);
            body.position = new Vector3(body.position.x, target, body.position.z);
        }
    }
}
