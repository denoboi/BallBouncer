using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using HCB.IncrimantalIdleSystem.Examples;
using UnityEngine;

public class MergeBallButton : IdleUpgradeButton
{
    public bool _canMerge;
    protected override void OnEnable()
    {
        
        HCB.Core.EventManager.OnMergeCheck.AddListener(MergeCheck);
        base.OnEnable();
    }
    
    protected override void OnDisable()
    {
        HCB.Core.EventManager.OnMergeCheck.RemoveListener(MergeCheck);
        base.OnDisable();
    }

    public override void CheckBuyablity(string id)
    {
        if (_canMerge == false)
        {
            Button.interactable = false;
            return;
        }
           
        base.CheckBuyablity(id);
    }
    
    private void MergeCheck(bool value)
    {
        _canMerge = value;
    }

    public override void UpgradeStat()
    {
        BallManager.Instance.MergeBalls();
        base.UpgradeStat();
    }


    // public override void UpdateStat(string id)
    // {
    //     if (id == "MergeBalls")
    //     {
    //        BallManager.Instance.MergeBalls();
    //     }
    // }


   
}
