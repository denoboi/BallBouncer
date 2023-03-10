using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.IncrimantalIdleSystem;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallCreator : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private Transform _ballSpawnPoint2;
    public Vector3 _ballDirection;
    
    

    private void OnEnable()
    {
        HCB.Core.EventManager.OnBallDie.AddListener(() => CreateBallsViaButton());
      
        _ballDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(0, 0));
        
        HCB.Core.EventManager.OnCreateBallLevelUp.AddListener(CreateBall);
    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnBallDie.RemoveListener(() => CreateBallsViaButton());
        HCB.Core.EventManager.OnCreateBallLevelUp.RemoveListener(CreateBall);

    }
    
    public bool CreateBallsViaButton()
    {
        
        
         Ball ball = Instantiate(_ballPrefab, _ballSpawnPoint.position, Random.rotation).GetComponent<Ball>();
         ball.ScaleTween(Vector3.zero, Vector3.one, .3f); //bug cikarabilir.
         //ball.transform.DORotate(Vector3.up, 0.5f, RotateMode.FastBeyond360);
         //ball.MoveTween(_ballSpawnPoint2.position, _ballSpawnPoint.position,.5f);
             ball.Initialize();
          BallManager.Instance.NumWeakBalls++;
          return true;
          

    }

    
    
    public void CreateBall(int level)
    {
        GameObject ball = Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
        ball.GetComponent<Ball>().Level = 2;
    }
    
   
    
    
   
    


   
    
   
    
}
