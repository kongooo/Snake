using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Scene1Controller : MonoBehaviour
{
    private static Scene1Controller _instance;
    public static Scene1Controller Instance { get { return _instance; } }

    public TextMeshProUGUI Length, Score, Speed;


    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        UpdateLength(SnakeControl.Instance.length);
        UpdateSpeed((int)SnakeControl.Instance.speed);
        UpdateScore(50);
    }

    void Update()
    {
        if (GetScore() < 0) SnakeControl.Instance.death = true;
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