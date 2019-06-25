using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseColor : MonoBehaviour
{
    public enum barrierColor { Red, Green, Blue, Yellow}
    public barrierColor BarrierColor;

    private void Awake()
    {
        initColor();
    }

    void initColor()
    {
        Color color = Color.black;
        switch(BarrierColor)
        {
            case barrierColor.Red:
                color = new Color32(255, 193, 172, 255);
                break;
            case barrierColor.Blue:
                color = new Color32(172, 215, 255, 255);
                break;
            case barrierColor.Green:
                color = new Color32(197, 255, 224, 255);
                break;
            case barrierColor.Yellow:
                color = new Color32(255, 253, 200, 255);
                break;
        }
        GetComponent<SpriteRenderer>().color = color;  
    }
}
