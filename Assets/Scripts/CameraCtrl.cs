using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] Transform pos;

    void Update()
    {
        transform.position = pos.position;
    }
}
