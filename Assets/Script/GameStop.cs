﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStop : MonoBehaviour
{
    public Button mButton;
    public int Stop_num;
    // Use this for initialization
    void Start()
    {
        //Gets ButtonMount
        Button btnMount = mButton.GetComponent<Button>();
        //add a listener to ButtonMount, executing TaskOnClick() when click ButtonMount
        btnMount.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Loading Scene1
        Time.timeScale = Stop_num;
    }
}