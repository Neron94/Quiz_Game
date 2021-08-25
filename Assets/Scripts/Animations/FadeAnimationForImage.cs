using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeAnimationForImage : MonoBehaviour
{
    [SerializeField] private Image myImage;
    [SerializeField] private float duration;
    [SerializeField] private Ease seqEase;
    [SerializeField] private Sequence fadeSequence;
    
    //��������� ��������
    public void FadeIn()
    {
        Fade(1);
    }
    //������������ ��������
    public void FadeOut()
    {
        fadeSequence.Join(myImage.DOFade(0, duration).SetEase(seqEase));
    }
    //������� ����� �������� ��������
    public void Fade(float value)
    {
        fadeSequence = DOTween.Sequence();
        fadeSequence.Append(myImage.DOFade(value, duration).SetEase(seqEase));
        fadeSequence.AppendInterval(1);
    }
}
