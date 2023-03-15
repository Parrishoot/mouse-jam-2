using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiController : MonoBehaviour
{
    private ParticleSystem confettiParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        confettiParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(confettiParticleSystem.isStopped) {
            Destroy(gameObject);
        }
    }
}
