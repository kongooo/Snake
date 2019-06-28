using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    private static LevelData _instance;
    public static LevelData Instance { get { return _instance; } }
    
    public enum Level { Easy, Normal, Hard}
    public Level level;
    public bool StartScene = true;
    [HideInInspector]public int BadGrass, Food, Sheild, Boom, Mush, Energy, SmartGrass, Wall, CommonSpeed, ForthSpeed;

    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            InitLevelData();
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEasyLevel()
    {
        level = Level.Easy;
        InitLevelData();    
    }

    public void SetNormalLevel()
    {
        level = Level.Normal;
        InitLevelData();
    }
    
    public void SetHardLevel()
    {
        level = Level.Hard;
        InitLevelData();
    }

    public void InitSettingPanel()
    {
        switch ((int)level)
        {
            case 0:
                GameObject.Find("MidOpen").SetActive(false);
                GameObject.Find("HighOpen").SetActive(false);
                break;
            case 1:
                GameObject.Find("LowOpen").SetActive(false);
                GameObject.Find("HighOpen").SetActive(false);
                break;
            case 2:
                GameObject.Find("LowOpen").SetActive(false);
                GameObject.Find("MidOpen").SetActive(false);
                break;
        }
    }

    void InitLevelData()
    {
        switch(level)
        {
            case Level.Easy:
                BadGrass = 20;
                Food = 40;
                Sheild = 10;
                Boom = 10;
                Mush = 15;
                Energy = 15;
                SmartGrass = 15;
                Wall = 20;
                CommonSpeed = 4;
                ForthSpeed = 5;
                break;
            case Level.Normal:
                BadGrass = 30;
                Food = 40;
                Sheild = 10;
                Boom = 20;
                Mush = 10;
                Energy = 10;
                SmartGrass = 10;
                Wall = 30;
                CommonSpeed = 5;
                ForthSpeed = 6;
                break;
            case Level.Hard:
                BadGrass = 40;
                Food = 40;
                Sheild = 10;
                Boom = 30;
                Mush = 5;
                Energy = 5;
                SmartGrass = 5;
                Wall = 40;
                CommonSpeed = 6;
                ForthSpeed = 7;
                break;
        }
    }
}
