using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip jumpAudio;
    public AudioClip walkAudio;
    public AudioClip crank1;
    public AudioClip crank2;

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

    public void PlayCrank1()
    {
        gameObject.GetComponent<AudioSource>().clip = crank1;
        gameObject.GetComponent<AudioSource>().Play();
    }

    
    public void PlayCrank2()
    {
        gameObject.GetComponent<AudioSource>().clip = crank2;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void Stop()
    {
        gameObject.GetComponent<AudioSource>().Pause();
    }

}
