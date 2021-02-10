using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    DoorSound doorSound;
    void Awake()
    {
        doorSound = GameObject.Find("DoorSound").GetComponent<DoorSound>();

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (collision.gameObject.tag == "Player")
        {
            if(Input.GetMouseButton(0))
            {
                doorSound.PlayDoor();
                switch (sceneName)
                {
                    case "Level_1": SceneManager.LoadScene("Level_2"); break;
                    case "Level_2": SceneManager.LoadScene("Level_3"); break;
                    case "Level_3": SceneManager.LoadScene("Level_4"); break;
                    case "Level_4": SceneManager.LoadScene("Level_5"); break;
                    case "Level_5": SceneManager.LoadScene("Level_6"); break;
                    case "Level_6": SceneManager.LoadScene("Credits");Destroy(GameObject.FindGameObjectWithTag("AudioSource"));break;
                }
            }
        }
    }
}
