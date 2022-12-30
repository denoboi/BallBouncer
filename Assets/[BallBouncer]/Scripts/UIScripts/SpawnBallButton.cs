using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBallButton : MonoBehaviour
{
   public void OnClick()
   {
        GameObject.Find("BallCreator").GetComponent<BallCreator>().CreateBallsViaButton();
   }
}
