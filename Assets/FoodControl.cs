﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl : MonoBehaviour
{
    public GameObject food;
    public float intervalTime, speed;
    public int min, max, maxCount;
    private float timing = 0;

    private void Awake()
    {
        timing = intervalTime;
    }


    void Update()
    {
        timing += Time.deltaTime;
        if (timing >= intervalTime)
        {
            init();
            timing = 0;
        }
    }

    void init()
    {
        int count = Random.Range(0, maxCount);
        int y = 0;
        for (int i = 0; i < count; i++) 
        {
            y = Random.Range(-4, 5);
            GameObject temp = Instantiate(food, new Vector3(15, y, 0), Quaternion.identity);
            temp.GetComponent<Diamond>().min = this.min;
            temp.GetComponent<Diamond>().max = this.max;
            temp.GetComponent<Diamond>().speed = this.speed;
            temp.GetComponent<Diamond>().RandomNum(Random.Range(min,max));
        }
    }
}
