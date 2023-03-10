using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using UnityEngine;
using UnityEngine.UI;

public class MoneyProgressBar : MonoBehaviour
{
    private const float MAX_PROGRESS = 800f;
    [SerializeField] private IdleStat Stat;
    [Space, SerializeField] private Image BarImage;

    //[SerializeField] private RectTransform ProgressBar;
    [SerializeField] private float CurrentProgress;
    
    

    private bool _isFull;

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(ResetProgress);
        HCB.Core.EventManager.OnMoneyEarned.AddListener(Progress);
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(ResetProgress);

        HCB.Core.EventManager.OnMoneyEarned.RemoveListener(Progress);
    }


    private void Progress()
    {
        CurrentProgress++;

        var progress = CurrentProgress / MAX_PROGRESS * (int)Stat.CurrentValue;
        BarImage.fillAmount = progress;

        if (!(progress >= 1) || _isFull) return;

        _isFull = true;
        EventManager.OnProgressBarFull.Invoke();
    }
    
    public void ResetProgress()
    {
        CurrentProgress = 0;
        BarImage.fillAmount = 0;
        _isFull = false;
        //gameObject.SetActive(false);
    }
}