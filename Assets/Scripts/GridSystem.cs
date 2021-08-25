using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private float cellSize;                            // размер карточки
    [SerializeField] private float marginOfCells;                       //отступ между карточками
    [SerializeField] private const float centerIndentOfParent = 18;     // отступ от центра для построения сетки
    [SerializeField] private const float defZofcardParent = 100;        //Значение по умолчанию Z позиции сетки (корректно для камеры)
    [SerializeField] private GameObject cardElement;                    // префаб карточки для спавна
    [SerializeField] private Transform parentForCards;                  // ссылка на объект родителя для всей сетки (для выравнивания сетки)
    [SerializeField] private List<GameObject> cardsOnDesk;              //Список всех карточек левла

    public List<GameObject> CardsOnDesk => cardsOnDesk;

    public void GenerateGrid(int line, int column)
    {
        //Генерируем сетку
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
        //Инициализируем сетку значениями карточек
        int count = 0;
        foreach (GameObject cell in cardsOnDesk)
        {
            cell.GetComponent<CellManager>().InitCell(spriteList[count], count, text[count]);
            count++;
        }
    }

}
