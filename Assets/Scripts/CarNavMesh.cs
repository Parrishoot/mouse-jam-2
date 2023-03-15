using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavMesh : MonoBehaviour
{

    public float carDetectionRadius = 10f;

    public float clownDetectionRadius = 10f;

    public float wanderRadius = 30f;

    public enum TARGET_TYPE {
        CLOWN,
        CAR,
        WANDER
    }

    public TARGET_TYPE targetType = TARGET_TYPE.CLOWN;

    private Vector3 targetPosition;

    private NavMeshAgent navMeshAgent;

    private bool wandering = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        FindTargets();

        navMeshAgent.destination = targetPosition;
    }

    private void FindTargets() {

        //do the raycast specifying the mask
        Transform closetClown = FindClosestObjectWithinRadius(clownDetectionRadius, "ClownsTarget");
        if(closetClown != null) {
            wandering = false;
            targetPosition = closetClown.transform.position;
            this.targetType = TARGET_TYPE.CLOWN;
            return;
        }

        Transform closetCar = FindClosestObjectWithinRadius(carDetectionRadius, "Player");
        if(closetCar != null) {
            wandering = false;
            targetPosition = closetCar.transform.position;
            this.targetType = TARGET_TYPE.CAR;
            return;
        }

        if((!navMeshAgent.pathPending && !navMeshAgent.hasPath) || wandering == false) {
            targetPosition = GetRandomReachablePoint();
            this.targetType = TARGET_TYPE.WANDER;
            wandering = true;
        }

    }

    private Transform FindClosestObjectWithinRadius(float radius, string layerName) {
        List<Collider> hits = new List<Collider>(Physics.OverlapSphere (transform.position, radius, LayerMask.GetMask(layerName)));
        hits.RemoveAll(x => !ViableTarget(x.gameObject));

        if(hits.Count != 0) {

            GameObject closestObject = hits[0].gameObject;
            float closestDistance = Vector3.Distance(closestObject.transform.position, transform.position);

            foreach(Collider hit in hits) {
                float objectDistance = Vector3.Distance(transform.position, hit.gameObject.transform.position); 
                bool reachable = navMeshAgent.CalculatePath(hit.gameObject.transform.position, new NavMeshPath());
                if(objectDistance < closestDistance) {
                    closestObject = hit.gameObject;
                    closestDistance = objectDistance; 
                }
            }

            return closestObject.transform;
        }

        return null;
    }

    private Vector3 GetRandomReachablePoint() {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
           
        NavMeshHit navHit;
           
        NavMesh.SamplePosition(randomDirection + transform.position, out navHit, wanderRadius, -1);
    
        return navHit.position;
    }

    private bool ViableTarget(GameObject targetObject) {
        return targetObject.transform.root.gameObject != transform.root.gameObject &&
               navMeshAgent.CalculatePath(targetObject.transform.root.position, new NavMeshPath());
    }
}
