using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    public AudioClip appear;
    public AudioClip spawn;
    public AudioClip death;
    public AudioClip attack;

    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAppear()
    {
        audioSource.clip = appear;
        audioSource.Play();
    }

    public void PlaySpawn()
    {
        audioSource.clip = spawn;
        audioSource.Play();
    }

    public void PlayDeath()
    {
        audioSource.clip = death;
        audioSource.Play();
    }   

    public void PlayAttack()
    {
        audioSource.pitch = 1.8f;
        audioSource.clip = attack;
        audioSource.Play();
    }
    

}
