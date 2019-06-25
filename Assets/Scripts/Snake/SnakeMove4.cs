using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove4 : MonoBehaviour
{
    private static SnakeMove4 _instance;
    public static SnakeMove4 Instance { get { return _instance; } }
    public GameObject bodyPrefab;
    [HideInInspector] public List<Body> snake = new List<Body>();
    public float space, length, Yspeed, Xspeed, YmaxSpeed;
    public bool death = false;

    private void Awake()
    {
        _instance = this;
        SnakeInit();
    }
    private void FixedUpdate()
    {
        HorizontalMove();
        CameraControl();
    }


    void SnakeInit()
    {
        Vector2 pos = transform.position;
        snake.Add(new Body(gameObject, bodyPrefab, pos));
        snake[0].ShowBody();
        for (int i = 1; i < length; i++)
        {
            pos -= new Vector2(space, 0);
            snake.Add(new Body(gameObject, bodyPrefab, pos));
            snake[i].ShowBody();
        }
    }

    void HorizontalMove()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z));
        snake[0].newBody.GetComponent<Rigidbody2D>().MovePosition(new Vector2(snake[0].GetCurrentPos().x + Xspeed * Time.deltaTime, Mathf.Lerp(snake[0].GetCurrentPos().y, mouseWorldPos.y, Time.deltaTime * Yspeed)));
        for (int i = 1; i < snake.Count; i++)
        {
            snake[i].newBody.GetComponent<Rigidbody2D>().MovePosition(snake[i-1].GetCurrentPos());
        }
        //SpeedControl();
    }

    void SpeedControl()
    {

        for (int i = 0; i < snake.Count; i++) 
        {
            //Debug.Log(snake[i].newBody.GetComponent<Rigidbody2D>().velocity);
            if (snake[i].newBody.GetComponent<Rigidbody2D>().velocity.y > YmaxSpeed)
                snake[i].newBody.GetComponent<Rigidbody2D>().velocity = new Vector2(snake[i].newBody.GetComponent<Rigidbody2D>().velocity.x, YmaxSpeed);
        }
    }

    void CameraControl()
    {
        Camera.main.transform.position = new Vector3(snake[0].GetCurrentPos().x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
