using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class SceneDirector: MonoBehaviour{

    public void LoadGame(string scene)
    {
        if(scene == "Level_1")
        {
            Destroy(GameObject.FindGameObjectWithTag("AudioSource"));
            Cursor.visible = false;
        }
        SceneManager.LoadScene(scene);
    }

}
