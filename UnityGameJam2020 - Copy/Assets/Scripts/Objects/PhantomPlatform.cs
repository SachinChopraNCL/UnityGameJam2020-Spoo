﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PhantomPlatform : MonoBehaviour
{
   public string layerMask;
   public bool isActive = true;

   public ShadowCaster2D shadowCaster; 

   public SpriteRenderer spriteRenderer;
   void Awake()
   {
      gameObject.layer = LayerMask.NameToLayer(layerMask);
      shadowCaster = GetComponent<ShadowCaster2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.color = new Vector4(1f,1f,1f,0.25f);
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
