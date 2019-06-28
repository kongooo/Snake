﻿using System.Collections;
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
        if (LevelData.Instance != null && LevelData.Instance.StartScene)
        {
            LevelData.Instance.InitSettingPanel();
            InitSceneState();
        }
        else
        {
            LevelData1.Instance.InitSettingPanel();
        }
    }
    private void Start()
    {
        UpdateLength(SnakeControl.Instance.length);
        UpdateSpeed((int)SnakeControl.Instance.speed);
        UpdateScore(50);
        EndCanvas.planeDistance = 0;
        UICanvas.planeDistance = 1;
    }

    void Update()
    {
        if (!test && (SnakeControl.Instance.death || GetScore() < 0))
        {
            test = true;
            GameOver();
        }
    }

    void InitSceneState()
    {
         LevelData.Instance.StartScene = false;
    }

    public void GameOver()
    {
        ScoreText.text = Score.text;
        UICanvas.gameObject.SetActive(false);
        EndCanvas.planeDistance = 1;
        GameObject.Find("Setting").SetActive(false);
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