using UnityEngine;
using DG.Tweening;
public class DotScale : MonoBehaviour
{
    #region Parameters

    #endregion
    #region MonoBehaviour Methods
    private void OnEnable()
    {
        transform.DOScale(1.2f, 1).SetLoops(-1, LoopType.Yoyo);
    }
    #endregion
    #region My Methods

    #endregion
}