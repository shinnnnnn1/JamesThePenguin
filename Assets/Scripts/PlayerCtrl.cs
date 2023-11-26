using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody rigid;
    public Camera cam;
    public Transform rotateObj;
    [SerializeField] float moveSpd = 10;
    [SerializeField] float sensitivity;
    float rotX;
    GameObject awrjvdn;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        Move();
        CameraRotationX();
        RotationY();
    }
    void Move()
    {
        Vector3 hor = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 ver = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 veloc = (hor + ver).normalized * moveSpd;
        rigid.MovePosition(transform.position + veloc * Time.deltaTime);
    }
    void CameraRotationX()
    {
        rotX -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        rotX = Mathf.Clamp(rotX, -90, 90);
        //cam.transform.localEulerAngles = Vector3.right * rotX;
        rotateObj.localEulerAngles = Vector3.right* rotX;
    }
    void RotationY()
    {
        Vector3 rotY = Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rotY));
    }
}
