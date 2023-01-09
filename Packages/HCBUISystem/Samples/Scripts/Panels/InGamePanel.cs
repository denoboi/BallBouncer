using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using UnityEngine.UI;


namespace HCB.UI
{
    public class InGamePanel : HCBPanel
    {

        [SerializeField] private Image _cursorImage;
        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
            LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);
            GameManager.Instance.OnStageFail.AddListener(HidePanel);
            GameManager.Instance.OnStageSuccess.AddListener(HidePanel);
        }



        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
            LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
            GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
            GameManager.Instance.OnStageSuccess.RemoveListener(HidePanel);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                Cursor.visible = false;
            _cursorImage.transform.position = Input.mousePosition;
        }
    }
}
