using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    AudioSource doorAudio;
    public AudioClip doorSound;
    void Start()
    {
      GameObject[] objs = GameObject.FindGameObjectsWithTag("DoorSound");
      if (objs.Length > 1)
      {
        Destroy(this.gameObject);
      }
      DontDestroyOnLoad(transform.gameObject);
      doorAudio = GetComponent<AudioSource>();
    }
    public void PlayDoor()
    {
        doorAudio.clip = doorSound;
        doorAudio.Play();
    }
}
