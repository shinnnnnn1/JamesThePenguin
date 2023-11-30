using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public enum EnemyType { Idle, Walk, Crouch, CrouchToStand};
    public EnemyType enemyType;

    public GameObject rifle;

    [SerializeField] bool rifleR;


    [SerializeField] Quaternion startR;
    [SerializeField] Vector3 startP;

    [SerializeField] Quaternion fireR;
    [SerializeField] Vector3 fireP;

    void Update()
    {

    }

}
