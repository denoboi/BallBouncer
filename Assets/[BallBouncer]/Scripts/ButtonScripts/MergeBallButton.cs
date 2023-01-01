using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class MergeBallButton : IdleStatObjectBase
{
    public override void UpdateStat(string id)
    {
        if (id == "MergeBalls")
        {
           BallManager.Instance.MergeBalls();
        }
    }


   
}
