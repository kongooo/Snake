using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaffControl : MonoBehaviour
{
    private Vector3 direction = Vector3.up, destination = Vector3.up, initialPos = Vector3.up;

    public float speed, distance, intervalTime;

    private float timeing = 0;
    private bool show = false;

    private void Awake()
    {
        InitDirect();
    }

    void Update()
    {
        timeControl();
    }

    void timeControl()
    {
        timeing += Time.deltaTime;
        if (timeing >= intervalTime) 
        {
            destinationControl();
            IntervalShow();
        }
    }

    void destinationControl()
    {
        if ((transform.position - destination).magnitude < 0.1f) 
        {
            if (destination == initialPos) timeing = 0;
            destination = destination == initialPos ? initialPos + direction * distance : initialPos;
        }
    }

    void IntervalShow()
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
    }

    void InitDirect()
    {
        switch (transform.rotation.eulerAngles.z)
        {
            case 0:
                direction = Vector3.up;
                break;
            case 90:
                direction = Vector3.left;
                break;
            case 270:
                direction = Vector3.right;
                break;
            case 180:
                direction = Vector3.down;
                break;
        }
        destination = transform.position + direction * distance;
        initialPos = transform.position;
    }
}
