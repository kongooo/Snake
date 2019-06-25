using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Body : MonoBehaviour
{
    private GameObject parent, body;
    [HideInInspector] public GameObject newBody;
    [HideInInspector] public Vector2 pos;
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

    public void SetPos(Vector2 newPos)
    {
        newBody.transform.position = newPos;
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
        switch (collision.tag)
        {            
            case "gaff":
                SnakeControl.Instance.DeleteBody(1);
                break;
            case "monster":
                SnakeControl.Instance.DeleteBody(1);
                break;
            case "MoveFood":
                HorizontalMoveControl.Instance.AddBody(collision.gameObject.GetComponent<Diamond>().randomNum);
                break;
            case "line":
                GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<SpriteRenderer>().color;
                break;
            case "barrier":
                if (GetComponent<SpriteRenderer>().color != collision.gameObject.GetComponent<SpriteRenderer>().color)
                    SnakeMove4.Instance.death = true;
                break;
        }
        if (SceneManager.GetActiveScene().name == "Scene0") 
        {
            int x = (int)collision.transform.position.x + 40, y = (int)collision.transform.position.y + 40;
            MapManager.Instance.Grids[x, y].SetUseFul(true);
            MapManager.Instance.RandomProp(1, MapManager.Instance.propPrefabs[type]);
        }
    }

    
}

