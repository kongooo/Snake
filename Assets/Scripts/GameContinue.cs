using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameContinue : MonoBehaviour
{
    [HideInInspector]public bool if_continue = true;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(IfContinue);
    }

    void IfContinue()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        switch (index)
        {
            case 1:
                if (LevelData1.Instance.levelAgo != LevelData1.Instance.level)
                    if_continue = false;
                break;
            case 6:
                if (LevelData3.Instance.levelAgo != LevelData3.Instance.level)
                    if_continue = false;
                break;
            case 7:
                if (LevelData4.Instance.levelAgo != LevelData4.Instance.level)
                    if_continue = false;
                break;
        }
        if (index == 2 || index == 3 || index == 4 || index == 5)
        {
            if (LevelData2.Instance.levelAgo != LevelData2.Instance.level)
                if_continue = false;
        }


        if (if_continue)
        {
            Time.timeScale = 1;
        }           
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
            
    }
}
