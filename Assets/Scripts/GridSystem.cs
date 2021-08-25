using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private float cellSize;                            // ������ ��������
    [SerializeField] private float marginOfCells;                       //������ ����� ����������
    [SerializeField] private const float centerIndentOfParent = 18;     // ������ �� ������ ��� ���������� �����
    [SerializeField] private const float defZofcardParent = 100;        //�������� �� ��������� Z ������� ����� (��������� ��� ������)
    [SerializeField] private GameObject cardElement;                    // ������ �������� ��� ������
    [SerializeField] private Transform parentForCards;                  // ������ �� ������ �������� ��� ���� ����� (��� ������������ �����)
    [SerializeField] private List<GameObject> cardsOnDesk;              //������ ���� �������� �����

    public List<GameObject> CardsOnDesk => cardsOnDesk;

    public void GenerateGrid(int line, int column)
    {
        //���������� �����
        cardsOnDesk.Clear();
        for (int lineCount = 0; lineCount < line; lineCount++)
        {
            for (int columnCount = 0; columnCount < column; columnCount++)
            {
                GameObject curCard = Instantiate(cardElement, parentForCards);
                curCard.transform.localPosition = new Vector3((cellSize*columnCount)+ marginOfCells * columnCount, (lineCount*cellSize)+marginOfCells*lineCount,0);
                cardsOnDesk.Add(curCard);
            }
        }
        parentForCards.localPosition = new Vector3(-(cellSize+marginOfCells), centerIndentOfParent * -(line-1), defZofcardParent);
    }
    public void InitGrid(List<Sprite> spriteList, List<string> text)
    {
        //�������������� ����� ���������� ��������
        int count = 0;
        foreach (GameObject cell in cardsOnDesk)
        {
            cell.GetComponent<CellManager>().InitCell(spriteList[count], count, text[count]);
            count++;
        }
    }

}
