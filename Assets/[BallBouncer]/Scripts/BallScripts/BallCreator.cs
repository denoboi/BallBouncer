using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCreator : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;

    private void OnEnable()
    {
        
        HCB.Core.EventManager.OnBallDie.AddListener(CreateBalls);
    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnBallDie.RemoveListener(CreateBalls);
        
    }

    public void CreateBalls()
    {
        GameObject ball = Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
    }
    
}
