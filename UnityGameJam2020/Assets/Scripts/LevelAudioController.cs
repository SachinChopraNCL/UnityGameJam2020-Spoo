using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip ambient;
    public AudioClip platforming;
    AudioSource levelAudio;

    Coroutine fade;

    void Start()
    {
       GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioSource");
         if (objs.Length > 1)
         {
             Destroy(this.gameObject);
         }
      DontDestroyOnLoad(transform.gameObject);
      levelAudio = GetComponent<AudioSource>();
      levelAudio.volume = 0;
      levelAudio.clip = ambient;
      levelAudio.Play();

      StopAllCoroutines();
      StartCoroutine(StartFade(levelAudio, 8f, 0.15f));
    }

    // Update is called once per frame
    public void PlayPlatforming()
    {
      if(gameObject.GetComponent<AudioSource>().clip == ambient)
      {
       levelAudio = GetComponent<AudioSource>(); 
       levelAudio.clip = platforming;
       levelAudio.volume = 0.15f;
       levelAudio.Play();
      }
    }

    public void FadeOut()
    {
      fade = StartCoroutine(StartFade(levelAudio, 1.5f, 0));
    }

    public void PlayAmbient()
    {
      if(gameObject.GetComponent<AudioSource>().clip == platforming)
      {
       levelAudio = GetComponent<AudioSource>(); 
       levelAudio.clip = ambient;
       levelAudio.Play();
       StopAllCoroutines();
       StartCoroutine(StartFade(levelAudio, 8f, 0.15f));
      }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            Debug.Log(targetVolume);
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
