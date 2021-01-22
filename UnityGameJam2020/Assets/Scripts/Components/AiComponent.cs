using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiComponent : MonoBehaviour
{
    public Torch torchClass;
    private float ghostHealth = 200.00f;
    public Vector2 startPos;
    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        torchClass = GameObject.Find("Torch").GetComponent<Torch>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            stealBattery();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ghostHealth == 0)
        {
            gameObject.transform.position = startPos;
            GhostHealth = 200.00f;
        }

    }

    void stealBattery()
    {
        float torchBattery = torchClass.Battery - 1f;
        torchClass.Battery = torchBattery;
    }

    public float GhostHealth
    {
        get { return ghostHealth; }
        set { ghostHealth = value; }
    }

}
