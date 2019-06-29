using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoveControl : MonoBehaviour
{
    private static HorizontalMoveControl _instance;
    public static HorizontalMoveControl Instance { get { return _instance; } }
    public GameObject bodyPrefab, headPrefab;
    [HideInInspector] public List<Body> snake = new List<Body>();
    public float space, length, speed;
    public bool death = false;

    private void Awake()
    {
        Time.timeScale = 1;
        _instance = this;
        SnakeInit();
    }
    private void FixedUpdate()
    {
        if (!death)
            HorizontalMove();
    }

    void SnakeInit()
    {
        Vector2 pos = transform.position;
        snake.Add(new Body(gameObject, headPrefab, pos));
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
        snake[0].SetRotation((mouseWorldPos - snake[0].GetCurrentVector3Pos()).normalized);
        snake[0].newBody.GetComponent<Rigidbody2D>().MovePosition(new Vector2(snake[0].GetCurrentPos().x, Mathf.Lerp(snake[0].GetCurrentPos().y, mouseWorldPos.y, Time.deltaTime * speed)));
        for (int i = 1; i < snake.Count; i++)
        {
            snake[i].newBody.GetComponent<Rigidbody2D>().MovePosition(new Vector2(snake[i].GetCurrentPos().x, snake[i - 1].GetCurrentPos().y));           
        }
    }

    public void AddBody(int count)
    {
        int last = snake.Count - 1;
        Vector2 pos = snake[last].GetCurrentPos();
        for (int i = last; i < last + count; i++)
        {
            pos -= new Vector2(space, 0);
            snake.Add(new Body(gameObject, bodyPrefab, pos));
            snake[snake.Count - 1].ShowBody();
        }
    }

    public void DeleteBody(int count)
    {
        if (count >= snake.Count && !death) 
        {
            death = true;
            Scene3Controller.Instance.AfterDeath();
            for (int i = 0; i < snake.Count; i++)
                Destroy(snake[i].newBody);
            return;
        }
        int length = snake.Count;
        for (int i = 0; i < count; i++) 
        {
            snake[length - 1 - i].DestroyBody();
            snake.RemoveAt(length - 1 - i);
        }
        if (snake.Count == 0) death = true;
    }
}
