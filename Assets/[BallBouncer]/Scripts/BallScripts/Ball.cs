using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody Rb => _rb == null ? _rb = GetComponent<Rigidbody>() : _rb;
    public int Level;
    
    
    public void Initialize()
    {
        Vector3 force = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Rb.AddForce(force, ForceMode.Impulse);
    }
   
}
