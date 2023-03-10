using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HCB.Core;
using HCB.Utilities;
using TMPro;

namespace HCB.IncrimantalIdleSystem.Examples
{
    public class IdleUpgradeButton : IdleStatUpgraderBase
    {
        
        
        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        [Header("Components")]
        // [SerializeField]
        // private TextMeshProUGUI StatIDText;
        [SerializeField]
        private TextMeshProUGUI StatLevelText;
        [SerializeField]
        private TextMeshProUGUI StatCostText;

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null)
                return;
            
            
            SceneController.Instance.OnSceneLoaded.AddListener(InitializeButton);
            Button.onClick.AddListener(UpgradeStat);
            EventManager.OnStatUpdated.AddListener(CheckBuyablity);
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null)
                return;
            
            SceneController.Instance.OnSceneLoaded.RemoveListener(InitializeButton);
            Button.onClick.RemoveListener(UpgradeStat);
            EventManager.OnStatUpdated.RemoveListener(CheckBuyablity);
        }

       
        public virtual void CheckBuyablity(string id)
        {
            
                Button.interactable = GameManager.Instance.PlayerData.CurrencyData[IdleStat.ExchangeType] >= IdleStat.CurrentCost;
        }

       
        
        
        private void InitializeButton()
        {
            Button.interactable = GameManager.Instance.PlayerData.CurrencyData[IdleStat.ExchangeType] >= IdleStat.CurrentCost;
            
            StatLevelText.SetText("lvl " + (IdleStat.Level + 1));
           // StatCostText.SetText(HCBUtilities.ScoreShow(IdleStat.CurrentCost));
            StatCostText.SetText(IdleStat.CurrentCost + "$");

        }

        public override void UpgradeStat()
        {

            // if (IdleStat.StatID == "MergeBalls" && !_canMerge)
            //     return;
            
            if (GameManager.Instance.PlayerData.CurrencyData[IdleStat.ExchangeType] < IdleStat.CurrentCost)
            {
                Button.interactable = false;
                return;
            }

           
            GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] -= (int)IdleStat.CurrentCost;
            EventManager.OnCurrencyInteracted.Invoke(IdleStat.ExchangeType, GameManager.Instance.PlayerData.CurrencyData[IdleStat.ExchangeType]);
            base.UpgradeStat();
            Button.interactable = GameManager.Instance.PlayerData.CurrencyData[IdleStat.ExchangeType] > IdleStat.CurrentCost;
            StatLevelText.SetText("lvl " + IdleStat.Level);
            StatCostText.SetText(IdleStat.CurrentCost + "$");
            
            EventManager.OnPlayerDataChange.Invoke();;
        }
    }
}
