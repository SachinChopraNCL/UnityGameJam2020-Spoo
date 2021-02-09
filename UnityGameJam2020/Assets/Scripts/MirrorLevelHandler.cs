using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Mirror[] mirrors;
    void Start()
    {
        mirrors = FindObjectsOfType(typeof(Mirror)) as Mirror[];

    }
    public void DisableAll()
    {
      foreach(Mirror mirror in mirrors)
      {
        mirror.SwitchOff();
      }
    }

}
