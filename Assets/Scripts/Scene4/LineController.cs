using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public bool Red, Yellow, Green, Blue;
    public float intervalTime;
    private float timing = 0;
    private Color red, yellow, green, blue;
    private List<Color> colorList = new List<Color>();
    private bool stop = false;

    private void Awake()
    {
        initColor();
    }
    void Update()
    {
        timing += Time.deltaTime;
        if (timing > intervalTime && !stop) 
        {
            RandomColor();
            timing = 0;
        }
    }

    void initColor()
    {
        red = new Color32(255, 193, 172, 255);
        yellow = new Color32(255, 253, 200, 255);
        green = new Color32(197, 255, 224, 255);
        blue = new Color32(172, 215, 255, 255);
        
        if (Red) colorList.Add(red);
        if (Yellow) colorList.Add(yellow);
        if (Green) colorList.Add(green);
        if (Blue) colorList.Add(blue);
    }

    void RandomColor()
    {
        GetComponent<SpriteRenderer>().color = colorList[Random.Range(0, colorList.Count)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") stop = true;
    }
}
