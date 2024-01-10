using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCtrl : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Transform body;
    [SerializeField] float sensitivity;
    float rotX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.dead) return;
        RotateY();
        RotateX();
    }

    void RotateY()
    {
        Vector3 rotY = Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rotY));
    }

    void RotateX()
    {
        rotX -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        rotX = Mathf.Clamp(rotX, -90, 90);
        body.localEulerAngles = Vector3.right * rotX;
    }
}
