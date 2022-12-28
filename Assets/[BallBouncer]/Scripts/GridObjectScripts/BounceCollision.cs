using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BounceCollision : MonoBehaviour
{
    private SkinnedMeshRenderer[] _meshRenderer;
    private Tween _blendShapeTween;

    private const float BLEND_SHAPE_DURATION = .1f;
    private const float BLEND_SHAPE_BACK_EASE = .3f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Ball>(out Ball ball))
        {
            ball.Bounce();
            ChangeBlendShape(0, 100, BLEND_SHAPE_DURATION);


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
}
