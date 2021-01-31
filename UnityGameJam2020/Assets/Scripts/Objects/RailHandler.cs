using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    toUpper,
    toLower
}

public enum Orientation
{
  up,
  down,
  left,
  right
}

public enum Alignment
{
  horizontal,
  vertical
}

public class RailHandler : MonoBehaviour
{
    public GameObject mirror; 
    public GameObject currentMirror;
    public Transform upperbound;
    public Transform lowerbound;
    public bool isMoving = false;
    public Direction direction;

    public Orientation orientation;
    public Alignment alignment;

    void Awake()
    {
        if(alignment == Alignment.vertical)
        {
          this.transform.localRotation = Quaternion.Euler(0,0,90);
        }
        currentMirror = Instantiate(mirror, lowerbound.position, Quaternion.identity);
        currentMirror.transform.rotation = Quaternion.FromToRotation(Vector2.right, Vector2.up);
        if(orientation == Orientation.up){
          currentMirror.transform.rotation = Quaternion.Euler(0,0,90);
        }
        if(orientation == Orientation.down){
            currentMirror.transform.rotation = Quaternion.Euler(0,0,-90);
        }
        if(orientation == Orientation.left){
          currentMirror.transform.rotation = Quaternion.Euler(0,0,-180);
        }
        if(orientation == Orientation.right){
          currentMirror.transform.rotation = Quaternion.Euler(0,0,-180);
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