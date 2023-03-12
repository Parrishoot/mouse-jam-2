using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    public Rigidbody launchRigidbody;

    public float launchForce;

    public bool spawned = true;

    private GameObject carObject;

    private bool pickedUp = false;

    private float pullSpeed = 8000f;

    private float totalPushTime = .5f;

    private float spawnCooldown = .5f;

    private float pushTime = 0f;

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
        if(pushTime >= 0) {
            pushTime -= Time.deltaTime; 
        }
    }

    private void FixedUpdate() {
        if(IsBeingPulled()) {
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
