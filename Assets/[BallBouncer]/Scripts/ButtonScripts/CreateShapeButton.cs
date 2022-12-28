using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateShapeButton : MonoBehaviour
{
    public void OnClick()
    {
        GameObject.Find("ShapeCreator").GetComponent<ShapeCreator>().CreateShape();
    }
}
