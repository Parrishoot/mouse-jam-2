using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIClownCarAudioPitcher : ClownCarAudioPitcher
{

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    public override void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        base.Start();
    }

    public override Vector3 GetCurrentVelocity()
    {
        return navMeshAgent.velocity;
    }
}
