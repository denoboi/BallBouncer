using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;


namespace HCB.UI
{
    public class InGamePanel : HCBPanel
    {
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
    }
}
