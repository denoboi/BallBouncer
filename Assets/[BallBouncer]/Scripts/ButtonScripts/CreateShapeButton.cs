using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;
using UnityEngine.UI;

public class CreateShapeButton : IdleStatObjectBase
{

    public override void UpdateStat(string id)
    {
        if(id.Equals("CreateShape"))
            GameObject.Find("ShapeCreator").GetComponent<ShapeCreator>().CreateShape();
    }
}
