using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
  public RailHandler railHandler;

  public Sprite [] sprites;
  public SpriteRenderer spriteRenderer;
  
  void Awake(){
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = sprites[1];
  }
  void OnTriggerStay2D(Collider2D collider)
  {
      if(collider.gameObject.tag == "Player")
      {
          if(Input.GetMouseButton(0))
          {
              railHandler.isMoving = true; 
              railHandler.direction = Direction.toLower;
              spriteRenderer.sprite = sprites[0];
          }
          else if(Input.GetMouseButton(1))
          {
              railHandler.isMoving = true; 
              railHandler.direction = Direction.toUpper;
              spriteRenderer.sprite = sprites[2];
          }
          else
          {
              railHandler.isMoving = false;
              spriteRenderer.sprite = sprites[1];
          }
      }
  }

  void OnTriggerExit2D(Collider2D collider)
  {
      if(collider.gameObject.tag == "Player")
      {
        railHandler.isMoving = false;
        spriteRenderer.sprite = sprites[1];
      }
  }
}
