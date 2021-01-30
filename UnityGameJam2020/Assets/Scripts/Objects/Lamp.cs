using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class Lamp : MonoBehaviour
{
   public float maxEnergy;
   public Light2D lampLight;
   public float innerRadius;
   public float outerRadius;
   public float rateMin;
   public float rateMax;
   public bool canSet = true;
   public LevelDirector levelDirector;
   public bool isCharging = false;
   float val = 0.25f;
   public List<PhantomPlatform> phantomPlatforms = new List<PhantomPlatform>();
   
   void Awake()
   {
       phantomPlatforms = FindObjectsOfType<PhantomPlatform>().ToList();
   }
   public void Update()
   {   
       if(isCharging)
       {
         if(lampLight.pointLightOuterRadius < outerRadius)
         {
            lampLight.pointLightOuterRadius += (float) outerRadius / (maxEnergy * rateMax);
            lampLight.pointLightInnerRadius += (float) innerRadius/ (maxEnergy * rateMax);
         }
       }
       else 
       {    
            lampLight.pointLightOuterRadius -= (float) outerRadius / (maxEnergy*rateMin);
            lampLight.pointLightInnerRadius -= (float) innerRadius / (maxEnergy*rateMin);
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
         
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.SwitchLayerMask("Ignore");
           }
           canSet = true;
       }

       if(lampLight.pointLightOuterRadius >= outerRadius && canSet)
       {
           levelDirector.isPlatforming = true;
           levelDirector.instantiate = true;
           canSet = false;
       }
   }


}
