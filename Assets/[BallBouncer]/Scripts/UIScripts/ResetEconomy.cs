using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetEconomy : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelFinish.AddListener(ResetPlayerPrefs);
        //SceneController.Instance.OnSceneLoaded.AddListener(ResetPlayerPrefs);
        
    }

    private void OnDisable()
    {
        if(Managers.Instance == null)
            return;
        
        LevelManager.Instance.OnLevelFinish.RemoveListener(ResetPlayerPrefs);
        
        //SceneController.Instance.OnSceneLoaded.RemoveListener(ResetPlayerPrefs);

    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("CreateShape");
        PlayerPrefs.DeleteKey("MergeBalls");
        PlayerPrefs.DeleteKey("SpawnBall");

        GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] = 20;
        
        HCB.Core.EventManager.OnPlayerDataChange.Invoke();

    }
    
    

    
}
