using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (collision.gameObject.name == "Player")
        {
            if (sceneName == "Mansion")
            {

                SceneManager.LoadScene("Credits");
            }
        }
    }
}
