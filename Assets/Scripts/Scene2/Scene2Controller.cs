using System.Collections;
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
        UICanvas.planeDistance = 1;
        UpdateLevel();
        UpdateScore(0);

        if (LevelData.Instance != null && LevelData.Instance.StartScene)
        {
            InitStartData();
        }
        else
        {
            InitScene2Data();
        }
    }

    private void Start()
    {
        UpdateLength(SnakeControl.Instance.length);
    }

    void InitStartData()
    {
        SnakeControl.Instance.commonSpeed = LevelData.Instance.CommonSpeed;
    }

    void InitScene2Data()
    {
        SnakeControl.Instance.commonSpeed = LevelData2.Instance.CommonSpeed;
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

    public void GameStop()
    {
        UICanvas.planeDistance = 0;
    }

    public void GameContinue()
    {
        UICanvas.planeDistance = 1;
    }

    public void GameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AfterDeath()
    {
        EndScoreText.text = scoreText.text;
        UICanvas.gameObject.SetActive(false);
        endCanvas.GetComponent<Canvas>().planeDistance = 100;
        GameObject.Find("Setting").SetActive(false);
    }

    public void AfterSuccess()
    {
        SnakeControl.Instance.startMove = false;
        UICanvas.gameObject.SetActive(false);
        nextCanvas.planeDistance = 100;
        GameObject.Find("Setting").SetActive(false);
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
