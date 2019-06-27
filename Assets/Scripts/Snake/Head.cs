using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Head : MonoBehaviour
{
    private bool power = false, doubleScore = false, show = false;
    public GameObject sheildPrefab;
    public Sprite snakeDizzy, snakeNormal;
    private GameObject tempSheild;
    private TextMeshProUGUI countDownText;
    private float timing = 0;
    public float showTime;
    private Canvas countCanvas;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Scene1") 
        {
            countCanvas = GameObject.Find("CountCanvas").GetComponent<Canvas>();
            countDownText = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
            countDownText.text = " ";
        }        
    }

    private void Update()
    {
        if (show) ShowCountDown();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int type = 0;
        switch (collision.tag)
        {
            case "food":
                if (SceneManager.GetActiveScene().name != "Scene1") 
                {
                    Scene2Controller.Instance.ChangeScore(100);
                    SnakeControl.Instance.AddBody(1);
                }
                else
                {
                    SnakeControl.Instance.AddBody(SnakeControl.Instance.foodLength);
                    MapManager.Instance.foods.Remove(collision.gameObject);
                    if (SnakeControl.Instance.autoFindFood)
                    {
                        MapManager.Instance.updatePoints();
                        Scene1Controller.Instance.ChangeScore(5);
                    }
                    else
                    {
                        Scene1Controller.Instance.ChangeScore(doubleScore ? 20 : 10);
                        doubleScore = true;
                        Invoke("recoverDouble", 10);
                    }
                }
                type = 2;
                Destroy(collision.gameObject);
                break;
            case "grass":
                if (!power)
                {
                    SnakeControl.Instance.DeleteBody(SnakeControl.Instance.grasslength);
                    Scene1Controller.Instance.ChangeScore(-10);
                }
                Destroy(collision.gameObject);
                type = 4;
                break;
            case "mush":
                SnakeControl.Instance.AddBody(SnakeControl.Instance.snake.Count);
                Scene1Controller.Instance.DoubleScore();
                type = 3;
                Destroy(collision.gameObject);
                break;
            case "boom":
                if (!power)
                {
                    SnakeControl.Instance.DeleteBody(SnakeControl.Instance.snake.Count / 2);
                    Scene1Controller.Instance.MulScore();
                }
                type = 0;
                Destroy(collision.gameObject);
                break;
            case "energy":
                SnakeControl.Instance.speed = SnakeControl.Instance.energySpeed;
                Scene1Controller.Instance.UpdateSpeed((int)SnakeControl.Instance.speed);
                Invoke("recoverSpeed", SnakeControl.Instance.speedUpTime);
                type = 1;
                Destroy(collision.gameObject);
                break;
            case "sheild":
                power = true;
                tempSheild = GameObject.Instantiate(sheildPrefab, gameObject.transform.position, Quaternion.identity);
                tempSheild.transform.SetParent(gameObject.transform, true);
                Invoke("recoverPower", SnakeControl.Instance.powerTime);
                type = 5;
                Destroy(collision.gameObject);
                break;
            case "SmartGrass":
                MapManager.Instance.updatePoints();
                SnakeControl.Instance.autoFindFood = true;
                Scene1Controller.Instance.ChangeScore(10);
                gameObject.GetComponent<SpriteRenderer>().sprite = snakeDizzy;
                Invoke("recoverAuto", SnakeControl.Instance.autoTime);
                Invoke("StartShowCountDown", SnakeControl.Instance.autoTime - showTime);
                type = 6;
                Destroy(collision.gameObject);
                break;
            case "key":
                Destroy(GameObject.FindGameObjectWithTag("lockedDoor"));
                Destroy(collision.gameObject);
                break;
            case "diamond":
                HorizontalMoveControl.Instance.DeleteBody(collision.gameObject.GetComponent<Diamond>().randomNum);
                Scene3Controller.Instance.ChangeScore(collision.gameObject.GetComponent<Diamond>().randomNum * 5);
                Destroy(collision.gameObject);
                break;
            case "MoveFood":
                HorizontalMoveControl.Instance.AddBody(collision.gameObject.GetComponent<Diamond>().randomNum);
                Scene3Controller.Instance.ChangeScore(collision.gameObject.GetComponent<Diamond>().randomNum * 10);
                Destroy(collision.gameObject);
                break;
            case "Success":
                SnakeControl.Instance.death = true;
                Scene2Controller.Instance.AfterSuccess();
                break;
            case "barrier":
                if (!SnakeMove4.Instance.death && GetComponent<SpriteRenderer>().color != collision.gameObject.GetComponent<SpriteRenderer>().color)
                {
                    SnakeMove4.Instance.death = true;
                    Scene4Controller.Instance.GameOver();
                }
                break;
        }
        if (SceneManager.GetActiveScene().name == "Scene1")
        {
            int x = (int)collision.transform.position.x + 40, y = (int)collision.transform.position.y + 40;
            MapManager.Instance.Grids[x, y].SetUseFul(true);
            MapManager.Instance.RandomProp(1, MapManager.Instance.propPrefabs[type]);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Scene1" && collision.gameObject.tag == "wall") 
            SnakeControl.Instance.death = true;
    }

    private void recoverSpeed()
    {
        SnakeControl.Instance.speed = SnakeControl.Instance.commonSpeed;
        Scene1Controller.Instance.UpdateSpeed((int)SnakeControl.Instance.speed);
    }

    private void ShowCountDown()
    {
        timing += Time.deltaTime;
        countDownText.text = ((int)(showTime - (int)timing)).ToString();
        countCanvas.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }

    private void StartShowCountDown()
    {
        timing = 0;
        show = true;
    }

    private void recoverPower()
    {
        power = false;
        Destroy(tempSheild);
    }

    private void recoverAuto()
    {
        show = false;
        countDownText.text = "";
        SnakeControl.Instance.autoFindFood = false;
        DrawLine.Instance.lineRenderer.positionCount = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = snakeNormal;
    }

    private void recoverDouble()
    {
        doubleScore = false;
    }
}
