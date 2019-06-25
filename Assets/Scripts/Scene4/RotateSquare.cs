using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSquare : MonoBehaviour
{
    private float rotateAngle = 0;
    public float speed;
    void Start()
    {
        
    }

   
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        rotateAngle += Time.deltaTime * speed;
        transform.rotation = Quaternion.EulerAngles(0, 0, rotateAngle);
    }
}
