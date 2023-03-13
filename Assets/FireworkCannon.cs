using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkCannon : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float cooldown = 1f;

    private float remainingCooldown = 0f;

    public virtual void Update() {
        if(remainingCooldown > 0) {
            remainingCooldown -= Time.deltaTime;
        }
    }

    protected void Shoot() {
        if(remainingCooldown <= 0) {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<ProjectileController>().Init(transform.root.gameObject, transform.forward);

            remainingCooldown = cooldown;
        }
    }
}
