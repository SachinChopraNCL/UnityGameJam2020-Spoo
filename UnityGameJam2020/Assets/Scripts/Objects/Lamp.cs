using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class Lamp : MonoBehaviour
{
   public Light2D lampLight;
   public Light2D globalLight;
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
   public GameObject timer;
   public GameObject fireImage;
   public Sprite fireImageSliderLarge;
   public Sprite fireImageSliderSmall;
    public LampAudioController audioController;

    bool playLowFire = true;
    bool playHighFire = false;
   
   float val = 0.25f;
   public List<PhantomPlatform> phantomPlatforms = new List<PhantomPlatform>();
   
   void Awake()
   {
       globalLight = GameObject.Find("Global Light 2D").GetComponent<Light2D>();
       phantomPlatforms = FindObjectsOfType<PhantomPlatform>().ToList();
       timer.GetComponent<Slider>().maxValue = outerRadius;
       fireImage.GetComponent<Image>().sprite = fireImageSliderSmall;
       timer.SetActive(false); 
   }
   public void Update()
   {   
       if(playLowFire)
       {
           audioController.PlayFireLow();
       }
       if(playHighFire)
       {
           audioController.PlayFireHigh();
       }
       if(isCharging && !discharging)
       {       
         timer.SetActive(true);
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
            if(discharging)
            {
                playHighFire = true;
                float ratio = ((float) outerRadius / rateMin);
                if(globalLight.intensity > 2.5)
                {
                    globalLight.intensity -= ratio * 10f;
                }
                else if(lampLight.pointLightOuterRadius < 6f)
                {
                    globalLight.intensity -= ratio * 0.2f;
                    if(globalLight.intensity <= 0.1)
                    {
                        globalLight.intensity = 0.1f;
                    }
                }
                else
                {
                    globalLight.intensity -= ratio * 0.15f;
                    if(globalLight.intensity <= 1)
                    {
                        globalLight.intensity = 1f;
                    }
                }
            
            } 
            else
            {
                playHighFire = false;
                playLowFire = true;
            }
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
       timer.GetComponent<Slider>().value = lampLight.pointLightOuterRadius;
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
           timer.GetComponent<Slider>().value = 0;
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
           timer.SetActive(false);
           globalLight.color = new Vector4(1f, 1f, 1f, 1f);
       }

       if(lampLight.pointLightOuterRadius >= outerRadius && canSet)
       {
           globalLight.intensity = 8f;
           globalLight.color = new Vector4(0.75f, 0.98f, 0.75f, 1f);
           fireAnimator.Play("bigFire");
           levelDirector.isPlatforming = true;
           levelDirector.instantiate = true;
           canSet = false;
           discharging = true;
           playLowFire = false;
           audioController.PlayFireTransition();
        }
   }


}
