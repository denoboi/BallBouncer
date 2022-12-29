using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using HCB.PoolingSystem;
using TMPro;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    
    public static IncomeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

   
}
