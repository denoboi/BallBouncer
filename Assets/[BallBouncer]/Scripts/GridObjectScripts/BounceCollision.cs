using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Ball>(out Ball ball))
        {
            ball.Bounce();
        }
        else
        {
            Debug.Log("No ball component found");
        }
    }
}
