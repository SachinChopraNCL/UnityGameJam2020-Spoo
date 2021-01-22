using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PhantomPlatform : MonoBehaviour
{
   public string layerMask;
   private bool isActive = false;

   public ShadowCaster2D shadowCaster; 
   void Awake()
   {
      gameObject.layer = LayerMask.NameToLayer(layerMask);
      shadowCaster = GetComponent<ShadowCaster2D>();
   }
   public void SwitchLayerMask (string newLayerMask)
   {
      gameObject.layer = LayerMask.NameToLayer(newLayerMask);
   }

   public void Update()
   {
      shadowCaster.castsShadows = isActive;
   }
}
