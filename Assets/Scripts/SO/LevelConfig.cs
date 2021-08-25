using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Config Element", menuName = "Level Config Element", order = 1)]
public class LevelConfig : ScriptableObject
{
    //Контейнер конфигурации с содержанием всех данных уровня
    [SerializeField] private Card_SO levelCardsConfig;
    [SerializeField] private int levelLine, levelcolumn;

    public Card_SO LevelCardsConfig => levelCardsConfig;
    public int[] LevelGridConfig
    {
        get
        {
            int[] config = new int[2];
            config[0] = levelLine;
            config[1] = levelcolumn;
            return config;
        }
    }

}
