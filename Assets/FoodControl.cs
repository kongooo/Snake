using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl : MonoBehaviour
{
    public GameObject food;
    public float intervalTime;
    private float speed;
    public int min, max, maxCount;
    private float timing = 0;
    private List<int> avoidRepeat = new List<int>();

    private void Awake()
    {
        timing = intervalTime;
    }

    private void Start()
    {
        this.speed = HorizontalMoveControl.Instance.speed;
    }


    void Update()
    {
        timing += Time.deltaTime;
        if (timing >= intervalTime)
        {
            init();
            timing = 0;
        }
    }

    void init()
    {
        int count = Random.Range(0, maxCount);
        int y = 0;
        avoidRepeat.Clear();
        for (int i = 0; i < count; i++) 
        {
        repeat: y = Random.Range(-4, 5);
            if (avoidRepeat.Contains(y)) goto repeat;
            GameObject temp = Instantiate(food, new Vector3(15, y, 0), Quaternion.identity);
            temp.GetComponent<Diamond>().min = this.min;
            temp.GetComponent<Diamond>().max = this.max;
            temp.GetComponent<Diamond>().speed = this.speed;
            temp.GetComponent<Diamond>().RandomNum(Random.Range(min,max));
            avoidRepeat.Add(y);
        }
    }
}
