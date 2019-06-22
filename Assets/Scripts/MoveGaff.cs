using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGaff : MonoBehaviour
{
    private Vector3 direction, initialPos;
    [HideInInspector]public float distance, speed;
    private bool death = false;

    private void Start()
    {
        InitDirect();
    }

    void Update()
    {
        if (!death) move();
        if (death) Destroy(gameObject);
    }

    void move()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if ((transform.position - initialPos).magnitude >= distance) death = true;
    }


    void InitDirect()
    {
        initialPos = transform.position;
        switch (transform.rotation.eulerAngles.z)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 90:
                direction = Vector2.left;
                break;
            case -90:
                direction = Vector2.right;
                break;
            case 180:
                direction = Vector2.down;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
            death = true;
    }
}
