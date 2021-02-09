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
    public LayerMask backLayer;
    public LayerMask groundLayer;
    public float rayCastDistance;
    GameObject ghostObject = null;
    public Torch torch; 
    public bool flip = false;
    public Transform lightPoint;

    public MirrorLevelHandler mirrorLevelHandler = null;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mirrorLevelHandler = (MirrorLevelHandler)FindObjectOfType(typeof(MirrorLevelHandler));
    }

    void Update()
    {
        if (ghostObject != null)
        {
            ghostObject.GetComponent<AIPath>().maxSpeed = 3.00f;
        }

        Raycaster();
        
    }

    void Raycaster()
    {
        Vector2 lightPos = lightPoint.position;
        Vector2 forward = new Vector2(1,0);
        float angle = 90;

        Vector2 dir =  lightPoint.rotation * Quaternion.Euler(0,0,angle) * forward;
        Vector2 normDir = dir.normalized;

        HitMirror(lightPos, dir, normDir);        
    }

    void HitMirror(Vector2 lightPos, Vector2 dir, Vector2 normDir)
    {   
        LayerMask mask = mirrorLayer | backLayer | groundLayer;
        RaycastHit2D mirrorCollision = Physics2D.Raycast(lightPos, normDir, rayCastDistance, mask);
        Debug.Log(mirrorCollision.collider.gameObject);
        if (mirrorCollision.collider != null) 
        {
            if(mirrorCollision.collider.gameObject.layer == 11) 
            {
                Collider2D currentMirrorCollider = mirrorCollision.collider;
                Mirror currentMirror = currentMirrorCollider.gameObject.GetComponent<Mirror>();
                Vector3 midPoint = currentMirrorCollider.gameObject.transform.GetChild(0).gameObject.transform.position;
                float ratio = (float)dir.y / (float)dir.x;
                float distance = Mathf.Abs(Vector2.Distance(midPoint, mirrorCollision.point));
                if(distance < 1f)
                {
                  currentMirror.CheckForInstantiation(Mathf.Atan(ratio), mirrorCollision, lightPos);
                }
            } 
            
        }
        else
        {
           if(mirrorLevelHandler != null)
           {
              mirrorLevelHandler.DisableAll();
           }
        }
    }
}
