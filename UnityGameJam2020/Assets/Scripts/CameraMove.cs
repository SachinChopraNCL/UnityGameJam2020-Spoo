using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player; 

    // Update is called once per frame
    void Awake()
    {
        player = GameObject.Find("Sandeep").transform;
    }
    void Update()
    {
       transform.position = new Vector3(0, player.position.y, -10);
    }
}
