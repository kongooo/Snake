using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenceChange : MonoBehaviour
{
    public Button mButton;
    public int Sence_num;

    void Start()
    {
        Button btnMount = mButton.GetComponent<Button>();
        btnMount.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        LevelData.Instance.StartScene = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Sence_num);
    }
}