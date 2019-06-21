using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControl : MonoBehaviour
{
    private static SnakeControl _instance;
    public static SnakeControl Instance { get { return _instance; } }

    public GameObject bodyPrefab, headPrefab;
    [HideInInspector] public List<Body> snake = new List<Body>();

    public int length, foodLength, grasslength;
    public float space, energySpeed, speedUpTime, commonSpeed, powerTime;
    public bool tailDocking;
    [HideInInspector] public float speed;
    public float mapScale, cameraScale, addNum;
    private Vector2 moveDirect = Vector2.zero, movePos = Vector2.zero;
    [HideInInspector] public int posIndex = 0;
    [HideInInspector] public bool startMove = false;
    void Awake()
    {
        _instance = this;
        SnakeInit();
    }
    private void Start()
    {
        //mapScale = MapManager.Instance.mapScale;
        speed = commonSpeed;
    }
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(startMove)
        {
            SnakeMoveControl(false);
            cameraControl();
        }       
    }

    void SnakeInit()
    {
        Vector2 pos = transform.position;
        snake.Add(new Body(gameObject, headPrefab, pos));
        snake[0].ShowBody();
        for (int i = 1; i < length; i++) 
        {
            pos -= new Vector2(0.0f, space);
            snake.Add(new Body(gameObject, bodyPrefab, pos));
            snake[i].ShowBody();
        }
    }

    void directControl()
    {
        moveDirect = (movePos - snake[0].GetCurrentPos()).normalized;
        Debug.Log("direct: " + moveDirect);
        Debug.Log("snake pos: " + snake[0].GetCurrentPos());
    }

    void PosControl()
    {
        if ((snake[0].GetCurrentPos() - MapManager.Instance.points[posIndex]).magnitude < 0.1f) 
        {
            if (posIndex > 0) posIndex--;
            movePos = MapManager.Instance.points[posIndex];
            Debug.Log("Pos: " + movePos);
        }
    }

    void SnakeMoveControl(bool mouse)
    {
        Vector2 direction = Vector2.zero;
        if (mouse)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            direction = (new Vector2(mouseWorldPos.x, mouseWorldPos.y) - snake[0].pos).normalized;
            SnakeMove(direction);
        }
        else
        {
            PosControl();
            directControl();
            direction = moveDirect;
            if ((snake[0].GetCurrentPos() - MapManager.Instance.points[0]).magnitude > 0.1f)
                SnakeMove(direction);
        }
        
    }

    void SnakeMove(Vector2 direction)
    {        
        snake[0].pos = direction * speed * Time.deltaTime + snake[0].GetCurrentPos();;
        snake[0].SetRotation(new Vector3(direction.x, direction.y, 0));
        snake[0].move();
        for (int i = 1; i < snake.Count; i++) 
        {
            Vector2 direct = (snake[i].pos - snake[i - 1].pos).normalized;
            Vector2 basePos = tailDocking ? snake[i - 1].pos : snake[i - 1].GetCurrentPos();
            snake[i].pos = basePos + direct * space;
            snake[i].move();
        }
        
    }

    void cameraControl()
    {
        float cameraX, cameraY;
        Vector2 cameraCurrentPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        cameraX = Mathf.Abs(snake[0].pos.x) < mapScale - cameraScale * (Camera.main.pixelWidth / 100) + addNum ? Mathf.Abs(snake[0].pos.x) : mapScale - cameraScale * (Camera.main.pixelWidth / 100) + addNum;
        cameraY = Mathf.Abs(snake[0].pos.y) < mapScale - cameraScale * (Camera.main.pixelHeight / 100) + addNum ? Mathf.Abs(snake[0].pos.y) : mapScale - cameraScale * (Camera.main.pixelHeight / 100) + addNum;
        if (snake[0].pos.x < 0) cameraX = -cameraX;
        if (snake[0].pos.y < 0) cameraY = -cameraY;
        if ((cameraCurrentPos - snake[0].GetCurrentPos()).magnitude > 1.5f)
            Camera.main.transform.Translate((snake[0].GetCurrentPos() - cameraCurrentPos).normalized * Time.deltaTime * speed);
    }

    public void AddBody(int count)
    {
        for (int i = 0; i < count; i++) 
        {
            Vector2 newPos = 2 * snake[snake.Count - 1].pos - snake[snake.Count - 2].pos;
            snake.Add(new Body(gameObject, bodyPrefab, newPos));
            snake[snake.Count - 1].ShowBody();
        }      
    }

    public void DeleteBody(int count)
    {
        int lastIndex = snake.Count - 1;
        for (int i = 0; i < count; i++) 
        {
            snake[lastIndex].DestroyBody();
            snake.RemoveAt(lastIndex--);
        }
    }
}
