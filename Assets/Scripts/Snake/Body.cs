using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private GameObject parent, body, newBody;
    [HideInInspector] public Vector2 pos;
    private bool power = false;
    public Body(GameObject parent, GameObject body, Vector2 pos)
    {
        this.body = body;
        this.parent = parent;
        this.pos = pos;
    }
    public void ShowBody()
    {
        newBody = GameObject.Instantiate(body, pos, Quaternion.identity);
        newBody.transform.SetParent(parent.transform, false);
    }

    public void DestroyBody()
    {
        Destroy(newBody);
    }
    public void move()
    {
        newBody.GetComponent<Rigidbody2D>().MovePosition(pos);
    }

    public Vector2 GetCurrentPos()
    {
        return newBody.transform.position;
    }

    public Vector3 GetCurrentVector3Pos()
    {
        return newBody.transform.position;
    }

    public void SetRotation(Vector3 direction)
    {
        newBody.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int type = 0;
        Destroy(collision.gameObject);
        switch (collision.tag)
        {
            case "food":
                SnakeControl.Instance.AddBody(SnakeControl.Instance.foodLength);
                MapManager.Instance.foods.Remove(collision.gameObject);
                if(SnakeControl.Instance.autoFindFood)
                {
                    MapManager.Instance.updatePoints();
                }
                type = 2;
                break;
            case "grass":
                if (!power) SnakeControl.Instance.DeleteBody(SnakeControl.Instance.grasslength);
                type = 4;
                break;
            case "mush":
                SnakeControl.Instance.AddBody(SnakeControl.Instance.snake.Count);
                type = 3;
                break;
            case "boom":
                if (!power) SnakeControl.Instance.DeleteBody(SnakeControl.Instance.snake.Count / 2);
                type = 0;
                break;
            case "energy":
                SnakeControl.Instance.speed = SnakeControl.Instance.energySpeed;
                Invoke("recoverSpeed", SnakeControl.Instance.speedUpTime);
                type = 1;
                break;
            case "sheild":
                power = true;
                Invoke("recoverPower", SnakeControl.Instance.powerTime);
                type = 5;
                break;
            case "SmartGrass":
                MapManager.Instance.updatePoints();
                SnakeControl.Instance.autoFindFood = true;
                Invoke("recoverAuto", SnakeControl.Instance.autoTime);
                type = 6;
                break;
        }
        int x = (int)collision.transform.position.x + 40, y = (int)collision.transform.position.y + 40;
        MapManager.Instance.Grids[x, y].SetUseFul(true);        
        MapManager.Instance.RandomProp(1, MapManager.Instance.propPrefabs[type]);
    }

    private void recoverSpeed()
    {
        SnakeControl.Instance.speed = SnakeControl.Instance.commonSpeed;
    }

    private void recoverPower()
    {
        power = false;
    }

    private void recoverAuto()
    {
        SnakeControl.Instance.autoFindFood = false;
        DrawLine.Instance.lineRenderer.positionCount = 0;
    }
}

