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
    public float space, energySpeed, speedUpTime, commonSpeed, powerTime, autoTime;
    public bool tailDocking, autoFindFood = false;
    [HideInInspector] public float speed;
    private Vector2 moveDirect = Vector2.zero;
    [HideInInspector] public Vector2 movePos = Vector2.zero;
    [HideInInspector] public int posIndex = 0;
    [HideInInspector] public bool startMove = false;
    Vector2 direction = Vector2.zero;
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
            SnakeMoveControl(autoFindFood);
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
            pos -= new Vector2(0.0f, space + 0.1f);
            snake.Add(new Body(gameObject, bodyPrefab, pos));
            snake[i].ShowBody();
        }
    }

    void directControl()
    {
        moveDirect = (movePos - snake[0].GetCurrentPos()).normalized;
    }

    void PosControl()
    {
        if (posIndex >= 0 && (snake[0].GetCurrentPos() - MapManager.Instance.points[posIndex]).magnitude < 0.1f) 
        {
            DrawLine.Instance.Draw();
            if (posIndex > 0) posIndex--;
            movePos = MapManager.Instance.points[posIndex];
        }
    }

    void SnakeMoveControl(bool autoFindFood)
    {
        if (!autoFindFood)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector3 diff = new Vector2(mouseWorldPos.x, mouseWorldPos.y) - snake[0].pos;
            if (diff.magnitude > 1.0f)
                direction = diff.normalized;
            SnakeMove(direction);
        }
        else
        {
            PosControl();
            directControl();
            direction = moveDirect;
            if ((snake[0].GetCurrentPos() - MapManager.Instance.points[0]).magnitude > 0.1f)
                SnakeMove(direction);
            if (DrawLine.Instance.linePos.Count > 0) 
                DrawLine.Instance.lineRenderer.SetPosition(DrawLine.Instance.linePos.Count - 1, snake[0].GetCurrentVector3Pos());
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
        cameraX = Mathf.Abs(snake[0].pos.x) < MapManager.Instance.mapScale - (Camera.main.pixelWidth / 100) ? Mathf.Abs(snake[0].pos.x) : MapManager.Instance.mapScale - (Camera.main.pixelWidth / 100);
        cameraY = Mathf.Abs(snake[0].pos.y) < MapManager.Instance.mapScale - (Camera.main.pixelHeight / 100) ? Mathf.Abs(snake[0].pos.y) : MapManager.Instance.mapScale - (Camera.main.pixelHeight / 100);
        if (snake[0].pos.x < 0) cameraX = -cameraX;
        if (snake[0].pos.y < 0) cameraY = -cameraY;
        Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x, cameraX, Time.deltaTime * speed), Mathf.Lerp(Camera.main.transform.position.y, cameraY, Time.deltaTime * speed), Camera.main.transform.position.z);
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
