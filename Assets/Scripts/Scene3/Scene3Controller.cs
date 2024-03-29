﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Scene3Controller : MonoBehaviour
{
    private static Scene3Controller _instance;
    public static Scene3Controller Instance { get { return _instance; } }

    public Canvas endCanvas, UICanvas;
    public TextMeshProUGUI scoreText, disText;
    public TextMeshProUGUI EndScoreText;
    private float timing = 0;

    private void Awake()
    {
        _instance = this;
        if (LevelData.Instance != null && LevelData.Instance.StartScene)
        {
            InitStartData();
        }
        else
        {
            InitScene3Data();
        }
    }

    private void Start()
    {
        endCanvas.planeDistance = 0;
        UICanvas.planeDistance = 1;
        UpdateScore(0);
    }


    private void Update()
    {
        UpdateDistance();
    }

    void InitStartData()
    {
        HorizontalMoveControl.Instance.speed = LevelData.Instance.CommonSpeed;
    }

    void InitScene3Data()
    {
        HorizontalMoveControl.Instance.speed = LevelData3.Instance.CommonSpeed;
    }

    public void GameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameStop()
    {
        UICanvas.planeDistance = 0;
    }

    public void GameContinue()
    {
        UICanvas.planeDistance = 1;
    }

    public void AfterDeath()
    {
        EndScoreText.text = scoreText.text;
        UICanvas.gameObject.SetActive(false);
        endCanvas.GetComponent<Canvas>().planeDistance = 100;
        GameObject.Find("Setting").SetActive(false);
        Time.timeScale = 0;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateDistance()
    {
        timing += Time.deltaTime;
        disText.text = ((int)(timing * HorizontalMoveControl.Instance.speed)).ToString();
    }


    public void ChangeScore(int num)
    {
        scoreText.text = (Int32.Parse(scoreText.text) + num).ToString();
    }
}
