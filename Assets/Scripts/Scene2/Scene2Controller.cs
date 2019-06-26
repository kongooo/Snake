﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Scene2Controller : MonoBehaviour
{
    private static Scene2Controller _instance;
    public static Scene2Controller Instance { get { return _instance; } }

    public Canvas endCanvas, nextCanvas, UICanvas;
    public TextMeshProUGUI lengthText, levelText, scoreText;
    public TextMeshProUGUI EndScoreText;

    private void Awake()
    {
        _instance = this;
        endCanvas.planeDistance = 0;
        nextCanvas.planeDistance = 0;
    }

    private void Start()
    {
        UpdateLevel();
        UpdateLength(SnakeControl.Instance.length);
        UpdateScore(0);
    }

    void UpdateLevel()
    {
        int level = 0;
        switch(SceneManager.GetActiveScene().name)
        {
            case "Scene2-1":
                level = 1;
                break;
            case "Scene2-2":
                level = 2;
                break;
            case "Scene2-3":
                level = 3;
                break;
            case "Scene2-4":
                level = 4;
                break;
        }
        levelText.text = level.ToString();
    }

    public void LoadLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Scene2-1":
                SceneManager.LoadScene("Scene2-2");
                break;
            case "Scene2-2":
                SceneManager.LoadScene("Scene2-3");
                break;
            case "Scene2-3":
                SceneManager.LoadScene("Scene2-4");
                break;
        }
    }

    public void GameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AfterDeath()
    {
        Debug.Log(endCanvas.planeDistance);
        EndScoreText.text = scoreText.text;
        UICanvas.gameObject.SetActive(false);
        endCanvas.GetComponent<Canvas>().planeDistance = 100;
        Debug.Log(endCanvas.planeDistance);
    }

    public void AfterSuccess()
    {
        SnakeControl.Instance.startMove = false;
        UICanvas.gameObject.SetActive(false);
        nextCanvas.planeDistance = 100;
    }

    public void UpdateLength(int length)
    {
        lengthText.text = length.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }


    public void ChangeScore(int num)
    {
        scoreText.text = (Int32.Parse(scoreText.text) + num).ToString();
    }

}