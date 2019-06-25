using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public GameObject[] Circles;
    public float Radius, Speed;
    private float[] angles;


    private void Awake()
    {
        angles = new float[2];
        angles[0] = 0;
        angles[1] = 180;
    }

    void Update()
    {
        RotateControl();
    }

    void RotateControl()
    {
        for (int i = 0; i < Circles.Length; i++)
        {
            Circles[i].transform.position = transform.position + Radius * new Vector3(Mathf.Cos(angles[i]), Mathf.Sin(angles[i]), Circles[i].transform.position.z);
            angles[i] += Time.deltaTime * Speed;
        }

    }
}
