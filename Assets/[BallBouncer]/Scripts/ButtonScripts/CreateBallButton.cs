using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class CreateBallButton : IdleStatObjectBase
{
    private BallCreator _ballCreator;
    
    public BallCreator BallCreator => _ballCreator ??= GetComponent<BallCreator>();

    public override void UpdateStat(string id)
    {
        if (id.Equals("SpawnBall"))
            BallCreator.CreateBallsViaButton();
    }

   
}
