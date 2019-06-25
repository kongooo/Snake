using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    public GameObject hazard;
    public Vector2 spawnValule;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWaves();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnWaves();
    }
    void SpawnWaves()
    {
        Vector2 spawnPosition = new Vector2(spawnValule.x, spawnValule.y);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);
    }
}
