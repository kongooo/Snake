using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData3 : MonoBehaviour
{
    private static LevelData3 _instance;
    public static LevelData3 Instance { get { return _instance; } }

    public enum Level { Easy, Normal, Hard }
    public Level level, levelAgo;
    [HideInInspector] public int BadGrass, Food, Sheild, Boom, Mush, Energy, SmartGrass, Wall, CommonSpeed, ForthSpeed;

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
        if (LevelData.Instance != null && LevelData.Instance.StartScene)
        {
            LevelData3.Instance.level = (LevelData3.Level)LevelData.Instance.level;
        }
    }

    public void SetEasyLevel()
    {
        LevelData3.Instance.level = Level.Easy;
        InitLevelData();
    }

    public void SetNormalLevel()
    {
        LevelData3.Instance.level = Level.Normal;
        InitLevelData();
    }

    public void SetHardLevel()
    {
        LevelData3.Instance.level = Level.Hard;
        InitLevelData();
    }

    public void InitSettingPanel()
    {
        switch ((int)level)
        {
            case 0:
                if (GameObject.Find("MidOpen") != null)
                    GameObject.Find("MidOpen").SetActive(false);
                if (GameObject.Find("HighOpen") != null)
                    GameObject.Find("HighOpen").SetActive(false);
                break;
            case 1:
                if (GameObject.Find("LowOpen") != null)
                    GameObject.Find("LowOpen").SetActive(false);
                if (GameObject.Find("HighOpen") != null)
                    GameObject.Find("HighOpen").SetActive(false);
                break;
            case 2:
                if (GameObject.Find("LowOpen") != null)
                    GameObject.Find("LowOpen").SetActive(false);
                if (GameObject.Find("MidOpen") != null)
                    GameObject.Find("MidOpen").SetActive(false);
                break;
        }
    }

    void InitLevelData()
    {
        switch (level)
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
