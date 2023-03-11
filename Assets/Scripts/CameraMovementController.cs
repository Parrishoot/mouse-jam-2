using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    public GameObject followObject;

    public float followSpeed;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = followObject.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = (followObject.transform.position + offset) - transform.position;
        gameObject.transform.Translate(diff * followSpeed * Time.deltaTime);
    }
}
