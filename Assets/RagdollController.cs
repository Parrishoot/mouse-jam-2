using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    
    public Rigidbody launchRigidbody;

    public float launchForce;

    private bool flying = false;

    // Start is called before the first frame update
    void Start()
    {
        DisableRigidBodies();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Launch(Vector3 direction) {
        if(!flying) {
            Debug.Log("Flying!");
            EnableRigidBodies();
            launchRigidbody.AddForce(direction * launchForce, ForceMode.Force);
            flying = true;
        }
    }
}
