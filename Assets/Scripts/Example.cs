using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Example : MonoBehaviour
{

    [SerializeField] float distance;
    float detectAngle = 180;
    int layer = 1 << 7;

    [SerializeField] Vector3 vec;
    [SerializeField] float angle;

    void Update()
    {
        Detecting();
    }
    void Detecting()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, distance, layer);
        if (coll.Length > 0)
        {
            vec = (coll[0].transform.position - transform.position).normalized;
            //Debug.Log(coll[0].transform.name);
            angle = Vector3.Angle(vec, transform.forward);
            if (angle < detectAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position, vec, out RaycastHit hit, distance))
                {
                    Debug.DrawRay(transform.position, (vec * hit.distance), Color.red);
                    Debug.Log(hit.transform.name);
                }
                else
                {
                    Debug.DrawRay(transform.position, (vec * distance), Color.green);
                }
            }
        }
    }
}
