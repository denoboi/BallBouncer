using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class SpawnShapeDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        
        HCB.Core.EventManager.OnCreateShapeCheck.Invoke(false);
        
    }

    private void OnTriggerExit(Collider other)
    {
        HCB.Core.EventManager.OnCreateShapeCheck.Invoke(true);
    }
    
   
}
