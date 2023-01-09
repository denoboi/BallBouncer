using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class ResetEconomy : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelFinish.AddListener(ResetPlayerPrefs);
        LevelManager.Instance.OnLevelStart.AddListener(ResetPlayerPrefs);
    }

    private void OnDisable()
    {
        if(Managers.Instance == null)
            return;
        
        LevelManager.Instance.OnLevelFinish.RemoveListener(ResetPlayerPrefs);
        LevelManager.Instance.OnLevelStart.RemoveListener(ResetPlayerPrefs);
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("CreateShape");
        PlayerPrefs.DeleteKey("MergeBalls");
        PlayerPrefs.DeleteKey("SpawnBall");

        GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] /= 3;

    }
    
    
}
