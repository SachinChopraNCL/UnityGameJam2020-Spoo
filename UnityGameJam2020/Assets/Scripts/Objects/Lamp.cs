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
   public float rateOfChange;
   public bool isCharging = true;
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
            lampLight.pointLightOuterRadius += (float) outerRadius / maxEnergy;
            lampLight.pointLightInnerRadius += (float) innerRadius/ maxEnergy;
         }
       }
       else 
       {    
            lampLight.pointLightOuterRadius -= (float) outerRadius / (maxEnergy*rateOfChange);
            lampLight.pointLightInnerRadius -= (float) innerRadius / (maxEnergy*rateOfChange);
            if(lampLight.pointLightOuterRadius < 0.1)
            {
                lampLight.pointLightOuterRadius = 0;
                lampLight.pointLightInnerRadius = 0;
            }
       }

       if(lampLight.pointLightOuterRadius > 1)
       {
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.SwitchLayerMask("Ground");
           }
       }
       
       if(lampLight.pointLightOuterRadius <= 1)
       {
           foreach(PhantomPlatform phantomPlatform in phantomPlatforms)
           {
               phantomPlatform.SwitchLayerMask("Ignore");
           }
       }
       isCharging = false;
   }


}
