using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{

    public float speed;
    protected Rigidbody carRigidbody;

    public PeopleSpawner peopleSpawner;

    public ClownCounter clownCounter;

    public float shakeAmountOnHit = 0f;
    public float shakeTimeOnHit = 0f;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(LayerMask.NameToLayer("Clowns") == other.gameObject.layer) {
            RagdollController controller = other.gameObject.GetComponentInParent<RagdollController>();
            
            if(controller.IsFree()) {
                controller.Launch(gameObject, GetLaunchDirection());
            }
            else if(controller.IsPickedUpByThisCar(gameObject)) {
                clownCounter.AddClown();
                controller.Despawn();
            }
        }
    }

    private Vector3 GetLaunchDirection() {
        return (Vector3.up + Vector3.up + (transform.forward)).normalized;
    }

    public void Clownsplosion() {
        CinemachineShake.GetInstance().SetShake(shakeAmountOnHit, shakeTimeOnHit);
        int lostClowns = clownCounter.LoseClowns();
        peopleSpawner.SendInTheClowns(lostClowns);
    }
}
