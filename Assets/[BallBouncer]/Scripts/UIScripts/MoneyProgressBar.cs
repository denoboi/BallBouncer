using System;
using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;
using UnityEngine.UI;

public class MoneyProgressBar : MonoBehaviour
{
    private const float MAX_PROGRESS = 100f;
    [SerializeField] private IdleStat Stat;
    [Space, SerializeField] private Image BarImage;

    [SerializeField] private RectTransform ProgressBar;
    [SerializeField] private float CurrentProgress;

    private bool _isFull;

    private void OnEnable()
    {
        HCB.Core.EventManager.OnMoneyEarned.AddListener(Progress);
    }

    private void OnDisable()
    {
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
}