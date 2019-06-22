using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    public TextMeshProUGUI Text;
    [HideInInspector]public int min, max;
    [HideInInspector]public float speed;

    private void Awake()
    {
        RandomNum();
    }


    void Update()
    {
        Move();
        if (transform.position.x < -12) Destroy(gameObject);
    }

    void RandomNum()
    {
        Text.text = Random.Range(min, max + 1).ToString();
    }

    void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
