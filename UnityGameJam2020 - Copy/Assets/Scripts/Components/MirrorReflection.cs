using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == 14)
        {
            Lamp currentLamp = col.gameObject.GetComponent<Lamp>();
            currentLamp.isCharging = true; 
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == 14)
        {
            Lamp currentLamp = col.gameObject.GetComponent<Lamp>();
            currentLamp.isCharging = false; 
        }
    }
}
