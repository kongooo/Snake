using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStop : MonoBehaviour
{
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
                break;
            case 2:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                break;
            case 3:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                break;
            case 4:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                break;
            case 5:
                LevelData2.Instance.levelAgo = LevelData2.Instance.level;
                break;
            case 6:
                LevelData3.Instance.levelAgo = LevelData3.Instance.level;
                break;
            case 7:
                LevelData4.Instance.levelAgo = LevelData4.Instance.level;
                break;
        }
        Time.timeScale = 0;
    }
}
