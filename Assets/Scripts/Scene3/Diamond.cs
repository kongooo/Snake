using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    public TextMeshProUGUI Text;
    [HideInInspector] public int min, max, randomNum;
    [HideInInspector] public float speed;

   
    void Update()
    {
        Move();
        if (transform.position.x < -12) Destroy(gameObject);
    }

    public void RandomNum(int num)
    {
        randomNum = num;
        Text.text = randomNum.ToString();
    }

    void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MoveFood")
            Destroy(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MoveFood")
            Destroy(collision.gameObject);
    }

    
}
