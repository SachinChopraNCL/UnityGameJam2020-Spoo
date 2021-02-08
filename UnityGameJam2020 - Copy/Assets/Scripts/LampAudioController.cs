using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip   fireLow;
    public AudioClip   fireHigh;
    public AudioClip   fireTransition;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFireLow()
    {
        if( audioSource.clip != fireLow)
        {
            audioSource.volume = 1;
            audioSource.clip = fireLow;
            audioSource.Play();
        }
    }
    public void PlayFireHigh()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.volume = 0.3f;
            audioSource.clip = fireHigh;
            audioSource.Play();
        }
    }

    public void PlayFireTransition()
    {
        audioSource.volume = 0.6f;
        audioSource.clip = fireTransition;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

}
