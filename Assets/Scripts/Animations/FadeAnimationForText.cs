using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeAnimationForText: MonoBehaviour
{
    [SerializeField] private Text textToFade;
    [SerializeField] private float duration;
    [SerializeField] private Ease seqEase;
    [SerializeField] private Sequence fadeSequence;

    public void Start()
    {
        FadeIn();
    }
    //Анимация появления текста
    public void FadeIn()
    {
        fadeSequence = DOTween.Sequence();
        fadeSequence.Append(textToFade.DOFade(1,duration).SetEase(seqEase));
    }

}
