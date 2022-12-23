using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;

    private void OnEnable()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    private void CreateBalls()
    {
       GameObject ball = Instantiate(_ballPrefab, _ballSpawnPoint);
    }
}
