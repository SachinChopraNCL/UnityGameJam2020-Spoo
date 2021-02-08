using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject currentReflection; 
    public Transform instantiationPoint;
    public float rayCastDistance;
    public LayerMask mirrorLayer;
    public LayerMask backLayer;
    public LayerMask lampLayer;
    public LayerMask groundLayer;
    bool rotateReflection = false;
    //Radians!
    float lastAngle = -1000;
    Vector3 collisionPoint;
    RaycastHit2D mirrorRayCast;
    Collider2D mirrorCollider;
    Vector2 origin;
    Vector3 newDirection;

    bool isOn = false;
    
    public void CheckForInstantiation(float angle, RaycastHit2D _mirrorRayCast, Vector2 _origin)
    {
        rotateReflection = (lastAngle != angle) ? true : false;
        lastAngle = angle; 
        mirrorRayCast = _mirrorRayCast;
        mirrorCollider = mirrorRayCast.collider;
        collisionPoint = mirrorRayCast.point;
        origin = _origin;
        currentReflection.SetActive(true);
        BounceLight();
        if(rotateReflection)
        {
            RotateReflection();
        }
        
        isOn = true;
    }

    public void SwitchOff()
    {
        currentReflection.SetActive(false);
    }


    void Awake()
    {
        currentReflection.SetActive(true);
    }
    
    void Update()
    {
        if(isOn)
        {
            BounceLight();
            RotateReflection();
        }
        
        if(!isOn)
        {
          SwitchOff();
        }
        isOn = false;
    
    }

    void RotateReflection()
    {
        currentReflection.transform.position = collisionPoint;
        
        newDirection = Vector3.zero;
        Vector3 objDirection = mirrorRayCast.point - origin;
        
        newDirection = Vector3.Reflect(objDirection, mirrorRayCast.normal);
        currentReflection.transform.rotation = Quaternion.FromToRotation(Vector2.right, newDirection);
    }

    void BounceLight()
    {
        Vector2 mirrorPosition = collisionPoint;
        Vector2 direction = newDirection.normalized;
        LayerMask mask = mirrorLayer | backLayer | groundLayer | lampLayer;
        RaycastHit2D mirrorCollision = Physics2D.Raycast(mirrorPosition, direction, rayCastDistance * 1.2f, mask);
        Debug.DrawRay(mirrorPosition, direction);
        if(mirrorCollision.collider != null)
        {
          if(mirrorCollision.collider.gameObject.layer == 11)
          {
            Collider2D currentMirrorCollider = mirrorCollision.collider;
            Mirror currentMirror = currentMirrorCollider.gameObject.GetComponent<Mirror>();
            Vector3 midPoint = currentMirrorCollider.gameObject.transform.GetChild(0).gameObject.transform.position;
            float ratio = (float)newDirection.y / (float)newDirection.x;
            
            float distance = Mathf.Abs(Vector2.Distance(midPoint, mirrorCollision.point));
            if(distance < 1f)
            {
              currentMirror.CheckForInstantiation(Mathf.Atan(ratio), mirrorCollision, mirrorPosition);
            }
          }

          if(mirrorCollision.collider.gameObject.layer == 14)
          {
             currentReflection.GetComponent<PolygonCollider2D>().enabled = true;
          }
          else
          {
             currentReflection.GetComponent<PolygonCollider2D>().enabled = false;
          }

        }
    }

}
