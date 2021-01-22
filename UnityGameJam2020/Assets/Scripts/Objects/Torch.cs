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
    public bool isTorchOn = true;
    void Awake()
    {
        mainCam = Camera.main;
        currentTransform = GetComponent<Transform>();
        currentLightSource = GetComponent<Light2D>();
    }
    void Update()
    {
        RotateTorch();
        SwitchTorch();
    }

    void RotateTorch()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookAt = mouseScreenPosition;
        float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    void SwitchTorch()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(isTorchOn)
            {
                currentLightSource.intensity = 0;
                isTorchOn = false;
            }
            else
            {
                currentLightSource.intensity = 1;
                isTorchOn = true;
            }
        }
    }

    public float Battery
    {
        get {return battery;}
        set {battery = value;}
    }
}
