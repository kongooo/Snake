using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsControl : MonoBehaviour
{
    public GameObject walls;
    public float intervalTime;
    private float timing = 0, speed;

    private void Awake()
    {
        timing = intervalTime;
    }

    private void Start()
    {
        this.speed = HorizontalMoveControl.Instance.speed;
    }

    void Update()
    {
        timing += Time.deltaTime;
        if (!HorizontalMoveControl.Instance.death && timing >= intervalTime)
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
