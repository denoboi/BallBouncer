using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShapeCreator : MonoBehaviour
{
    #region Parameters

    [SerializeField] private RectTransform _createShapePanel;
    private List<GameObject> _instantiatedButtons = new List<GameObject>();
    private const int BUTTON_COUNT = 3;
    public GameObject[] Shapes;

    #endregion

    private void OnEnable()
    { 
        EventManager.OnShapeCreated.AddListener(CreateShape);
    }

    private void OnDisable()
    {
        EventManager.OnShapeCreated.RemoveListener(CreateShape);

    }

    public void CreateShape(CreateShapeButton.StairTypes types) 
    { 
        //int randomShape = Random.Range(0, Shapes.Length);
        Instantiate(Shapes[(int)types], transform.position, Quaternion.identity);
    }
    
    void RandomButtonSet()
    {
        if (_instantiatedButtons.Count>0)
        {
            for (int i = 0; i < _instantiatedButtons.Count; i++)
            {
                Destroy(_instantiatedButtons[i]);
            }
            _instantiatedButtons.Clear();
        }
        for (int i = 0; i < BUTTON_COUNT; i++)
        {
            GameObject newButton = Instantiate(Shapes[Random.Range(0, Shapes.Length)], _createShapePanel);
            _instantiatedButtons.Add(newButton);
        }
    }
    
    public void OpenCreateShapePanel()
    {
        RandomButtonSet();
        _createShapePanel.transform.DOMoveY(20, .5f);
    }
    
    public void CloseCreateStairPanel()
    {

        _createShapePanel.transform.DOMoveY(-500, .2f);
    }
}
