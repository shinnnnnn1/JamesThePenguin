using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePos : MonoBehaviour
{
    //mixamorig:RightHand
    [SerializeField] Transform hand;
    [SerializeField] Vector3 pos;
    [SerializeField] Quaternion rot;

    void Start()
    {
        if(hand == null)
        {
            //transform.position = pos;
            transform.rotation = rot;

        }
    }

    void Update()
    {
        if(hand != null)
        {
            transform.position = hand.position + pos;
            transform.rotation = hand.rotation * rot;
        }
        else
        {
            //transform.position = pos;
            //transform.rotation = rot;
        }
    }
}
