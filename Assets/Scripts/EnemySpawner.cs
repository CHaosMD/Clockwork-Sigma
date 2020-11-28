using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    GameObject[] enemies;
    //float randX;
    //Vector2 whereToSpawn;
    float spawnRate = 2f;
    float nextSpawn;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;
        spawnRate = Random.Range(1f,4f);
        nextSpawn = Random.Range(1f,4f);
    }

    // Update is called once per frame
    void Update()
    {
        startTime = Time.timeSinceLevelLoad;
        if (startTime > nextSpawn)
        {
            nextSpawn = startTime + spawnRate;
            spawnRate = Random.Range(1f, 4f);
            //randX = Random.Range(-3f, 3f);
            //whereToSpawn = new Vector2(transform.position.x, transform.position.y);
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
