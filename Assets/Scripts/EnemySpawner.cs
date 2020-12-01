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
            //Instantiate(enemy, transform.position, Quaternion.identity);
            EnemyCheker();
        }
    }
    void EnemyCheker()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyTag");
        int enemiesCol = enemies.Length;
        if (enemiesCol<=6)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }

    }
}
