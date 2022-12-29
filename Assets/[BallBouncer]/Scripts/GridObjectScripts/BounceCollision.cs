using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out BounceVelocity bounceVelocity))
        {
            // bounceVelocity.BounceSpeed(collision.contacts[0].normal);
            // Debug.Log("BounceSpeed" + bounceVelocity);
        }
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            ball.Bounce();
            ChangeBlendShape(0, 100, BLEND_SHAPE_DURATION);
            HCB.Core.EventManager.OnPlayerDataChange.Invoke();
            EarnMoney();
            

        }
        else
        {
            Debug.Log("No ball component found");
        }
    }

    private void Start()
    {
        _meshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
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
    
    public void EarnMoney()
    {
        GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += 1;
        HCB.Core.EventManager.OnMoneyEarned?.Invoke();
        
        CreateFloatingText("+" + 1.ToString("N1") + " $", Color.green, 1f);
    }
    
    public void CreateFloatingText(string s, Color color, float delay)
    {
        TextMeshPro text = PoolingSystem.Instance.InstantiateAPS("MoneyText",gameObject.transform.position).GetComponent<TextMeshPro>();
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
