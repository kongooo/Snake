using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Scene1Controller : MonoBehaviour
{
    private static Scene1Controller _instance;
    public static Scene1Controller Instance { get { return _instance; } }

    public TextMeshProUGUI Length, Score, Speed, ScoreText;

    public Canvas EndCanvas, UICanvas;

    private bool test = false;

    private void Awake()
    {
        _instance = this;
        UpdateScore(50);
        EndCanvas.planeDistance = 0;
        UICanvas.planeDistance = 1;        
    }

    private void OnEnable()
    {
        if (LevelData.Instance != null && LevelData.Instance.StartScene)
        {
            InitStartData();
        }
        else
        {
            InitScene1Data();
        }
    }

    private void Start()
    {
        UpdateLength(SnakeControl.Instance.length);
        UpdateSpeed((int)SnakeControl.Instance.speed);
    }


    void Update()
    {
        if (!test && (SnakeControl.Instance.death || GetScore() < 0))
        {
            test = true;
            GameOver();            
        }
    }

    void InitStartData()
    {
        MapManager.Instance.badGrassCount = LevelData.Instance.BadGrass;
        MapManager.Instance.foodCount = LevelData.Instance.Food;
        MapManager.Instance.sheildCount = LevelData.Instance.Sheild;
        MapManager.Instance.boomCount = LevelData.Instance.Boom;
        MapManager.Instance.mushCount = LevelData.Instance.Mush;
        MapManager.Instance.energyCount = LevelData.Instance.Energy;
        MapManager.Instance.smartGrassCount = LevelData.Instance.SmartGrass;
        MapManager.Instance.wallCountAverage = LevelData.Instance.Wall;
        SnakeControl.Instance.commonSpeed = LevelData.Instance.CommonSpeed;
    }

    void InitScene1Data()
    {
        MapManager.Instance.badGrassCount = LevelData1.Instance.BadGrass;
        MapManager.Instance.foodCount = LevelData1.Instance.Food;
        MapManager.Instance.sheildCount = LevelData1.Instance.Sheild;
        MapManager.Instance.boomCount = LevelData1.Instance.Boom;
        MapManager.Instance.mushCount = LevelData1.Instance.Mush;
        MapManager.Instance.energyCount = LevelData1.Instance.Energy;
        MapManager.Instance.smartGrassCount = LevelData1.Instance.SmartGrass;
        MapManager.Instance.wallCountAverage = LevelData1.Instance.Wall;
        SnakeControl.Instance.commonSpeed = LevelData1.Instance.CommonSpeed;
    }

    public void GameOver()
    {
        ScoreText.text = Score.text;
        UICanvas.gameObject.SetActive(false);
        EndCanvas.planeDistance = 1;
        GameObject.Find("Setting").SetActive(false);
        Time.timeScale = 0;
    }

    public void GameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameStop()
    {
        SnakeControl.Instance.startMove = false;
        UICanvas.planeDistance = 0;
    }

    public void GameContinue()
    {
        SnakeControl.Instance.startMove = true;
        UICanvas.planeDistance = 1;
    }

    public void UpdateLength(int length)
    {
        Length.text = length.ToString();
    }

    public void UpdateScore(int score)
    {
        Score.text = score.ToString();
    }
    public void UpdateSpeed(int speed)
    {
        Speed.text = speed.ToString();
    }

    private int GetScore()
    {
        return Int32.Parse(Score.text);
    }

    public void ChangeScore(int num)
    {
        Score.text = (GetScore() + num).ToString();
    }

    public void DoubleScore()
    {
        Score.text = (GetScore() * 2).ToString();
    }

    public void MulScore()
    {
        Score.text = (GetScore() / 2).ToString();
    }
}