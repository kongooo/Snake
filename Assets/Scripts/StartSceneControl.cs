using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetSettingPanel()
    {
        if (LevelData.Instance != null)
            LevelData.Instance.InitSettingPanel();
    }
}
