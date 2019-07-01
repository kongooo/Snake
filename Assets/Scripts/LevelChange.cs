using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    public int Model, level;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeModel);
    }

    void ChangeModel()
    {
        switch(Model)
        {
            case 0:
                ChangeLevel();
                break;
            case 1:
                ChangeLevel1();
                break;
            case 2:
                ChangeLevel2();
                break;
            case 3:
                ChangeLevel3();
                break;
            case 4:
                ChangeLevel4();
                break;

        }
    }

    void ChangeLevel()
    {
        switch (level)
        {
            case 0:
                LevelData.Instance.SetEasyLevel();
                LevelData1.Instance.SetEasyLevel();
                LevelData2.Instance.SetEasyLevel();
                LevelData3.Instance.SetEasyLevel();
                LevelData4.Instance.SetEasyLevel();
                break;
            case 1:
                LevelData.Instance.SetNormalLevel();
                LevelData1.Instance.SetNormalLevel();
                LevelData2.Instance.SetNormalLevel();
                LevelData3.Instance.SetNormalLevel();
                LevelData4.Instance.SetNormalLevel();
                break;
            case 2:
                LevelData.Instance.SetHardLevel();
                LevelData1.Instance.SetHardLevel();
                LevelData2.Instance.SetHardLevel();
                LevelData3.Instance.SetHardLevel();
                LevelData4.Instance.SetHardLevel();
                break;
        }
    }

    void ChangeLevel1()
    {
        switch (level)
        {
            case 0:
                LevelData1.Instance.SetEasyLevel();
                break;
            case 1:
                LevelData1.Instance.SetNormalLevel();
                break;
            case 2:
                LevelData1.Instance.SetHardLevel();
                break;
        }
    }

    void ChangeLevel2()
    {
        switch (level)
        {
            case 0:
                LevelData2.Instance.SetEasyLevel();
                break;
            case 1:
                LevelData2.Instance.SetNormalLevel();
                break;
            case 2:
                LevelData2.Instance.SetHardLevel();
                break;
        }
    }

    void ChangeLevel3()
    {
        switch (level)
        {
            case 0:
                LevelData3.Instance.SetEasyLevel();
                break;
            case 1:
                LevelData3.Instance.SetNormalLevel();
                break;
            case 2:
                LevelData3.Instance.SetHardLevel();
                break;
        }
    }

    void ChangeLevel4()
    {
        switch (level)
        {
            case 0:
                LevelData4.Instance.SetEasyLevel();
                break;
            case 1:
                LevelData4.Instance.SetNormalLevel();
                break;
            case 2:
                LevelData4.Instance.SetHardLevel();
                break;
        }
    }
}
