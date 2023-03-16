using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownWanderer : NavMeshWanderer
{
    Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if(!navMeshAgent.pathPending && !navMeshAgent.hasPath) {
            targetPosition = GetRandomReachablePoint();
        }

        navMeshAgent.destination = targetPosition;
    }

    public void BeginWander() {
        enabled = true;
    }

    public void EndWander() {
        enabled = false;
    }
}
