using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownHornController : MonoBehaviour
{

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2)) {
            audioSource.Stop();
            audioSource.pitch = Random.Range(.95f, 1.05f);
            audioSource.Play();
        }
    }
}
