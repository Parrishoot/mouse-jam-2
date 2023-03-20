using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCarAudioPitcher : MonoBehaviour
{
    public float pitchOffset = .5f;
    public float maxVelocityForPitchOffset = 5f;

    private AudioSource audioSource;
    private Rigidbody carRigidbody;
    
    public virtual void Start() {
        audioSource = GetComponent<AudioSource>();
        carRigidbody = GetComponent<Rigidbody>();
    }

    public void Update() {
        Vector3 currentVelocity = GetCurrentVelocity();
        audioSource.pitch = 1 + ((Mathf.Abs(currentVelocity.x) + Mathf.Abs(currentVelocity.z)) / maxVelocityForPitchOffset) * pitchOffset;
    }

    public virtual Vector3 GetCurrentVelocity() {
        return carRigidbody.velocity;
    }
}
