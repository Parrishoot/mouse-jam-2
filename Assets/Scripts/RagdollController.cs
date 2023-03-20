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
        UP,
        RESETTING,
    }

    public STATE ragdollState = STATE.RAGDOLL;

    public Animator animator;

    public RandomSoundController randomSoundController;

    private GameObject carObject;

    public bool pickedUp = false;

    public float timeToResetTheBones;

    public ClownWanderer clownWanderer;

    private float pullSpeed = 8000f;

    private float totalPushTime = .5f;

    private float pushTime = 0f;

    private float sleepTime = 0f;

    private bool ragdolled = true;

    private bool destroyed = false;

    private float elaspedResetBonesTime;

    private class BoneTransform {
        public Vector3 Position {get; set;}
        public Quaternion Rotation {get; set;}
    }

    private BoneTransform[] standupTransforms;
    private BoneTransform[] ragdollTransforms;
    private Transform[] bones;

    void Awake() {
        bones = launchRigidbody.GetComponentsInChildren<Transform>();
        standupTransforms = new BoneTransform[bones.Length];
        ragdollTransforms = new BoneTransform[bones.Length];

        for(int boneIndex = 0; boneIndex < bones.Length; boneIndex++) {
            standupTransforms[boneIndex] = new BoneTransform();
            ragdollTransforms[boneIndex] = new BoneTransform();
        }

        PopuatieAnimationStartBoneTransforms("GettingUp", standupTransforms);
    }

    // Start is called before the first frame update
    void Start()
    {

        if(!spawned) {
            DisableRigidBodies();
            
        }
        else {
            EnableRigidBodies();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(ragdollState == STATE.RAGDOLL) {
            if(pushTime >= 0) {
                pushTime -= Time.deltaTime; 
            }

            if(!pickedUp && sleepTime >= 0f) {
                sleepTime -= Time.deltaTime;
            }
            else if(!pickedUp) {

                // Align hips rotation
                Vector3 originalHipsPosition = hips.transform.position;
                Quaternion originalHipsRotation = hips.transform.rotation;

                Vector3 desiredDirection = hips.transform.up *= -1;
                desiredDirection.y = 0;
                desiredDirection.Normalize();

                Quaternion fromToRotation = Quaternion.FromToRotation(transform.forward, desiredDirection);
                transform.rotation *= fromToRotation;

                hips.transform.position = originalHipsPosition;
                hips.transform.rotation = originalHipsRotation;

                // Align hips position                
                originalHipsPosition = hips.transform.position;
                animator.gameObject.transform.position = launchRigidbody.position;

                // Vector3 positionOffset = standupTransforms[0].Position;
                // positionOffset.y = 0;
                // positionOffset = transform.rotation * positionOffset;
                // transform.position -= positionOffset;

                if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo)) {
                    transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
                }

                hips.transform.position = originalHipsPosition;

                PopulateBoneTransforms(ragdollTransforms);

                ragdollState = STATE.RESETTING;
            }
        }
        else if(ragdollState == STATE.RESETTING) {
            elaspedResetBonesTime += Time.deltaTime;
            float elaspedPercentage = elaspedResetBonesTime / timeToResetTheBones;

            for(int i = 0; i < bones.Length; i++) {
                bones[i].localPosition = Vector3.Lerp(ragdollTransforms[i].Position, standupTransforms[i].Position, elaspedPercentage);
                bones[i].localRotation = Quaternion.Lerp(ragdollTransforms[i].Rotation, standupTransforms[i].Rotation, elaspedPercentage);
            }

            if(elaspedPercentage >= 1) {
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
        animator.enabled = false;
        clownWanderer.enabled = false;
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
        }
        sleepTime = totalSleepTime * Random.Range(.8f, 1.2f);
        ragdollState = STATE.RAGDOLL;
    }

    public void DisableRigidBodies() {
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
            rb.isKinematic = true;
        }
        animator.enabled = true;

        ragdollState = STATE.UP;
    }

    public void Launch(GameObject car, Vector3 direction) {
        if(!pickedUp) {
            randomSoundController.Play();
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
        launchRigidbody.AddForce(launchDirection * launchForce);
        yield return new WaitForSeconds(.5f);
        pickedUp = false;
        yield return null;
    }

    public void Despawn() {
        destroyed = true;
        Destroy(gameObject);
    }

    private void PopulateBoneTransforms(BoneTransform[] boneTransforms) {
        for(int i = 0; i < bones.Length; i++) {
            boneTransforms[i].Position = bones[i].localPosition;
            boneTransforms[i].Rotation = bones[i].localRotation;
        }
    }

    private void PopuatieAnimationStartBoneTransforms(string clipName, BoneTransform[] boneTransforms) {

        Vector3 positionBeforeSampling = transform.position;
        Quaternion rotationBeforeSampling = transform.rotation;

        foreach(AnimationClip clip in animator.runtimeAnimatorController.animationClips) {
            if(clip.name == clipName) {
                clip.SampleAnimation(gameObject, 0);
                PopulateBoneTransforms(standupTransforms);
                break;
            }
        }

        transform.position = positionBeforeSampling;
        transform.rotation = rotationBeforeSampling;
    }
}
