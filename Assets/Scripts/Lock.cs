using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float limit;
    [SerializeField] float rotY;
    float rotX;

    void LateUpdate()
    {
        transform.rotation = player.rotation;

        rotX -= Input.GetAxisRaw("Mouse Y") * 3;
        rotX = Mathf.Clamp(rotX, -limit, limit);
        //transform.localEulerAngles = Vector3.up * rotY + Vector3.right * rotX;
    }
}
