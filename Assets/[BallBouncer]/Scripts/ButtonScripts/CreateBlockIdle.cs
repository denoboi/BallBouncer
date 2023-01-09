using System.Collections;
using System.Collections.Generic;
using HCB;
using HCB.Core;
using HCB.IncrimantalIdleSystem.Examples;
using UnityEngine;

public class CreateBlockIdle : IdleUpgradeButton
{
    public bool _canCreateBlock;
    
  
    
    protected override void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(()=> HCB.Core.EventManager.OnCreateShapeCheck.Invoke(true));
        HCB.Core.EventManager.OnCreateShapeCheck.AddListener(BlockCheck);
        base.OnEnable();
    }
    
    protected override void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(()=> HCB.Core.EventManager.OnCreateShapeCheck.Invoke(true));
        HCB.Core.EventManager.OnCreateShapeCheck.RemoveListener(BlockCheck);
        base.OnDisable();
    }

    public override void CheckBuyablity(string id)
    {
        if (_canCreateBlock == false)
        {
            Button.interactable = false;
            return;
        }
           
        base.CheckBuyablity(id);
    }
    
    private void BlockCheck(bool value)
    {
        _canCreateBlock = value;
    }
    
    // public override void UpgradeStat()
    // {
    //     BallManager.Instance.MergeBalls();
    //     base.UpgradeStat();
    // }
}
