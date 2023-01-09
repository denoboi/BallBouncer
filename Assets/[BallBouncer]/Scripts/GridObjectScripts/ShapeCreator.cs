using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShapeCreator : MonoBehaviour //NextLevelButton buradan ayrilacak.
{
    #region Parameters

    [SerializeField] private RectTransform _createShapePanel;
    private List<GameObject> _instantiatedButtons = new List<GameObject>();
    private const int BUTTON_COUNT = 3;
    public GameObject[] Shapes;
    public GameObject NextLevelButtond;

    #endregion

    private void OnEnable()
    { 
        //Close NextLevelButton on start
        LevelManager.Instance.OnLevelStart.AddListener(CloseNextLevelButton);
        
        //Open NextLevelButton on ProgressBarFull
        EventManager.OnProgressBarFull.AddListener(OpenNextLevelButton);
        
        EventManager.OnShapeCreated.AddListener(CreateShape);
        EventManager.CloseShapePanel.AddListener(CloseCreateShapePanel);
    }

    private void OnDisable()
    {
        if(Managers.Instance == null) return;
        
        LevelManager.Instance.OnLevelStart.RemoveListener(CloseNextLevelButton);
        EventManager.OnProgressBarFull.RemoveListener(OpenNextLevelButton);

        EventManager.OnShapeCreated.RemoveListener(CreateShape);
        EventManager.CloseShapePanel.RemoveListener(CloseCreateShapePanel);


    }

    public void CreateShape(CreateShapeButton.StairTypes types) 
    {
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
    
    public void CloseCreateShapePanel()
    {
        _createShapePanel.transform.DOMoveY(-500, .2f);
    }
    
    
    
    public void NextLevelButtonClick()
    {
        GameManager.Instance.CompeleteStage(true);
    }

    public void OpenNextLevelButton()
    {
        NextLevelButtond.SetActive(true);
    }
    
    public void CloseNextLevelButton()
    {
        NextLevelButtond.SetActive(false);
    }
}
