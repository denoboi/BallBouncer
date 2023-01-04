using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;
using UnityEngine.UI;

public class CreateShapeButton : MonoBehaviour
{
    
    public StairTypes SelectStairTypes;
   
   
    public enum StairTypes
    {
        Flat = 0,
        L = 1,
        Z = 2,
        Block = 3
    }
    
    public void CreateStair()
    {
        EventManager.OnShapeCreated.Invoke(SelectStairTypes);
        EventManager.CloseShapePanel.Invoke();
    }
   
}
