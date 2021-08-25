using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour, IExamine, IIsGameStart
{
    //�������� ����� ���������� �����
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private List<LevelConfig> allLevels;           //������ ���� ������
    [SerializeField] private RandomGenerator randomGenerator;       //��������� ������
    [SerializeField] private int levelsPast;                        //������������� ��� ����������� �������� �� �������
    [SerializeField] private int taskId;                            //������������� ������� ������ �� ������� ������
    [SerializeField] private Text taskText;                         //����� ������� ������
    [SerializeField] private List<string> textId;                   //���������� ��������� ������� ��������������� �������� ��� ������
    [SerializeField] private List<Sprite> generatedSprites;         //���������� ������ �������� ��������������� ��� ������
    [SerializeField] private bool gameIsStart;                      //���� �������� �� ����
    [SerializeField] private GameObject curtainCanvas;              //������� ��������
    [SerializeField] private GameObject loadingScreen;              //������� ��������

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
        //������ ����� �������
        GenerateLevel();
        gameIsStart = true;
    }
    private LevelConfig LevelConfig()
    {
        //������ ������ ������� ������
        return allLevels[levelsPast];
    }
    private void GenerateLevel()
    {
        //���������� ������ �������� ��� ������
        gridSystem.GenerateGrid(LevelConfig().LevelGridConfig[0], LevelConfig().LevelGridConfig[1]);
        InitTable();
        SetTask();
        levelsPast++;
    }
    private void InitTable()
    {
        //�������������� ��� ��������
        textId.Clear();
        generatedSprites.Clear();
        generatedSprites = randomGenerator.Generate(LevelConfig().LevelCardsConfig, 
                                                    LevelConfig().LevelGridConfig[0] * LevelConfig().LevelGridConfig[1], out textId);
        gridSystem.InitGrid(generatedSprites, textId);
    }
    private void SetTask()
    {
        //���������� ������ ����� ��� ������
        taskId = Random.Range(0, gridSystem.CardsOnDesk.Count-1);
        taskText.text = "Find: "+gridSystem.CardsOnDesk[taskId].GetComponent<CellManager>().TextId;
    }
    private  IEnumerator Restart()
    {   
        //���������� ���� � ���������� � 2 ��� ��������
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
        //������ �������� ������� ������� ��� ������������ ������ (������ ��� ������ ��������)
        StartCoroutine("Restart");
    }
    public void ExamineTask(CellManager cell)
    {
        //�������� �������� ������
        if (cell.Id == taskId)
        {
            cell.Animate(true);
            StartCoroutine("NextLevel");
        }
        else cell.Animate(false);
    }
    public IEnumerator NextLevel()
    {
        //�������� ���������� ������ � �������� � 3 ���
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
        //������ � ����� ���������� �� ����
        return gameIsStart;
    }

}
