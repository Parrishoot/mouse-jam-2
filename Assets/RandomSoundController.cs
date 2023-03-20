using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundController : MonoBehaviour
{
    public List<AudioClip> clips;

    public bool stopOnNewPlay;

    public void Play() {
        AudioSource audiosource = GetComponent<AudioSource>();

        if(stopOnNewPlay) {
            audiosource.Stop();
        }
        if(!audiosource.isPlaying) {
            audiosource.clip = clips[Random.Range(0, clips.Count)];
            audiosource.Play();
        }
    }
}
