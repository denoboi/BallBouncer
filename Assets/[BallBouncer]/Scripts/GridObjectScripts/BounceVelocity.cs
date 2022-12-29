using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BounceVelocity : MonoBehaviour
{
    private Vector3 _lastFrameVelocity;
    private Rigidbody _rb;

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.collider.TryGetComponent(out BounceCollision bounceCollision))
       {
           BounceSpeed(collision.contacts[0].normal);
           Debug.Log("BOMBALA");
       }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _lastFrameVelocity = _rb.velocity;
    }
    

    public void BounceSpeed(Vector3 collisionNormal)
    {
        var speed = _lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);
        Vector3 newDirection = Vector3.Reflect(transform.forward, collisionNormal);
        transform.rotation = Quaternion.LookRotation(newDirection);
        
        _rb.AddForce(newDirection * 20, ForceMode.VelocityChange);
    }
}
