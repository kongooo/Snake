using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGaffControl : MonoBehaviour
{
    public float distance, speed, intervalTime;
    private float timing = 0;
    private Vector3 rotate = Vector3.zero;
    public enum direct { up, down, left, right}
    public direct Direction;
    public GameObject moveGaff;

    void Update()
    {
        controlTime();
    }

    void controlTime()
    {
        timing += Time.deltaTime;
        if (timing >= intervalTime) 
        {
            moveControl();
            timing = 0;
        }
    }

    void initRotate()
    {
        switch (Direction)
        {
            case direct.up:
                rotate = new Vector3(0, 0, 0);
                break;
            case direct.left:
                rotate = new Vector3(0, 0, 90);
                break;
            case direct.right:
                rotate = new Vector3(0, 0, -90);
                break;
            case direct.down:
                rotate = new Vector3(0, 0, 180);
                break;
        }
    }

    void moveControl()
    {
        initRotate();
        GameObject gaff = GameObject.Instantiate(moveGaff, gameObject.transform.position, Quaternion.Euler(rotate.x, rotate.y, rotate.z));
        gaff.transform.SetParent(gameObject.transform, true);
        gaff.GetComponent<MoveGaff>().distance = this.distance;
        gaff.GetComponent<MoveGaff>().speed = this.speed;
    }
}
