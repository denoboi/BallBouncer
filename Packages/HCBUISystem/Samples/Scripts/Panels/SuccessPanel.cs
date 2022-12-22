using HCB.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.UI
{
    public class SuccessPanel : FadePanelBase
    {
        public bool IsButtonInteracted { get; private set; }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(OnSceneLoaded);
            GameManager.Instance.OnStageSuccess.AddListener(ShowPanelAnimated);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(OnSceneLoaded);
            GameManager.Instance.OnStageSuccess.RemoveListener(ShowPanelAnimated);
        }

        public void NextButton()
        {
            if (IsButtonInteracted)
                return;

            IsButtonInteracted = true;
            HidePanelAnimated();

            LevelManager.Instance.LoadNextLevel();
        }

        private void OnSceneLoaded()
        {
            IsButtonInteracted = false;
            HidePanel();
        }
    }
}
