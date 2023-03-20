using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource musicSource;

    public void PlayMusic() {
        if(!musicSource.isPlaying) {
            musicSource.Play();
        }
    }

    public void StopMusic() {
        if(musicSource.isPlaying) {
            musicSource.Stop();
        }
    }
}
