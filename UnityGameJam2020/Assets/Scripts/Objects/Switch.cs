using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public RailHandler railHandler;

  void OnTriggerStay2D(Collider2D collider)
  {
      if(collider.gameObject.tag == "Player")
      {
          if(Input.GetMouseButton(0))
          {
              railHandler.isMoving = true; 
              railHandler.direction = Direction.toLower;
          }
          else if(Input.GetMouseButton(1))
          {
              railHandler.isMoving = true; 
              railHandler.direction = Direction.toUpper;
          }
          else
          {
              railHandler.isMoving = false;
          }
      }
  }
}
