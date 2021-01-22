using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject currentReflection; 
    public Transform instantiationPoint;
    public float rayCastDistance;
    public LayerMask mirrorLayer;
    public LayerMask lampLayer;
    bool rotateReflection = false;
    bool stillLooking = false;
    //Radians!
    float lastAngle = -1000;
    Vector3 collisionPoint;
    RaycastHit2D mirrorRayCast;
    Collider2D mirrorCollider;
    Vector2 origin;
    Vector3 newDirection;
    
    public void CheckForInstantiation(float angle, RaycastHit2D _mirrorRayCast, Vector2 _origin)
    {
        rotateReflection = (lastAngle != angle) ? true : false;
        lastAngle = angle; 
        stillLooking = true;
        mirrorRayCast = _mirrorRayCast;
        mirrorCollider = mirrorRayCast.collider;
        collisionPoint = mirrorRayCast.point;
        origin = _origin;
    }

    void Awake()
    {
        currentReflection.SetActive(true);
    }
    void Update()
    {
        if(rotateReflection)
        {
            RotateReflection();
        }

        if(stillLooking)
        {
            BounceLight();
        }
        else if(!stillLooking)
        {
            currentReflection.SetActive(false);
            lastAngle = -1000;
        }
        
        stillLooking = false;
    }

    void RotateReflection()
    {
        currentReflection.SetActive(true);
        currentReflection.transform.position = collisionPoint;
        
        newDirection = Vector3.zero;
        Vector3 playerDirection = mirrorRayCast.point - origin;
          
        newDirection = Vector3.Reflect(playerDirection, mirrorRayCast.normal);
        currentReflection.transform.rotation = Quaternion.FromToRotation(Vector2.right, newDirection);

    }

    void BounceLight()
    {
        currentReflection.SetActive(true);
        Vector2 mirrorPosition = collisionPoint;
        Vector2 direction = newDirection.normalized;
        Vector2 displacement = mirrorRayCast.normal;
        RaycastHit2D mirrorCollision = Physics2D.Raycast(mirrorPosition + displacement, direction, rayCastDistance, mirrorLayer);
        if(mirrorCollision.collider != null)
        {
          Collider2D currentMirrorCollider = mirrorCollision.collider;
          Mirror currentMirror = currentMirrorCollider.gameObject.GetComponent<Mirror>();
          float ratio = (float)newDirection.y / (float)newDirection.x;
          currentMirror.CheckForInstantiation(Mathf.Atan(ratio), mirrorCollision, mirrorPosition);
        }

        RaycastHit2D lampCollision = Physics2D.Raycast(mirrorPosition, direction, rayCastDistance, lampLayer);
        if(lampCollision.collider != null)
        {
            Lamp currentLamp = lampCollision.collider.gameObject.transform.parent.gameObject.GetComponent<Lamp>();
            currentLamp.isCharging = true;
        }
       
    }
  
}
