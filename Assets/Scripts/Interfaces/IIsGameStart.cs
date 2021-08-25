using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIsGameStart
{
    //Интерфейс для проверки флага запущена ли игра для треканья показа боунса анимации
    public bool IsGameStart();
}
