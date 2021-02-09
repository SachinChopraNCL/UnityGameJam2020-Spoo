using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip ambient;
    public AudioClip platforming;
    AudioSource levelAudio;

    void Start()
    {
      levelAudio = GetComponent<AudioSource>(); 
      levelAudio.clip = ambient;
      levelAudio.Play();
    }

    // Update is called once per frame
    public void PlayPlatforming()
    {
      if(gameObject.GetComponent<AudioSource>().clip == ambient)
      {
       levelAudio = GetComponent<AudioSource>(); 
       levelAudio.clip = platforming;
       levelAudio.Play();
      }
    }

    public void PlayAmbient()
    {
      if(gameObject.GetComponent<AudioSource>().clip == platforming)
      {
       levelAudio = GetComponent<AudioSource>(); 
       levelAudio.clip = ambient;
       levelAudio.Play();
      }
    }
}
