using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsControl : MonoBehaviour
{
    public GameObject walls;
    public float speed, intervalTime;
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
        GameObject theWalls = Instantiate(walls);
        theWalls.GetComponent<MovingWalls>().speed = speed;
    }
}
