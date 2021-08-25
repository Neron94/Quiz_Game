using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

public class AnswerBounceAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float toSideValue;
    [SerializeField] private float bounceValue;
    [SerializeField] private Ease seqEase;
    [SerializeField] private Sequence fadeSequence;
    [SerializeField] private bool isAnimPlaying; // ���� �� ���������� �������� ���� ��� ��� ����
    private TweenCallback myCallBack; // ���� ��� (������� ���������) ����� ��� ��������� ���������� ������ ��������

    private void Start()
    {
        myCallBack += AnimTracker;
        isAnimPlaying = true;
    }
    //���������� ����������� �������� (� ����� ������ ������������ ��������)
    private void AnimTracker()
    {
        if (isAnimPlaying) isAnimPlaying = false;
        else isAnimPlaying = true;
    }

    //����� �������� ������� ������
    public void BounceScale(Transform objectToBounce)
    {
        if(isAnimPlaying == true)
        {
            fadeSequence = DOTween.Sequence();
            fadeSequence.Append(objectToBounce.DOScale(bounceValue, duration).SetEase(seqEase)).OnStart(myCallBack);
            fadeSequence.AppendInterval(0.5f);
            fadeSequence.Append(objectToBounce.DOScale(1, duration).SetEase(seqEase)).OnComplete(myCallBack);
        }
    }
    //���� ������ ��� ��������� ������
    public void Shake(Transform objectToBounce)
    {
        if (isAnimPlaying == true)
        {
            fadeSequence = DOTween.Sequence();
            fadeSequence.Append(objectToBounce.DOShakePosition(toSideValue, duration).SetEase(seqEase)).OnStart(myCallBack).OnComplete(myCallBack);
        }
    }

}
