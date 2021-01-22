using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class SceneDirector: MonoBehaviour{

    public void LoadGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
