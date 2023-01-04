using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody Rb => _rb == null ? _rb = GetComponent<Rigidbody>() : _rb;
    public int Level;
    
    
    private string _scaleTweenID;
    private string _moveTweenID;
    
    private void Start()
    {
        _scaleTweenID = GetInstanceID() + "scaleUp";
        _moveTweenID = GetInstanceID() + "move";
    }
    
    public void Initialize()
    {
        Vector3 force = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Rb.AddForce(force, ForceMode.Impulse);
    }
    
    public void ScaleTween(Vector3 from, Vector3 to, float duration, float delay = 0, Action onComplete = null)
    {
        DOTween.Kill(_scaleTweenID);
        transform.localScale = from;
        transform.DOScale(to, duration).SetEase(Ease.Linear).SetId(_scaleTweenID).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
    
    public void MoveTween(Vector3 from, Vector3 to, float duration, float delay = 0, Action onComplete = null)
    {
        DOTween.Kill(_moveTweenID);
        transform.position = from;
        transform.DOMove(to, duration).SetEase(Ease.Linear).SetId(_moveTweenID).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
   
}
