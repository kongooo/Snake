using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (!HorizontalMoveControl.Instance.death)
            Move();
        if (transform.position.x < 0) Destroy(gameObject);
    }



    void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
