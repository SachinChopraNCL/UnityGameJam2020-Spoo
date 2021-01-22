using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    toUpper,
    toLower
}
public class RailHandler : MonoBehaviour
{
    public GameObject mirror; 
    public GameObject currentMirror;
    public Transform upperbound;
    public Transform lowerbound;
    public bool isMoving = false;
    public Direction direction;

    void Awake()
    {
        currentMirror = Instantiate(mirror, lowerbound.position, Quaternion.identity);
        if(upperbound.position.y !=lowerbound.position.y)
        {
          currentMirror.transform.rotation = Quaternion.FromToRotation(Vector2.right, Vector2.up);
        }
    }

    void Update()
    {
       if(isMoving)
       {
          if(direction == Direction.toUpper)
          {
            currentMirror.transform.position = Vector2.MoveTowards(currentMirror.transform.position, upperbound.position, Time.deltaTime);
          }
          else if(direction == Direction.toLower)
          {
            currentMirror.transform.position = Vector2.MoveTowards(currentMirror.transform.position, lowerbound.position, Time.deltaTime);
          }
       }
    }
}