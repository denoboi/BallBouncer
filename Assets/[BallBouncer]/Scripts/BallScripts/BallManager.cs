using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Utilities;
using UnityEngine;

public class BallManager : Singleton<BallManager>
{
    [SerializeField] private int _maxLevel;
    private BallCreator _ballCreator;

    public GameObject WeakBall;
    public GameObject MediumBall;
    public GameObject StrongBall;
    
    [SerializeField] private Transform _mediumBallSpawnPoint;

    public int NumWeakBalls = 1;
    public int NumMediumBalls = 0;
    
    public bool IsMediumBallButtonPressed = false;
    public BallCreator BallCreator => _ballCreator == null ? _ballCreator = GetComponent<BallCreator>() : _ballCreator;
    
    public List<Ball> _weakBalls = new List<Ball>();
    [SerializeField] private ParticleSystem _mediumBallParticleSystem;

    private void OnEnable()
    {
        HCB.Core.EventManager.OnMediumBallDie.AddListener(()=>CreateLevelTwoBalls());
    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnMediumBallDie.RemoveListener(()=>CreateLevelTwoBalls());

    }

    private void Update()
    {
        CheckWeakMerge(); //bug cikarabilir.
    }

    public void MergeBalls()
    {
        if (NumWeakBalls >= 3)
        {
            CreateLevelTwoBalls();
            GameObject[] weakBalls = GameObject.FindGameObjectsWithTag("WeakBall");

            for (int i = 0; i < 3; i++)
            {
                Destroy(weakBalls[i]);
            }
            _mediumBallParticleSystem.Play();
            NumWeakBalls -= 3;
           // CreateLevelTwoBallsButton();
           
           IsMediumBallButtonPressed = false;
        }
    }
    
    private bool CreateLevelTwoBalls()
    {
        Ball ball = Instantiate(MediumBall, _mediumBallSpawnPoint.position, Quaternion.identity).GetComponent<Ball>();
        ball.InitializeMediumBall();
        ball.ScaleTween(Vector3.zero, Vector3.one, 0.5f); //bug makinesi
        BallManager.Instance.NumMediumBalls++;
        return true;
    }

    private void CheckWeakMerge()
    {
        if(NumWeakBalls>= 3)
        {
            HCB.Core.EventManager.OnMergeCheck.Invoke(true);
        }
        
        else if(NumWeakBalls < 3)
        {
            HCB.Core.EventManager.OnMergeCheck.Invoke(false);
        }
        
    }
    
    
    
    
    // public void AddBall(Ball ball)
    // {
    //     if (!_weakBalls.Contains(ball))
    //     {
    //         _weakBalls.Add(ball);
    //         //HCB.Core.EventManager.OnMergeCheck.Invoke(CanMerge());
    //     }
    //     
    // }
    //
    // public void RemoveBall(Ball ball)
    // {
    //         if(_weakBalls.Contains(ball))
    //         {
    //             _weakBalls.Remove(ball);
    //             //HCB.Core.EventManager.OnMergeCheck.Invoke(CanMerge());
    //         }
    // }
    
    
    
    // public bool CanMerge()
    // {
    //     if (_balls.Count >= 3)
    //     {
    //         for (int i = 1; i <= _maxLevel; i++)
    //         {
    //            int currentCount = 0;
    //            
    //            for (int j = 0; j < _balls.Count; j++)
    //            {
    //                if (_balls[j].Level == i)
    //                {
    //                    currentCount++;
    //                    if (currentCount >= 3)
    //                        return true;
    //                }
    //            }
    //         }
    //         
    //         
    //     }
    //     return false;  
    //    
    // }


    // public void Merge()
    // {
    //     for (int i = 1; i <= _maxLevel; i++)
    //     {
    //         int currentCount = 0;
    //         
    //         List<Ball> mergeableBalls = new List<Ball>();
    //         
    //         for (int j = 0; j < _balls.Count; j++)
    //         {
    //             if (_balls[j].Level == i)
    //             {
    //                 currentCount++;
    //                 mergeableBalls.Add(_balls[j]);
    //                 if (currentCount >= 3)
    //                 {
    //                     for (int k = 0; k < mergeableBalls.Count; k++)
    //                     {
    //                         Destroy(mergeableBalls[k].gameObject);
    //                     }
    //                     
    //                     HCB.Core.EventManager.OnCreateBallLevelUp.Invoke(i);
    //                 }
    //                     
    //                     
    //             }
    //         }
    //         
            
        // }
    // }
}
