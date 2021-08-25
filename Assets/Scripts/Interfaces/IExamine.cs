using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExamine
{
    //Метод для проверки верности ответа
    public void ExamineTask(CellManager cell);
}
