using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CellManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer icon; //������ ��������
    [SerializeField] private int id; //������������� ��� ��������
    [SerializeField] private string textId; //��������� �������������
    [SerializeField] private IExamine examineCell;
    [SerializeField] private AnswerBounceAnimation animController; //���������� ��������� ����������
    [SerializeField] private BounceAnimation startBounceAnim; //���������� ����� ���������� ���������
    [SerializeField] private IIsGameStart gameHandler;
    [SerializeField] private GameObject particles; //������� ��� ������� ������ (���������)
    private UnityAction<CellManager> clickEvent; // ����� �� ������� �� ��������
    
    private void Awake()
    {
        examineCell = GameObject.Find("GameCore").GetComponent<GameHandler>();
        gameHandler = GameObject.Find("GameCore").GetComponent<GameHandler>();
        icon = transform.GetChild(0).GetComponent<SpriteRenderer>();
        clickEvent += examineCell.ExamineTask;
        animController = transform.GetComponent<AnswerBounceAnimation>();
        particles = transform.GetChild(1).gameObject;
        particles.SetActive(false);
    }

    public SpriteRenderer Icon => icon;
    public int Id => id;
    public string TextId => textId;

    public void InitCell(Sprite myIcon, int iD, string text)
    {
        icon.sprite = myIcon;
        id = iD;
        textId = text;
        #region �������� ��� �������� �������� ��� � ������ ��������
        if (textId == "7") icon.transform.Rotate(0,0,-90);
        else if (textId == "8") icon.transform.Rotate(0, 0, -90);
        #endregion
        if (gameHandler.IsGameStart() == false) transform.GetComponent<BounceAnimation>().BounceScale(transform); //���������� ��������� ���������
        else
        {
            transform.localScale = new Vector3(5.5f,5.5f,5.5f); // � ����� ������ �������� ����� ������� ������ ��� ��������
                                                                 //��������� - ����� �� �� ���������� ��������� �������� ������ ��� ��� �������� �� �����������
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        clickEvent.Invoke(this);
    }
    // ���������� ���������
    public void Animate(bool answerIs)
    {
        if (answerIs)
        {
            animController.BounceScale(transform.GetChild(0));
            particles.SetActive(true);
        }
        else animController.Shake(transform.GetChild(0));
    } 
}
