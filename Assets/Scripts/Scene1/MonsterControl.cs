using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    public float distance, speed;
    private Vector3 destination, initialPos;

    void Start()
    {
        initialPos = transform.position;
        destination = initialPos + new Vector3(0, -distance, 0);
    }

    void Update()
    {
        move();
    }

    void destinationControl()
    {
        if ((transform.position - destination).magnitude < 0.1f)
        {
            destination = destination == initialPos ? initialPos + new Vector3(0, -distance, 0) : initialPos;
        }
    }

    void move()
    {
        destinationControl();
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
    }
}
