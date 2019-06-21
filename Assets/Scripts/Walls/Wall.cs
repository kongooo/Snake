using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall 
{
    private bool rotate = false;
    private int size;
    private GameObject wall, parent, newWall;
    private Vector2 pos;

    public Wall(bool rotate, Vector2 pos, GameObject wallPrefab, GameObject parentPrefab)
    {
        this.rotate = rotate;
        this.wall = wallPrefab;
        this.parent = parentPrefab;
        this.pos = pos;
    }

    public void ShowWall()
    {
        newWall = GameObject.Instantiate(wall, pos, Quaternion.identity);
        newWall.transform.SetParent(parent.transform, false);
        if(rotate) newWall.transform.RotateAround(new Vector3(0, 0, 1), Mathf.PI/2);
    }

    public int GetSize()
    {
        return (int)newWall.GetComponent<BoxCollider2D>().size.x;
    }

    public Vector2 GetPos()
    {
        return pos;
    }

    public bool GetRotate()
    {
        return rotate;
    }
}
