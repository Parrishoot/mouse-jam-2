using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{

    public float speed;
    private Rigidbody carRigidbody;

    private float movement;

    private float targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            movement = 1;
        }
        else if(Input.GetMouseButton(1)) {
            movement = -1;
        }
        else {
            movement = 0;
        }
    }

    private void FixedUpdate() {
        if(transform.eulerAngles.y != targetRotation) {
            transform.eulerAngles = new Vector3(0, targetRotation, 0);
        }
        carRigidbody.AddForce(transform.right * movement * speed * Time.fixedDeltaTime, ForceMode.Impulse);    
    }
    
    public void SetRotation(float rotation) {
        targetRotation = rotation;
    }

    private void OnCollisionEnter(Collision other) {
        if(LayerMask.NameToLayer("Clowns") == other.gameObject.layer) {
            RagdollController controller = other.gameObject.GetComponentInParent<RagdollController>();
            
            if(controller.IsFree()) {
                controller.Launch(gameObject, GetLaunchDirection());
            }
            else if(controller.IsPickedUpByThisCar(gameObject)) {
                Destroy(controller.gameObject);
            }
        }
    }

    private Vector3 GetLaunchDirection() {
        return (Vector3.up + (transform.right).normalized).normalized;
    }
}
