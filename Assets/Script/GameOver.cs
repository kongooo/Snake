using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int len = GameObject.Find("Snake").GetComponent<SnakeControl>().length;
        if (len <= 4){
            Time.timeScale = 0;
        }
    }
}
