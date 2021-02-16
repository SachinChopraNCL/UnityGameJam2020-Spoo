using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private float battery = 3.00f; 
    Camera mainCam;
    Transform currentTransform;
    Light2D currentLightSource; 
    SpriteRenderer spriteRenderer;
    [SerializeField]
    GameObject torchLight;
    public bool isTorchOn = true;  
    public bool stunned = false;
    public bool rotate = false;
    public bool isRight = true;

    public LayerMask torchLayer;
    public LayerMask playerLayer;
    public LayerMask defaultLayer; 
    public Animator animator;

    public float rayCastDistance;
    void Awake()
    {
        mainCam = Camera.main;
        currentTransform = GetComponent<Transform>();
        currentLightSource = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        RotateTorch();
    }

    void RotateTorch()
    {
        if(rotate && !stunned)
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lookAt = mouseScreenPosition;
            float AngleRad = Mathf.Atan2(lookAt.y - this.transform.parent.position.y, lookAt.x - this.transform.parent.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
            
            if(!isRight)
            {
                torchLight.transform.localRotation = Quaternion.Euler(0,0, 68);
            }
            else
            {
                torchLight.transform.localRotation = Quaternion.Euler(0,0,-112);
            }
        }
    }

    public float Battery
    {
        get {return battery;}
        set {battery = value;}
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == 13 || col.gameObject.layer == 14)
        {
            Vector2 dir =  col.gameObject.transform.position -  torchLight.transform.position;
            Vector2 normDir = dir.normalized;
            int finalMask =  ~(playerLayer | torchLayer | defaultLayer);
            RaycastHit2D rayCol = Physics2D.Raycast(torchLight.transform.position, normDir, 25, finalMask);
            Debug.DrawRay(torchLight.transform.position, normDir);
            Debug.Log(rayCol.collider.gameObject);
            if( rayCol.collider != null && rayCol.collider.gameObject.layer == 13)
            {
                GameObject ghostObject = col.gameObject;
                Enemy enemy = ghostObject.GetComponent<Enemy>();
                ghostObject.GetComponent<AIPath>().maxSpeed *= 0.6f;
                enemy.health -= 1;
            }
            else if(rayCol.collider != null && rayCol.collider.gameObject.layer == 14)
            {
                Lamp currentLamp = col.gameObject.GetComponent<Lamp>();
                currentLamp.isCharging = true; 
            }
        }

    }

    void OnTriggerExit2D(Collider2D col){
            if(col.gameObject.layer == 13)
            {
                GameObject ghostObject = col.gameObject;
                ghostObject.GetComponent<AIPath>().maxSpeed = 4.5f;
            }
            if(col.gameObject.layer == 14)
            {
                Lamp currentLamp = col.gameObject.GetComponent<Lamp>();
                currentLamp.isCharging = false; 
            }
    }

}
