using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShapeDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
    }
}
