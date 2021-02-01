using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (collision.gameObject.layer == 10)
        {
            if(Input.GetMouseButton(0))
            {
                if (sceneName == "TestScene")
                {
                    SceneManager.LoadScene("Credits");
                }
            }
        }
    }
}
