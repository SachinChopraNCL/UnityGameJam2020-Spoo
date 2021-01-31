using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class Lamp : MonoBehaviour
{
       public Light2D lampLight;
   public float innerRadius;
   public float outerRadius;
   public float rateMin;
   public float rateMax;
   public bool canSet = true;
   public LevelDirector levelDirector;
   public bool isCharging = false;

   public bool discharging = false;
   public Animator fireAnimator;
   public GameObject fireRenderer;
   float val = 0.25f;
   public List<PhantomPlatform> phantomPlatforms = new List<PhantomPlatform>();
   
   void Awake()
   {
       phantomPlatforms = FindObjectsOfType<PhantomPlatform>().ToList();
   }
   public void Update()
   {   
       if(isCharging && !discharging)
       {
         if(lampLight.pointLightOuterRadius < outerRadius)
         {
            lampLight.pointLightOuterRadius += (float) outerRadius /  rateMax;
            lampLight.pointLightInnerRadius += (float) innerRadius/  rateMax;

            float xScale = fireRenderer.transform.localScale.x + ((float)1f / rateMax);
            float yScale = fireRenderer.transform.localScale.y + ((float)1f / rateMax);
            float yPos = fireRenderer.transform.localPosition.y + ((float)0.2f / rateMax);
            fireRenderer.transform.localScale = new Vector3(xScale, yScale, 1);
            fireRenderer.transform.localPosition = new Vector3(0, yPos, 0);
         }
       }
       else
       {    
            lampLight.pointLightOuterRadius -= (float) outerRadius / rateMin;
            lampLight.pointLightInnerRadius -= (float) innerRadius / rateMin;
            float xScale = fireRenderer.transform.localScale.x - ((float)1.5f / rateMin);
            float yScale = fireRenderer.transform.localScale.y - ((float)1.5f / rateMin);
            float yPos = fireRenderer.transform.localPosition.y - ((float)0.3f / rateMin);
            fireRenderer.transform.localScale = new Vector3(xScale, yScale, 1);
            fireRenderer.transform.localPosition = new Vector3(0, yPos, 0);
            if(lampLight.pointLightOuterRadius < 1)
            {
                lampLight.pointLightOuterRadius = 1f;
                lampLight.pointLightInnerRadius = 0.2f;
            }
       }
       if(lampLight.pointLightOuterRadius > outerRadius - 0.2f)
       {
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.SwitchLayerMask("Ground");
               phantomPlatform.isActive = true;
           }
       }

       if(lampLight.pointLightOuterRadius > outerRadius - 2f)
       {
           val += 0.005f;
           if(val > 1)
           {
               val = 1;
           }
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.spriteRenderer.color = new Vector4(1f,1f,1f,val);
           }
       }

       if(lampLight.pointLightOuterRadius <= 2)
       {
           
           val -= 0.01f;
           if(val < 0.25f)
           {
               val = 0.25f;
           }
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.spriteRenderer.color = new Vector4(1f,1f,1f,val);
           }
        }


       if(lampLight.pointLightOuterRadius <= 1)
       {
           fireAnimator.transform.localScale = new Vector3(1.45f, 1.45f, 0);
           fireAnimator.transform.localPosition = new Vector3(0,0.16f,0);
           fireAnimator.Play("smallFire");
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.SwitchLayerMask("Ignore");
               phantomPlatform.isActive = false;
           }
           canSet = true;
           discharging = false;
       }

       if(lampLight.pointLightOuterRadius >= outerRadius && canSet)
       {
           fireAnimator.Play("bigFire");
           levelDirector.isPlatforming = true;
           levelDirector.instantiate = true;
           canSet = false;
           discharging = true;
       }
   }


}
