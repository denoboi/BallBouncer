using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.Utilities;


namespace HCB.CollectableSystem.FloatingCollectableSystem
{
    public abstract class FloatingCollectableBase : CollectableBase
    {
        private bool _isCollected;
        public bool IsCollected { get { return _isCollected; } set { _isCollected = value; } }

        public int CurrencyAmount = 1;        

        public override void Collect(Collector collector)
        {
            if (IsCollected)
                return;

            IsCollected = true;
            base.Collect(collector);
          
            FloatingCollectableEventManager.OnFloatingCollectableCollected.Invoke(transform.position, OnFloatingMovementCompleted);          
            Destroy(gameObject);
        }

        public virtual void OnFloatingMovementCompleted() 
        {
            PlayerData playerData = GameManager.Instance.PlayerData;
            playerData.CurrencyData[ExchangeType.Coin] += CurrencyAmount;
            HapticManager.Haptic(HapticTypes.RigidImpact);
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}
