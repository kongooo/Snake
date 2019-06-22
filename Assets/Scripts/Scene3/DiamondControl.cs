using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondControl : MonoBehaviour
{
    public GameObject[] diamonds;
    public float speed, intervalTime;
    public int min, max;
    private float timing = 0;

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
        for (int i = 0; i < diamonds.Length; i++) 
        {
            GameObject diamond = Instantiate(diamonds[i], gameObject.transform, false);
            diamond.GetComponent<Diamond>().min = this.min;
            diamond.GetComponent<Diamond>().max = this.max;
            diamond.GetComponent<Diamond>().speed = this.speed;
        }
    }


}
