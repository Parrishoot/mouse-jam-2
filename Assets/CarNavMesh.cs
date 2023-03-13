using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavMesh : MonoBehaviour
{

    public float carDetectionRadius = 10f;

    public float clownDetectionRadius = 10f;

    public float wanderRadius = 30f;

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
        Transform closetClown = FindClosestObjectWithinRadius(clownDetectionRadius, "Clowns");
        if(closetClown != null) {
            wandering = false;
            targetPosition = closetClown.transform.position;
            return;
        }

        Transform closetCar = FindClosestObjectWithinRadius(carDetectionRadius, "Player");
        if(closetCar != null) {
            wandering = false;
            targetPosition = closetCar.transform.position;
            return;
        }

        if((!navMeshAgent.pathPending && !navMeshAgent.hasPath) || wandering == false) {
            targetPosition = GetRandomReachablePoint();
            wandering = true;
        }

    }

    private Transform FindClosestObjectWithinRadius(float radius, string layerName) {
        List<Collider> hits = new List<Collider>(Physics.OverlapSphere (transform.position, radius, LayerMask.GetMask(layerName)));
        hits.RemoveAll(x => x.transform.root.gameObject == transform.root.gameObject);

        if(hits.Count != 0) {

            GameObject closestObject = hits[0].gameObject;
            float closestDistance = Vector3.Distance(closestObject.transform.position, transform.position);

            foreach(Collider hit in hits) {
                float objectDistance = Vector3.Distance(transform.position, hit.gameObject.transform.position); 
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
}
