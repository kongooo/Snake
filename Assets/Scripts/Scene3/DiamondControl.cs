using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondControl : MonoBehaviour
{
    public GameObject[] diamonds;
    public float intervalTime;
    public float speed;
    public int min, max;
    private float timing = 0;

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
        int num = Random.Range(0, diamonds.Length);
        for (int i = 0; i < diamonds.Length; i++) 
        {
            GameObject diamond = Instantiate(diamonds[i], gameObject.transform, false);
            diamond.GetComponent<Diamond>().min = this.min;
            diamond.GetComponent<Diamond>().max = this.max;
            diamond.GetComponent<Diamond>().speed = this.speed;
            num = i == num ? Random.Range(min, HorizontalMoveControl.Instance.snake.Count) : Random.Range(min, max);
            diamond.GetComponent<Diamond>().RandomNum(num);
        }
    }


}
