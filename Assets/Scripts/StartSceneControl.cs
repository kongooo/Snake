using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        Exit();
    }

    void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void SetSettingPanel()
    {
        if (LevelData.Instance != null)
            LevelData.Instance.InitSettingPanel();
    }
}
