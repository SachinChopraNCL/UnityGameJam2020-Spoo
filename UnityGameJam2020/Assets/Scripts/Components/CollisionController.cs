﻿using Pathfinding;
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

    public bool flip = false;
    public Transform lightPoint;
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

        Vector2 lightPos = lightPoint.position;
        Vector2 forward = new Vector2(1,0);
        float angle = flip ? 90 : 90;
        Vector2 dir =  lightPoint.rotation * Quaternion.Euler(0,0,angle) * forward;
        Vector2 normDir = dir.normalized;
        HitMirror(lightPos, dir, normDir);        
    }

    void HitMirror(Vector2 lightPos, Vector2 dir, Vector2 normDir)
    {   
        RaycastHit2D mirrorCollision = Physics2D.Raycast(lightPos, normDir, rayCastDistance, mirrorLayer);
        Debug.DrawRay(lightPos, dir);
        if (mirrorCollision.collider != null) 
        {
            Collider2D currentMirrorCollider = mirrorCollision.collider;
            Mirror currentMirror = currentMirrorCollider.gameObject.GetComponent<Mirror>();
            float ratio = (float)dir.y / (float)dir.x;
            currentMirror.CheckForInstantiation(Mathf.Atan(ratio), mirrorCollision, lightPos);

        }
    }
}
