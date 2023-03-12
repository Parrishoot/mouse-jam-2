using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMovementController : CarMovementController
{
    private float movement;

    private float targetRotation;

    protected override void Start() {
        targetRotation = transform.eulerAngles.y;
        movement = 0;
        base.Start();
    }

     // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            movement = 1;
        }
        else {
            movement = 0;
        }

        if(Input.GetMouseButtonDown(1)) {
            Shoot();
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
}
