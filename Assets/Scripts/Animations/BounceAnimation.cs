using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BounceAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float targValue;
    [SerializeField] private Ease seqEase;
    [SerializeField] private Sequence fadeSequence;

    //Анимация появления карточек
    public void BounceScale(Transform objectToBounce)
    {
        fadeSequence = DOTween.Sequence();
        fadeSequence.Append(objectToBounce.DOScale(targValue, duration).SetEase(seqEase));
    }
}
