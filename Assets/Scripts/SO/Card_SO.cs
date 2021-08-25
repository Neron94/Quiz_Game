using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Element", menuName = "Card Element", order = 1)]
public class Card_SO : ScriptableObject
{
    //Контейнер карточки
    [SerializeField] private List<Sprite> allIconTypes;
    [SerializeField] private List<string> id;
    public List<Sprite> AllIconTypes => allIconTypes;
    public List<string> Id => id;
}
