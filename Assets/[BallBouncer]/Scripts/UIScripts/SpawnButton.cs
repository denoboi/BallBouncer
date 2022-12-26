using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
   public void OnClick()
   {
        GameObject.Find("BallCreator").GetComponent<BallCreator>().CreateBalls();
   }
}
