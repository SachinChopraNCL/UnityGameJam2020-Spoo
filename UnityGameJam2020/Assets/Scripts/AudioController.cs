using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip jumpAudio;
    public AudioClip walkAudio;
    bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
       isPlayer = gameObject.CompareTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            PlayerAudio();
        }
    }

    void PlayerAudio()
    {
        if(Input.GetKeyDown("a")||Input.GetKeyDown("d")){
            gameObject.GetComponent<AudioSource>().clip = walkAudio;
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyUp("a")||Input.GetKeyUp("d")||Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
        if (Input.GetKeyDown("space"))
        {
            gameObject.GetComponent<AudioSource>().clip = jumpAudio;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }    

}
