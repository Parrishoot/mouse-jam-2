using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip audioClip;

    public void Whistle() {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
