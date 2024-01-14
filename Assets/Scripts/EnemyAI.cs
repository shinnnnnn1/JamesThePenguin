using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Enemy enemy;
    [HideInInspector] public NavMeshAgent nav;

    [SerializeField] Transform[] points;
    [SerializeField] float repeatTime;
    int count;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        nav = GetComponent<NavMeshAgent>();
        InvokeRepeating("MovePoint", 0, repeatTime);
    }

    void MovePoint()
    {
        if(enemy.rigid.velocity == Vector3.zero)
        {
            nav.SetDestination(points[count++].position);
            if(count >= points.Length)
            {
                count = 0;
            }
        }
    }
}
