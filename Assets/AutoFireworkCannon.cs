using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFireworkCannon : FireworkCannon
{

    public float lookaheadDistance = 10f;
    public float lookaheadBounds = 1f;

    private Collider selfCollider;

    void Start() {
        selfCollider = GetComponentInParent<Collider>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        selfCollider.enabled = false;

        if (Physics.SphereCast(transform.position, lookaheadBounds, transform.forward, out hit, lookaheadDistance, LayerMask.GetMask("Car", "Player"))) {
            Shoot();
        }

        selfCollider.enabled = true;
    }
}
