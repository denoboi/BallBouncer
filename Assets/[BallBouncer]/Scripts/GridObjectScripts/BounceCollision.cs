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
    public void ScaleTween(Vector3 from, Vector3 to, float duration, float delay = 0, Action onComplete = null)
    {
        DOTween.Kill(_scaleTweenID);
        transform.localScale = from;
        transform.DOScale(to, duration).SetEase(Ease.Linear).SetId(_scaleTweenID).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            
            ChangeBlendShape(0, 100, BLEND_SHAPE_DURATION);
            EarnMoney(1);
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,1) * 30, ForceMode.Impulse);
            //ball.transform.DOScale(new Vector3(.5f, .5f, .5f), .5f).SetEase(Ease.OutBounce);
            
              //ball.transform.DOShakeScale(shakeDuration,shakeAmount,10,90,true);
              ball.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

              if (collision.collider.CompareTag("MediumBall"))
                  EarnMoney(4);
        }
        else
        {
            Debug.Log("No ball component found");
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
        HCB.Core.EventManager.OnMoneyEarned?.Invoke();
        HCB.Core.EventManager.OnPlayerDataChange?.Invoke();
        
        CreateFloatingText("+" + 1.ToString("N1") + " $", Color.green, .7f);
    }
    
    public void CreateFloatingText(string s, Color color, float delay)
    {
        TextMeshPro text = PoolingSystem.Instance.InstantiateAPS("MoneyText",gameObject.transform.position + Vector3.up).GetComponentInChildren<TextMeshPro>();
        //text.transform.LookAt(Camera.main.transform);
        text.SetText(s);
        //text.DOFade(1, 1f);
        text.color = color;
        text.transform.DOMoveZ(text.transform.position.z + 2f, delay);
        text.DOFade(0, delay / 2)
            .SetDelay(delay / 2)
            .OnComplete(() => PoolingSystem.Instance.DestroyAPS(text.gameObject));
    }

   
}
