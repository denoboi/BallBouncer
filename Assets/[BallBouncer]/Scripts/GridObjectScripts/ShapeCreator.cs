using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeCreator : MonoBehaviour
{
    
    public GameObject[] _shapes;
    
    public void CreateShape()
    { 
        int randomShape = Random.Range(0, _shapes.Length);
        Instantiate(_shapes[randomShape], transform.position, Quaternion.identity);
    }
}
