using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void LoadScene2_1()
    {
        SceneManager.LoadScene("Scene2-1");
    }
    public void LoadScene2_2()
    {
        SceneManager.LoadScene("Scene2-2");
    }
    public void LoadScene2_3()
    {
        SceneManager.LoadScene("Scene2-3");
    }
    public void LoadScene2_4()
    {
        SceneManager.LoadScene("Scene2-4");
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene("Scene3");
    }
    public void LoadScene4()
    {
        SceneManager.LoadScene("Scene4");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
