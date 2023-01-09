using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using HCB.IncrimantalIdleSystem.Examples;
using HCB.PoolingSystem;
using HCB.SplineMovementSystem;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BounceCollision : MonoBehaviour
{
    private SkinnedMeshRenderer[] _meshRenderer;
    private Tween _blendShapeTween;

    private const float BLEND_SHAPE_DURATION = .1f;
    private const float BLEND_SHAPE_BACK_EASE = .3f;

    private string _scaleTweenID;
    
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.5f;

    [SerializeField] private float _forceAmount = 30f;
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("WeakBall"))
        {
            
            ChangeBlendShape(0, 100, BLEND_SHAPE_DURATION);
            EarnMoney(1);
            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,1) * 50, ForceMode.Impulse);
            collision.gameObject.transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);
              
            HapticManager.Haptic(HapticTypes.LightImpact);
            

            if (collision.gameObject.transform.localScale.x < 0 || collision.gameObject.transform.localScale.y < 0 ||
                collision.gameObject.transform.localScale.z < 0)
            {
                Debug.Log("Destroyed");
                Destroy(collision.collider.gameObject);
                BallManager.Instance.NumWeakBalls--;
            }
                  

        }
        
        if (collision.collider.CompareTag("MediumBall"))
        {
            ChangeBlendShape(0, 100, BLEND_SHAPE_DURATION);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,1) * 50, ForceMode.Impulse);
            collision.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            HapticManager.Haptic(HapticTypes.LightImpact);
            EarnMoney(4);

            if (collision.gameObject.transform.localScale.x < 0 || collision.gameObject.transform.localScale.y < 0 ||
                collision.gameObject.transform.localScale.z < 0)
            {
                Debug.Log("Destroyed");
                Destroy(collision.collider.gameObject);
                BallManager.Instance.NumMediumBalls--;
            }
                
        }
    }

    private void Start()
    {
        _meshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        _scaleTweenID = GetInstanceID() + "scaleUp";
    }



    private void ChangeBlendShape(float startValue, float endValue, float duration)
    {
        float blendShapeWeight = Mathf.Lerp(startValue, endValue, duration);

        _blendShapeTween = DOTween.To(() => blendShapeWeight, x => blendShapeWeight = x, endValue, duration);

        _blendShapeTween.OnUpdate(() =>
        {
            foreach (var mesh in _meshRenderer)
            {
                mesh.SetBlendShapeWeight(0, blendShapeWeight);
            }
        }).OnComplete(() => { ChangeBlendShapeToNormal(100, 0, BLEND_SHAPE_BACK_EASE); });

    }

    private void ChangeBlendShapeToNormal(float startValue, float endValue, float duration)
    {
        float blendShapeWeight = Mathf.Lerp(startValue, endValue, duration);

        _blendShapeTween = DOTween.To(() => blendShapeWeight, x => blendShapeWeight = x, endValue, duration);

        _blendShapeTween.OnUpdate(() =>
        {
            foreach (var mesh in _meshRenderer)
            {
                mesh.SetBlendShapeWeight(0, blendShapeWeight);
            }
        });

    }
    
    public void EarnMoney(float value)
    {
        GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += value;
       
        IdleUpgradeButton[] idleUpgradeButtons = FindObjectsOfType<IdleUpgradeButton>();
        foreach (var idleUpgradeButton in idleUpgradeButtons)
        {
            idleUpgradeButton.CheckBuyablity(null);
            
        }
        HCB.Core.EventManager.OnMoneyEarned.Invoke();
        HCB.Core.EventManager.OnPlayerDataChange.Invoke();
        
        CreateFloatingText("+" + value.ToString("N1") + " $", .7f);
    }
    
    public void CreateFloatingText(string s, float delay)
    {
        GameObject text =
            PoolingSystem.Instance.InstantiateAPS("MoneyText", gameObject.transform.position + Vector3.up);
        //text.transform.LookAt(Camera.main.transform);
        text.GetComponentInChildren<TextMeshPro>().DOFade(100, .01f);
        text.GetComponentInChildren<TextMeshPro>().SetText(s);
        //text.DOFade(1, 1f);
        
        text.transform.DOMoveZ(text.transform.position.z + 2f, delay);
        text.GetComponentInChildren<TextMeshPro>().DOFade(0, delay)
            .SetDelay(delay)
            .OnComplete(() => PoolingSystem.Instance.DestroyAPS(text));
    }
    
    

   
}
