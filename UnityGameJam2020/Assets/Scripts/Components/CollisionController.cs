using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public Rigidbody2D rb; 
    public LayerMask mirrorLayer;
    public LayerMask enemyLayer;
    public float rayCastDistance;
    GameObject ghostObject = null;
    public Torch torch;
    public LayerMask lampLayer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (ghostObject != null)
        {
            ghostObject.GetComponent<AIPath>().maxSpeed = 3.00f;
        }

       if(torch.isTorchOn)
        {
            Raycaster();
        }
    }

    void Raycaster()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        Vector2 direction = mouseScreenPosition - playerPosition;
        Vector2 normDirection = direction.normalized;


        HitMirror(playerPosition, normDirection, direction);
        HitGhost(playerPosition, normDirection);
        
    }

    void HitMirror(Vector2 playerPos, Vector2 normDir, Vector2 dir)
    {
        RaycastHit2D mirrorCollision = Physics2D.Raycast(playerPos, normDir, rayCastDistance, mirrorLayer);
        if (mirrorCollision.collider != null)
        {
            Collider2D currentMirrorCollider = mirrorCollision.collider;
            Mirror currentMirror = currentMirrorCollider.gameObject.GetComponent<Mirror>();
            float ratio = (float)dir.y / (float)dir.x;
            currentMirror.CheckForInstantiation(Mathf.Atan(ratio), mirrorCollision, playerPos);

        }

        RaycastHit2D lampCollision = Physics2D.Raycast(playerPos, normDir, rayCastDistance, lampLayer);
        if(lampCollision.collider != null)
        {
            Lamp currentLamp = lampCollision.collider.gameObject.transform.parent.gameObject.GetComponent<Lamp>();
            currentLamp.isCharging = true;

        }
    }

    void HitGhost(Vector2 playerPos, Vector2 normDir)
    {
        
        RaycastHit2D ghostCollision = Physics2D.Raycast(playerPos, normDir, rayCastDistance, enemyLayer);
        if (ghostCollision.collider != null)
        {
            Collider2D ghostCollider = ghostCollision.collider;
            ghostObject = ghostCollider.gameObject;
            ghostObject.GetComponent<AIPath>().maxSpeed = 0.5f;
            float thisGhostHealth = ghostObject.GetComponent<AiComponent>().GhostHealth;
            thisGhostHealth = thisGhostHealth - 1;
            ghostObject.GetComponent<AiComponent>().GhostHealth = thisGhostHealth;
        }

    }
}
