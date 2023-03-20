using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDisabler : MonoBehaviour
{
    public void DisableAudio() {

        AudioSource audioSource;

        audioSource = GetComponent<AudioSource>();
        if(audioSource != null) {
            audioSource.enabled = false;
        }

        foreach(AudioSource childAudioSources in GetComponentsInChildren<AudioSource>()) {
            childAudioSources.enabled = false;
        }
    }
}
