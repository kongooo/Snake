using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGaffControl : MonoBehaviour
{
    public float distance, speed, intervalTime;
    public int orderPerSecond,record=-1;
    private float timing = 0;
    private Vector3 rotate = Vector3.zero;
    public enum direct { up, down, left, right}
    public direct Direction;
    public GameObject moveGaff;
    private void Start()
    {
        timing = intervalTime;
    }
    void Update()
    {
        controlTime();
    }

    void controlTime()
    {
        timing += Time.deltaTime;
        float num2 = timing;
        int num = Mathf.FloorToInt(timing * 10);
        Debug.Log(num);
        Debug.Log(timing);
        if (num % orderPerSecond == 0&&num!=record)
        {
            moveControl();
            timing = 0;
            record = num;
        }
        timing = num2;
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
