using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Ball>(out Ball ball))
        {
            HCB.Core.EventManager.OnBallDie.Invoke();
            Debug.Log("Ball died" + ball);
        }
    }
}
