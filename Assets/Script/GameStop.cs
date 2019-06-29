using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStop : MonoBehaviour
{
    bool change = true;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                LevelData1.Instance.levelAgo = LevelData1.Instance.level;
                LevelData1.Instance.InitSettingPanel();
                break;
            case 2:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                LevelData2.Instance.InitSettingPanel();
                break;
            case 3:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                LevelData2.Instance.InitSettingPanel();
                break;
            case 4:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                LevelData2.Instance.InitSettingPanel();
                break;
            case 5:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                LevelData2.Instance.InitSettingPanel();
                break;
            case 6:
                LevelData3.Instance.levelAgo = LevelData3.Instance.level;
                LevelData3.Instance.InitSettingPanel();
                break;
            case 7:
                LevelData4.Instance.levelAgo = LevelData4.Instance.level;
                LevelData4.Instance.InitSettingPanel();
                break;
        }
        LevelData.Instance.StartScene = false;
        Time.timeScale = 0;
    }
}
