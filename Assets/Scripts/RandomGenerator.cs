using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField] private List<Sprite> whiteList;        //�������� ������ ��� �� ������������� �������� ����������� �������� ��� ������
    
    public List<Sprite> Generate(Card_SO cardType, int cellCount,out List<string> textId)
    {
        //���������� ���������� ��� �������� ������
        whiteList.Clear();
        textId = new List<string>();
        int maxCells = cellCount;
        for (int i = cellCount; i > 0; i--)
        {
            int range = Random.Range(0, cardType.AllIconTypes.Count-1);
            if (IsSptiteInWhiteList(cardType.AllIconTypes[range]) == false)
            {
                whiteList.Add(cardType.AllIconTypes[range]);
                textId.Add(cardType.Id[range]);
            }
            else // ���� ����������� �������� �������� ����������� �� ���������� �������������� ������� ��� ������� � ���� ��� ������
            {
                int iterationCount = 0;
                foreach (Sprite sprite in cardType.AllIconTypes) 
                {
                    if (IsSptiteInWhiteList(sprite) == false)
                    {
                        whiteList.Add(sprite);
                        textId.Add(cardType.Id[iterationCount]);
                        break;
                    }
                    iterationCount++;
                }

            }

        }
        return whiteList;
    }
    private bool IsSptiteInWhiteList(Sprite spriteToTest)
    {
        //�������� ���� �� �������� �������� ��� � ������ �������� �� �������
        foreach (Sprite iconSprite in whiteList)
        {
            if (iconSprite == spriteToTest) return true;
        }
        return false;
    }
}
