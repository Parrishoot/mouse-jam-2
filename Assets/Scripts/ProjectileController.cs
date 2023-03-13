using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed;

    public GameObject spawnObject;

    public GameObject confettiPrefab;

    public Transform confettiSpawnTransform;

    public Rigidbody projectileRigidbody;

    private Vector3 direction;

    private bool destroyed = false;

    public void Init(GameObject spawnObject, Vector3 direction) {
        this.direction = direction;
        this.spawnObject = spawnObject;
        transform.eulerAngles = spawnObject.transform.eulerAngles;
        projectileRigidbody.freezeRotation = true;
    }

    private void FixedUpdate() {
        projectileRigidbody.AddForce(direction * Time.fixedDeltaTime * projectileSpeed, ForceMode.Impulse);        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Car" && other.gameObject != spawnObject && !destroyed) {
            other.GetComponent<CarMovementController>().Clownsplosion();
            Despawn();
        }

    }

    private void OnCollisionEnter(Collision other) {
         Despawn();
    }

    private void Despawn() {
        destroyed = true;
        Destroy(gameObject);
        Instantiate(confettiPrefab, confettiSpawnTransform.position, Quaternion.identity);
    }
}
