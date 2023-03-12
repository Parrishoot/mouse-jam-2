using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    public Rigidbody launchRigidbody;

    public float launchForce;

    private GameObject carObject;

    private bool pickedUp = false;

    private float pullSpeed = 8000f;

    private float totalPushTime = .7f;

    private float pushTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        DisableRigidBodies();
    }

    // Update is called once per frame
    void Update()
    {
        if(pushTime >= 0) {
            pushTime -= Time.deltaTime; 
        }
    }

    private void FixedUpdate() {
        if(IsBeingPulled()) {
            Debug.Log("Pulled");
            launchRigidbody.AddForce(((carObject.transform.position - launchRigidbody.gameObject.transform.position).normalized * pullSpeed * Time.fixedDeltaTime), ForceMode.Impulse);
        }
    }

    public void EnableRigidBodies() {
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = false;
        }
    }

    public void DisableRigidBodies() {
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = true;
        }
    }

    public void Launch(GameObject car, Vector3 direction) {
        if(!pickedUp) {
            pickedUp = true;
            pushTime = totalPushTime;
            EnableRigidBodies();
            launchRigidbody.AddForce(direction * launchForce);
            carObject = car;
        }
    }

    private bool IsBeingPulled() {
        return pickedUp && pushTime <= 0;
    }

    public bool IsFree() {
        return !pickedUp;
    }

    public bool IsPickedUpByThisCar(GameObject car) {
        if(carObject == null) {
            return false;
        }
        return carObject.Equals(car) && IsBeingPulled();
    }

}
