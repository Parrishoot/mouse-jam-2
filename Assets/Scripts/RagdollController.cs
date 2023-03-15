using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    public Rigidbody launchRigidbody;

    public GameObject hips;

    public float launchForce;

    public bool spawned = true;

    public float totalSleepTime = 4f;

    public enum STATE {
        RAGDOLL,
        UP
    }

    public STATE ragdollState = STATE.RAGDOLL;

    private GameObject carObject;

    private bool pickedUp = false;

    private float pullSpeed = 8000f;

    private float totalPushTime = .5f;

    private float pushTime = 0f;

    private float sleepTime = 0f;

    private bool ragdolled = true;

    private bool destroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!spawned) {
            DisableRigidBodies();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ragdolled) {
            if(pushTime >= 0) {
                pushTime -= Time.deltaTime; 
            }

            if(!pickedUp && sleepTime >= 0f) {
                sleepTime -= Time.deltaTime;
            }
            else if(!pickedUp) {
                DisableRigidBodies();
            }
        }
    }

    private void FixedUpdate() {
        if(IsBeingPulled()) {
            launchRigidbody.AddForce(((carObject.transform.position - launchRigidbody.gameObject.transform.position).normalized * pullSpeed * Time.fixedDeltaTime), ForceMode.Impulse);
        }
    }

    public void EnableRigidBodies() {
        GetComponent<Animator>().enabled = false;
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
        }
        sleepTime = totalSleepTime;
        ragdolled = true;
    }

    public void DisableRigidBodies() {
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = true;
        }
        GetComponent<Animator>().enabled = true;
                
        Vector3 originalHipsPosition = hips.transform.position;
        transform.position = launchRigidbody.position;

        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo)) {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
        }

        hips.transform.position = originalHipsPosition;

        ragdolled = false;
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
        return pickedUp && pushTime <= 0 && carObject != null;
    }

    public bool IsFree() {
        return !pickedUp;
    }

    public bool IsPickedUpByThisCar(GameObject car) {
        if(carObject == null) {
            return false;
        }
        return carObject.Equals(car) && IsBeingPulled() && !destroyed;
    }

    public void Init() {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        EnableRigidBodies();
        StartCoroutine(SpawnLaunch(new Vector3(x, 1, z)));
    }

    private IEnumerator SpawnLaunch(Vector3 launchDirection) {
        pickedUp = true;
        yield return new WaitForFixedUpdate();
        launchRigidbody.AddForce(launchDirection * launchForce / 1.5f);
        yield return new WaitForSeconds(.5f);
        pickedUp = false;
        yield return null;
    }

    public void Despawn() {
        destroyed = true;
        Destroy(gameObject);
    }

}
