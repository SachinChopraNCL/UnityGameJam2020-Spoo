using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : MonoBehaviour
{
    public bool isPlatforming = false;

    public bool instantiate = false;

    public int numberOfEnemies = 5;
    public Transform[] spawnPoints;

    public GameObject enemyPrefab;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlatforming)
        {
            if(instantiate)
            {
                for(int i = 0; i < numberOfEnemies; i++)
                {
                   Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);    
                }

                instantiate = false;
            }
        }
    }
}
