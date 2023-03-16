using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshWanderer : MonoBehaviour
{

    public float wanderRadius = 30f;
    protected NavMeshAgent navMeshAgent;


     // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    protected Vector3 GetRandomReachablePoint() {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
        
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection + transform.position, out navHit, wanderRadius, 1);
    
        return navHit.position;
    }
}
