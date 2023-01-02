using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    public GameObject powerfulBallPrefab;
    public GameObject mediumBallPrefab;
    public GameObject weakBallPrefab;

    private Vector3 _spawnPoint;

    private void Start()
    {
        _spawnPoint = gameObject.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WeakBall"))
        {
            HCB.Core.EventManager.OnBallDie.Invoke();
            BallManager.Instance.NumWeakBalls--;
            //Debug.Log("Ball died" + ball);
            Destroy(other.gameObject);
        }
        
        else if(other.CompareTag("MediumBall"))
        {
            HCB.Core.EventManager.OnMediumBallDie.Invoke();
            BallManager.Instance.NumMediumBalls--;
            Debug.Log("Ball died" + other);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("PowerfulBall"))
        {
            // HCB.Core.EventManager.OnPowerfulBallDie.Invoke();
            // BallManager.Instance.NumPowerfulBalls--;
            Debug.Log("Ball died" + other);
            Destroy(other.gameObject);
        }
       
    }
    
    
        // void OnBecameInvisible()
        // {
        //     if (this.gameObject.CompareTag("PowerfulBall"))
        //     {
        //         // Instantiate a new powerful ball
        //         GameObject newBall = Instantiate(powerfulBallPrefab);
        //         newBall.transform.position = new Vector3(0, 0, -4);
        //     }
        //     else if (this.gameObject.CompareTag("MediumBall"))
        //     {
        //         // Instantiate a new medium ball
        //         GameObject newBall = Instantiate(mediumBallPrefab, _spawnPoint, Quaternion.identity);
        //         //newBall.transform.position = new Vector3(0, 0, -4);
        //     }
        //     else if (this.gameObject.CompareTag("WeakBall"))
        //     {
        //         // Instantiate a new weak ball
        //         GameObject newBall = Instantiate(weakBallPrefab, _spawnPoint, Quaternion.identity);
        //        // newBall.transform.position = new Vector3(0, 0, -4);
        //     }
        // }
    

}
