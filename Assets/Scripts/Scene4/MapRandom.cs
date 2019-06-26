using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRandom : MonoBehaviour
{
    public GameObject[] Blocks;
    private bool enter = false;

    void RandomNextBlock()
    {
        int index = Random.Range(0, Blocks.Length);
        float distance = (gameObject.GetComponent<BoxCollider2D>().size.x + Blocks[index].GetComponent<BoxCollider2D>().size.x) / 2;
        Vector3 newPos = transform.position + new Vector3(distance, 0, 0);
        Instantiate(Blocks[index], newPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !enter)
        {
            enter = true;
            RandomNextBlock();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "tail")
            Destroy(gameObject);
    }
}
