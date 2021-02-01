using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip jumpAudio;
    public AudioClip walkAudio;

    public void PlayJump()
    {
        gameObject.GetComponent<AudioSource>().clip = jumpAudio;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void PlayWalk()
    {
        if( !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().clip = walkAudio;
            gameObject.GetComponent<AudioSource>().Play();
        }
   
    }

    public void Stop()
    {
        gameObject.GetComponent<AudioSource>().Pause();
    }

}
