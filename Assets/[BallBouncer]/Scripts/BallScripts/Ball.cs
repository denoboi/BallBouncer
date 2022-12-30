using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int Level;
    private void OnEnable()
    {
        BallManager.Instance.AddBall(this);
    }

    private void OnDisable()
    {
        BallManager.Instance.RemoveBall(this);
    }

    public void Bounce()
    {
        Debug.Log("Bounce");
        
    }
}
