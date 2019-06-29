using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLevelData : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(initStart);
    }

    private void initStart()
    {
        LevelData.Instance.StartScene = true;
    }
}
