using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Transform body;
    [SerializeField][Range(1f, 10f)] float moveSpd = 10;
    [SerializeField][Range(0.1f, 5f)] float sensitivity;
    public float rotX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
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
}
