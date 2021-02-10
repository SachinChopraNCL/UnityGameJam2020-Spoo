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
    }

    // Update is called once per frame
    public void PlayPlatforming()
    {
      if(levelAudio.clip != platforming)
      {
       levelAudio.clip = platforming;
       levelAudio.volume = 0.15f;
       levelAudio.Play();
      }
    }

    public void FadeOut(float delay)
    {
      fade = StartCoroutine(FadeOut(levelAudio, delay, 0));
    }

    public void PlayAmbient()
    {
      if(levelAudio.clip != ambient)
      {
       levelAudio.clip = ambient;
       levelAudio.Play();
       StopAllCoroutines();
       StartCoroutine(FadeIn(levelAudio, 0.15f));
      }
    }
    

    public static IEnumerator FadeOut(AudioSource audioSource, float duration, float targetVolume)
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
   
   public static IEnumerator FadeIn(AudioSource audioSource, float targetVolume)
   {
      while(audioSource.volume <= targetVolume){
        audioSource.volume += 0.0001f;
        yield return null;
      }
      yield break;
   }
}
