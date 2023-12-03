using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] Transform pos;
    [SerializeField] Transform up;

    [SerializeField] Vector3 dsdsd;
    public Vector3 distance;
    private void Start()
    {
        //distance = pos.position - transform.position;
    }
    void Update()
    {
        //transform.position = pos.position + distance;
        //transform.rotation = pos.rotation * Quaternion.Euler(0, 90, 90);
        dsdsd = up.position;
    }
}
