using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour, IExamine, IIsGameStart
{
    //Основной пульт управления игрой
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private List<LevelConfig> allLevels;           //список всех левлов
    [SerializeField] private RandomGenerator randomGenerator;       //генератор уровня
    [SerializeField] private int levelsPast;                        //идентификатор для корректного перехода по уровням
    [SerializeField] private int taskId;                            //идентификатор верного ответа на текущем уровне
    [SerializeField] private Text taskText;                         //текст верного ответа
    [SerializeField] private List<string> textId;                   //вместилище текстовых ответов сгенерированных карточек для уровня
    [SerializeField] private List<Sprite> generatedSprites;         //вместилище иконок карточек сгенерированных для уровня
    [SerializeField] private bool gameIsStart;                      //флаг началась ли игра
    [SerializeField] private GameObject curtainCanvas;              //занавес рестарта
    [SerializeField] private GameObject loadingScreen;              //занавес загрузки

    private void Awake()
    {
        curtainCanvas = GameObject.Find("curtainCanvas").gameObject;
        loadingScreen = curtainCanvas.transform.GetChild(2).gameObject;
        loadingScreen.SetActive(false);
        curtainCanvas.SetActive(false);
    }
    private void Start()
    {
        GameStart();
    }
    private void GameStart()
    {
        //Запуск цикла уровней
        GenerateLevel();
        gameIsStart = true;
    }
    private LevelConfig LevelConfig()
    {
        //Выдаем конфиг нужного уровня
        return allLevels[levelsPast];
    }
    private void GenerateLevel()
    {
        //генерируем список карточек для уровня
        gridSystem.GenerateGrid(LevelConfig().LevelGridConfig[0], LevelConfig().LevelGridConfig[1]);
        InitTable();
        SetTask();
        levelsPast++;
    }
    private void InitTable()
    {
        //Инициализируем все карточки
        textId.Clear();
        generatedSprites.Clear();
        generatedSprites = randomGenerator.Generate(LevelConfig().LevelCardsConfig, 
                                                    LevelConfig().LevelGridConfig[0] * LevelConfig().LevelGridConfig[1], out textId);
        gridSystem.InitGrid(generatedSprites, textId);
    }
    private void SetTask()
    {
        //Определяем Верный ответ для уровня
        taskId = Random.Range(0, gridSystem.CardsOnDesk.Count-1);
        taskText.text = "Find: "+gridSystem.CardsOnDesk[taskId].GetComponent<CellManager>().TextId;
    }
    private  IEnumerator Restart()
    {   
        //Перезапуск игры с интервалом в 2 сек загрузки
        gameIsStart = false;
        levelsPast = 0;
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<FadeAnimationForImage>().FadeIn();
        loadingScreen.GetComponent<FadeAnimationForImage>().FadeOut();
        yield return new WaitForSeconds(2);
        GameStart();
        loadingScreen.SetActive(false);
        curtainCanvas.SetActive(false);
    }
    
    public void RestartGameCoroutine()
    {
        //Запуск курутины отступа времени для Загрузочного экрана (Ссылка для кнопки рестарта)
        StartCoroutine("Restart");
    }
    public void ExamineTask(CellManager cell)
    {
        //Проверка верности ответа
        if (cell.Id == taskId)
        {
            cell.Animate(true);
            StartCoroutine("NextLevel");
        }
        else cell.Animate(false);
    }
    public IEnumerator NextLevel()
    {
        //Загрузка следующего уровня с отступом в 3 сек
        yield return new WaitForSeconds(3f);
        foreach(GameObject cell in gridSystem.CardsOnDesk)
        {
            Destroy(cell);
        }
        
        if(allLevels.Count == levelsPast)
        {
            curtainCanvas.SetActive(true);
            curtainCanvas.GetComponent<FadeAnimationForImage>().FadeIn();
        }
        else
        {
            GenerateLevel();
        }
    }
    public bool IsGameStart()
    {
        //Доступ к флагу стартовала ли игра
        return gameIsStart;
    }

}
