using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTool : MonoBehaviour
{
    private static DontDestroyTool _instance;
    public static DontDestroyTool Instance { get { return _instance; } }
    public int SnakeOrder;

    void Awake()
    {
        if(_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void orderSet(int order)
    {
        SnakeOrder = order;
    }
    public int getOrder()
    {
        return SnakeOrder;
    }
}